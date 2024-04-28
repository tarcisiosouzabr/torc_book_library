using Microsoft.EntityFrameworkCore;
using TorcBooks.Data.Entities;

namespace TorcBooks.Data
{
    public class TorcDbContext : DbContext
    {
        public TorcDbContext(DbContextOptions<TorcDbContext> options) : base (options)
        {
            
        }
        public DbSet<Books> Books { get; set; }
    }
}
