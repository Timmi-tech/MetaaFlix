using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamNest.Application.DTOs;
using StreamNest.Application.Services.Contracts;

namespace StreamNest.API.Controllers
{
    [Route("api/Rating")]
    [ApiController]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RatingController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateMovieAsync([FromBody] RateVideoDto dto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            await _service.RatingService.RateVideoAsync(userId,dto);
            return Ok(new { message = "Rating submitted" });
        }
        [HttpGet("{videoId}/average")]
        public async Task<IActionResult> GetAverageRatingAsync(Guid videoId)
        {
            var averageRating = await _service.RatingService.GetVideoAverageRatingAsync(videoId);
            return Ok(averageRating);
        }
        private string? GetUserIdFromClaims()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}