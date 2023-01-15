using Microsoft.EntityFrameworkCore;
using nzwalks.Data;
using nzwalks.Models.Domain;

namespace nzwalks.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public WalkDifficultyRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalkDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalkDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWalkDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                nZWalkDbContext.WalkDifficulty.Remove(existingWalkDifficulty);
                await nZWalkDbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalkDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalkDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalkDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }
            existingWalkDifficulty.Code=walkDifficulty.Code;
            await nZWalkDbContext.SaveChangesAsync();
            return existingWalkDifficulty;


        }
    }
}
