using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskboard.API.Models;
namespace Taskboard.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
    }
}
