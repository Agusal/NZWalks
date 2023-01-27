using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepositorie
    {
       Task<IEnumerable<Region>> GetAllAsync();
    }
}
