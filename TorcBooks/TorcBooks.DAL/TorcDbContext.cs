using Microsoft.EntityFrameworkCore;
using TorcBooks.DAL.Entities;

namespace TorcBooks.DAL
{
    public class TorcDbContext : DbContext
    {
        public TorcDbContext(DbContextOptions<TorcDbContext> options) : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
    }
}
