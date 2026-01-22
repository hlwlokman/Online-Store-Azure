using Microsoft.Azure.Cosmos;
using WidgetStore.Models;

namespace WidgetStore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly Container _container;

        public ReviewService(CosmosClient cosmosClient)
        {
            var database = cosmosClient.GetDatabase("WidgetStoreDB");
            _container = database.GetContainer("Reviews");
        }

        public async Task<string> CreateReviewAsync(Review review)
        {
            review.Id = Guid.NewGuid().ToString();
            review.CreatedDate = DateTime.UtcNow;

            await _container.CreateItemAsync(review, new PartitionKey(review.ProductId));
            return review.Id;
        }

        public async Task<List<Review>> GetProductReviewsAsync(int productId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.productId = @productId")
                .WithParameter("@productId", productId);

            var reviews = new List<Review>();
            var iterator = _container.GetItemQueryIterator<Review>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                reviews.AddRange(response);
            }

            return reviews;
        }
    }
}