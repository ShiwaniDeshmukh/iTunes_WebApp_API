using iTunes_WebApp_API.Models;
using Microsoft.EntityFrameworkCore;

namespace iTunes_WebApp_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //public DbSet<Albums> Albums { get; set; }
        //public DbSet<MusicVideos> MusicVideos { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<ClickCount> ClickCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClickCount>()
                .HasOne(c => c.Track)
                .WithMany()
                .HasForeignKey(c => c.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
