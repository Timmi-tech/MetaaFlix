using Microsoft.EntityFrameworkCore;
using StreamNest.Domain.Contracts;
using StreamNest.Domain.Entities.Models;

namespace StreamNest.Infrastructure.Repository
{
    public class RatingsRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void AddRatingAsync(Rating rating) => Create(rating);
        public void UpdateRatingAsync(Rating rating) => Update(rating);

        public async Task<Rating?> GetUserRatingAsync(Guid videoId, string userId, bool trackChanges)
        {
            return await FindByCondition(r => r.VideoId == videoId && r.UserId == userId, trackChanges).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Rating>> GetRatingsByVideoIdAsync(Guid videoId, bool trackChanges)
        {
            return await FindByCondition(r => r.VideoId == videoId, trackChanges).ToListAsync();
        }
    }
}