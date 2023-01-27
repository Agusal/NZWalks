using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepositorie : IRegionRepositorie
    {
        private readonly NZWalksDBContext nNZWalksDBContext;

        public RegionRepositorie(NZWalksDBContext nNZWalksDBContext)
        {
            this.nNZWalksDBContext = nNZWalksDBContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            //return nNZWalksDBContext.Regions.ToList();
            //para convertir la api en asyncrono primero tenemos que obtener las Regions de la database (del Repository) de forma asyncronica para esto devemos cambiar el metodo
            //de Regions.Tolist a Regions.ToListAsync

            return await nNZWalksDBContext.Regions.ToListAsync();
        }
    }
}
