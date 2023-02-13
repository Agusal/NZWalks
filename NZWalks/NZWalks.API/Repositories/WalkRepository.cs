using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository:IWalkRepository
    {
        private readonly NZWalksDBContext nNZWalksDBContext;

        public WalkRepository(NZWalksDBContext nNZWalksDBContext)
        {
            this.nNZWalksDBContext = nNZWalksDBContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //Assing new id
            walk.Id=Guid.NewGuid(); 
            await nNZWalksDBContext.AddAsync(walk);
            await nNZWalksDBContext.SaveChangesAsync(); 
            return walk;    
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nNZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return walk;
            }
            //Delete Region
            nNZWalksDBContext.Walks.Remove(walk);
            await nNZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            
            return await nNZWalksDBContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nNZWalksDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id ==id);   

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk=await nNZWalksDBContext.Walks.FindAsync(id);
            if (existingWalk!=null)
            {
                existingWalk.Id = id;
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId= walk.RegionId;
                await nNZWalksDBContext.SaveChangesAsync();

            }
            return null;
        }
    }   
}
