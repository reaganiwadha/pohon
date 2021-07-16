using Microsoft.EntityFrameworkCore;

namespace Pohon.Data
{
    public class PohonDbContext : DbContext
    {
        public PohonDbContext(DbContextOptions<PohonDbContext> options) : base(options)
        {
            
        }   
    }
}