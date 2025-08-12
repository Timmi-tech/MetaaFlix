namespace StreamNest.Application.DTOs
{
    public record RateVideoDto
    {
        public Guid VideoId { get; init; }
        public int Score { get; init; }
    }
    public record VideoRatingResponseDto
    {
        public double AverageRating { get; init; }
        public int TotalRatings { get; init; }
    }

}