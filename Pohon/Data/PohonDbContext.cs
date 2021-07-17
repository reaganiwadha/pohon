using Microsoft.EntityFrameworkCore;
using Pohon.Migrations;
using Pohon.Models;

namespace Pohon.Data
{
    public class PohonDbContext : DbContext
    {
        public PohonDbContext(DbContextOptions<PohonDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<GithubOAuthDetail>();
        }
    }
}