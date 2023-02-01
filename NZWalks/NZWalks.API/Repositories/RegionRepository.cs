using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nNZWalksDBContext;

        public RegionRepository(NZWalksDBContext nNZWalksDBContext)
        {
            this.nNZWalksDBContext = nNZWalksDBContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await nNZWalksDBContext.AddAsync(region);
            await nNZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nNZWalksDBContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (region==null )
            {
                return region;
            }
            //Delete Region
            nNZWalksDBContext.Regions.Remove(region);
            await nNZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            //return nNZWalksDBContext.Regions.ToList();
            //para convertir la api en asyncrono primero tenemos que obtener las Regions de la database (del Repository) de forma asyncronica para esto devemos cambiar el metodo
            //de Regions.Tolist a Regions.ToListAsync

            return await nNZWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
             return await nNZWalksDBContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nNZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code= region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat= region.Lat;
            existingRegion.Long= region.Long;
            existingRegion.Population= region.Population;

            await nNZWalksDBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
