using Azure;
using Azure.Data.Tables;

namespace WidgetStore.Models
{
    // Write Model - for creating orders
    public class CreateOrderCommand
    {
        public string UserEmail { get; set; }
        public List<OrderItemCommand> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderItemCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    // Read Model - for querying orders
    public class OrderReadModel
    {
        public string OrderId { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int OrderProcessedDays { get; set; }
    }

    // Table Storage Entity
    public class OrderEntity : ITableEntity
    {
        public string PartitionKey { get; set; } // UserEmail
        public string RowKey { get; set; } // OrderId
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ItemsJson { get; set; }
    }
}