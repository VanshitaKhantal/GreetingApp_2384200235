using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using RepositoryLayer.Context;
using System.Collections.Generic;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace RepositoryLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Greetings { get; set; } // Greeting Table
    }
}
