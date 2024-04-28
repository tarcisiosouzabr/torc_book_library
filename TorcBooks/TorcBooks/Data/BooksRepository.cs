using Microsoft.EntityFrameworkCore;
using TorcBooks.Data.Entities;

namespace TorcBooks.Data
{
    public class BooksRepository
    {
        private readonly TorcDbContext _dbContext;
        public BooksRepository(TorcDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Books>> GetAsync(string searchBy, string searchValue)
        {
            var query = _dbContext.Books.AsQueryable();
            switch (searchBy)
            {
                case "title":
                    query = query.Where(x => x.Title.Contains(searchValue));
                    break;
                case "type":
                    query = query.Where(x => x.Type.Contains(searchValue));
                    break;
                case "isbn":
                    query = query.Where(x => x.ISBN.Contains(searchValue));
                    break;
                case "category":
                    query = query.Where(x => x.Category.Contains(searchValue));
                    break;
                case "authors":
                    query = query.Where(x => x.FirstName.Contains(searchValue) || x.LastName.Contains(searchValue));
                    break;
                default:
                    query = query.Where(x => x.Title.Contains(searchValue));
                    break;
            }
            return query.ToListAsync();
        }
    }
}
