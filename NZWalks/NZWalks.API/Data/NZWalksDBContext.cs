using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Diagnostics;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> options): base (options)     
        {

        }

        public  DbSet<Region>  Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }




    }
}
