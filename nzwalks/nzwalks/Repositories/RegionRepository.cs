using Microsoft.EntityFrameworkCore;
using nzwalks.Data;
using nzwalks.Models.Domain;

namespace nzwalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public RegionRepository(NZWalkDbContext nZWalkDbContext)
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id= Guid.NewGuid();
            await nZWalkDbContext.AddAsync(region);
            await nZWalkDbContext.SaveChangesAsync();
            return region;

            
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region= await nZWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           if (region == null)
            {
                return null;
            }
           //delete the region
           nZWalkDbContext.Regions.Remove(region);
           await nZWalkDbContext.SaveChangesAsync();
           return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalkDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalkDbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion= await nZWalkDbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code= region.Code;
            existingRegion.Name= region.Name;
            existingRegion.Population= region.Population;
            existingRegion.Lat= region.Lat;
            existingRegion.Long= region.Long;
            existingRegion.Area= region.Area;
            await nZWalkDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
