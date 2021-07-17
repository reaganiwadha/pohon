using System;
using Microsoft.EntityFrameworkCore;
using Pohon.Models;

namespace Pohon.Data
{
    public class PohonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GithubOAuthDetail> GithubOAuthDetails { get; set; }
        
        public PohonDbContext(DbContextOptions<PohonDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<GithubOAuthDetail>();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
    }
}