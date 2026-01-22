using WidgetStore.Models;

namespace WidgetStore.Services
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(CreateOrderCommand command);
        Task<OrderReadModel> GetOrderAsync(string orderId, string userEmail);
        Task<List<OrderReadModel>> GetUserOrdersAsync(string userEmail);
        Task UpdateShippingDateAsync(string orderId, string userEmail, DateTime shippingDate);
    }
}