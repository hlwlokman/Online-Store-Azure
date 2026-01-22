using WidgetStore.Models;

namespace WidgetStore.Services
{
    public interface IReviewService
    {
        Task<string> CreateReviewAsync(Review review);
        Task<List<Review>> GetProductReviewsAsync(int productId);
    }
}