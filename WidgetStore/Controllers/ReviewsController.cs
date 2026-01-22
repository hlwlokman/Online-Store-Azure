using Microsoft.AspNetCore.Mvc;
using WidgetStore.Services;
using WidgetStore.Models;

namespace WidgetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateReview(Review review)
        {
            var reviewId = await _reviewService.CreateReviewAsync(review);
            return Ok(new { ReviewId = reviewId });
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<Review>>> GetProductReviews(int productId)
        {
            var reviews = await _reviewService.GetProductReviewsAsync(productId);
            return Ok(reviews);
        }
    }
}