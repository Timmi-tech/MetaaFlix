using StreamNest.Application.DTOs;

namespace StreamNest.Application.Services.Contracts
{
    public interface IRatingService
    {
        Task RateVideoAsync(string userId, RateVideoDto dto);
        Task<VideoRatingResponseDto> GetVideoAverageRatingAsync(Guid videoId);
    }
}