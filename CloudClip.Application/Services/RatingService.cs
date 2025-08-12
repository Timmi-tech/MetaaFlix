using StreamNest.Application.DTOs;
using StreamNest.Application.Services.Contracts;
using StreamNest.Domain.Contracts;
using StreamNest.Domain.Entities.Models;

namespace StreamNest.Application.Services
{
    internal class RatingService : IRatingService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public RatingService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task RateVideoAsync(string userId, RateVideoDto dto)
        {
            var existingRating = await _repository.Rating.GetUserRatingAsync(dto.VideoId, userId, trackChanges: false);

            if (existingRating != null)
            {
                existingRating.Score = dto.Score;
                existingRating.UpdatedAt = DateTime.UtcNow;
                _repository.Rating.UpdateRatingAsync(existingRating);
            }
            else
            {
                var video = await _repository.Video.GetVideoByIdAsync(dto.VideoId, trackChanges: false);
                var user = await _repository.User.GetUserProfileAsync(userId, trackChanges: false);

                var rating = new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    VideoId = dto.VideoId,
                    Score = dto.Score,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _repository.Rating.AddRatingAsync(rating);
            }

            await _repository.SaveAsync();
        }
        public async Task<VideoRatingResponseDto> GetVideoAverageRatingAsync(Guid videoId)
        {
            var ratings = await _repository.Rating.GetRatingsByVideoIdAsync(videoId, trackChanges: false);
            if (!ratings.Any())
                return new VideoRatingResponseDto { AverageRating = 0, TotalRatings = 0 };

            var average = ratings.Average(r => r.Score);
            return new VideoRatingResponseDto
            {
                AverageRating = Math.Round(average, 2),
                TotalRatings = ratings.Count()
            };
        }
    }
}