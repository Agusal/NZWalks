using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkDifficultyRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id= Guid.NewGuid();
            await nZWalksDBContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDBContext.WalkDifficulty.ToListAsync();

        }
        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        { 
            var existingWalkDiffculty = await nZWalksDBContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDiffculty == null)
            {
                return null;

            }
            existingWalkDiffculty.Code = walkDifficulty.Code;
            await nZWalksDBContext.SaveChangesAsync();
            return existingWalkDiffculty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDiffculty = await nZWalksDBContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDiffculty != null)
            {
                nZWalksDBContext.WalkDifficulty.Remove(existingWalkDiffculty);
                await nZWalksDBContext.SaveChangesAsync();
                return existingWalkDiffculty;   

            }
            return null;
        }
    }
}
