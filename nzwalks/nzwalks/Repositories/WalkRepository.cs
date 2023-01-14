using Microsoft.EntityFrameworkCore;
using nzwalks.Data;
using nzwalks.Models.Domain;

namespace nzwalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public WalkRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalkDbContext.AddAsync(walk);
            await nZWalkDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk=await nZWalkDbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            nZWalkDbContext.Walks.Remove(existingWalk);
            nZWalkDbContext.SaveChanges();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                nZWalkDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalkDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalkDbContext.Walks.FindAsync(id);
            if (existingWalk != null)
            {
                existingWalk.Length=walk.Length;
                existingWalk.Name=walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await nZWalkDbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
            
        }
    }
}
