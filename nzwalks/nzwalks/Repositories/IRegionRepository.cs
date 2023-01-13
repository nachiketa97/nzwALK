using nzwalks.Models.Domain;

namespace nzwalks.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync(); 
    }
}
