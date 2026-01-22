using Azure.Data.Tables;
using Newtonsoft.Json;
using WidgetStore.Models;

namespace WidgetStore.Services
{
    // Order service implementation by Lokman
    // This service handles all order operations
    public class OrderService : IOrderService
    {
        private readonly TableClient _tableClient;

        public OrderService(TableServiceClient tableServiceClient)
        {
            // Connect to Orders table in Azure Table Storage
            _tableClient = tableServiceClient.GetTableClient("Orders");
            _tableClient.CreateIfNotExists();
        }

        // CQRS - Command (Write operation)
        // This method creates new order
        public async Task<string> CreateOrderAsync(CreateOrderCommand command)
        {
            var orderId = Guid.NewGuid().ToString();

            // Store order in Table Storage
            // Using user email as partition key for better performance
            var entity = new OrderEntity
            {
                PartitionKey = command.UserEmail, // For fast search by user
                RowKey = orderId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = command.TotalAmount,
                Status = "Pending",
                ItemsJson = JsonConvert.SerializeObject(command.Items)
            };

            await _tableClient.AddEntityAsync(entity);
            return orderId;
        }

        // CQRS - Query (Read operation)
        // This method gets order details
        public async Task<OrderReadModel> GetOrderAsync(string orderId, string userEmail)
        {
            var entity = await _tableClient.GetEntityAsync<OrderEntity>(userEmail, orderId);
            return MapToReadModel(entity.Value);
        }

        // Get all orders for one user
        public async Task<List<OrderReadModel>> GetUserOrdersAsync(string userEmail)
        {
            var orders = new List<OrderReadModel>();

            // Query using partition key (user email) for fast result
            await foreach (var entity in _tableClient.QueryAsync<OrderEntity>(
                filter: $"PartitionKey eq '{userEmail}'"))
            {
                orders.Add(MapToReadModel(entity));
            }

            return orders;
        }

        // Update order when shipped
        public async Task UpdateShippingDateAsync(string orderId, string userEmail, DateTime shippingDate)
        {
            var entity = await _tableClient.GetEntityAsync<OrderEntity>(userEmail, orderId);
            entity.Value.ShippingDate = shippingDate;
            entity.Value.Status = "Shipped";

            await _tableClient.UpdateEntityAsync(entity.Value, entity.Value.ETag);
        }

        // Helper method - Convert storage entity to read model
        private OrderReadModel MapToReadModel(OrderEntity entity)
        {
            // Calculate how many days to process order
            var processedDays = entity.ShippingDate.HasValue
                ? (entity.ShippingDate.Value - entity.OrderDate).Days
                : 0;

            return new OrderReadModel
            {
                OrderId = entity.RowKey,
                UserEmail = entity.PartitionKey,
                OrderDate = entity.OrderDate,
                ShippingDate = entity.ShippingDate,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status,
                OrderProcessedDays = processedDays
            };
        }
    }
}