using Microsoft.EntityFrameworkCore;
using BarnCaseWebApi.Models;

namespace BarnCaseWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Barn> Barns { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}