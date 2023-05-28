using iTunes_WebApp_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace iTunes_WebApp_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Albums> Albums { get; set; }
        public DbSet<MusicVideos> MusicVideos { get; set; }
        public DbSet<Songs> Songs { get; set; }

    }
}
