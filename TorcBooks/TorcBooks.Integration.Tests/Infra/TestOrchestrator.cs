using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorcBooks.Data;
using TorcBooks.Data.Entities;

namespace TorcBooks.Integration.Tests.Infra
{
    public class TestOrchestrator
    {
        public static void ClearDatabase(IServiceCollection services)
        {
            var dbContextService = services.Where(x => x.ServiceType == typeof(TorcDbContext)).FirstOrDefault();
            if (dbContextService != null)
            {
                TorcDbContext? dbContext = dbContextService.ImplementationInstance as TorcDbContext;
                dbContext?.Database.EnsureCreated();
                dbContext?.Books.ExecuteDelete();
                var booksToAdd = new List<Books>
                        {
                            new Books { Title = "Pride and Prejudice", FirstName = "Jane", LastName = "Austen", TotalCopies = 100, CopiesInUse = 80, Type = "Hardcover", ISBN = "1234567891", Category = "Fiction" },
                            new Books { Title = "To Kill a Mockingbird", FirstName = "Harper", LastName = "Lee", TotalCopies = 75, CopiesInUse = 65, Type = "Paperback", ISBN = "1234567892", Category = "Fiction" },
                            new Books { Title = "The Catcher in the Rye", FirstName = "J.D.", LastName = "Salinger", TotalCopies = 50, CopiesInUse = 45, Type = "Hardcover", ISBN = "1234567893", Category = "Fiction" },
                            new Books { Title = "The Great Gatsby", FirstName = "F. Scott", LastName = "Fitzgerald", TotalCopies = 50, CopiesInUse = 22, Type = "Hardcover", ISBN = "1234567894", Category = "Non-Fiction" },
                            new Books { Title = "The Alchemist", FirstName = "Paulo", LastName = "Coelho", TotalCopies = 50, CopiesInUse = 35, Type = "Hardcover", ISBN = "1234567895", Category = "Biography" },
                            new Books { Title = "The Book Thief", FirstName = "Markus", LastName = "Zusak", TotalCopies = 75, CopiesInUse = 11, Type = "Hardcover", ISBN = "1234567896", Category = "Mystery" },
                            new Books { Title = "The Chronicles of Narnia", FirstName = "C.S.", LastName = "Lewis", TotalCopies = 100, CopiesInUse = 14, Type = "Paperback", ISBN = "1234567897", Category = "Sci-Fi" },
                            new Books { Title = "The Da Vinci Code", FirstName = "Dan", LastName = "Brown", TotalCopies = 50, CopiesInUse = 40, Type = "Paperback", ISBN = "1234567898", Category = "Sci-Fi" },
                            new Books { Title = "The Grapes of Wrath", FirstName = "John", LastName = "Steinbeck", TotalCopies = 50, CopiesInUse = 35, Type = "Hardcover", ISBN = "1234567899", Category = "Fiction" },
                            new Books { Title = "The Hitchhiker's Guide to the Galaxy", FirstName = "Douglas", LastName = "Adams", TotalCopies = 50, CopiesInUse = 35, Type = "Paperback", ISBN = "1234567900", Category = "Non-Fiction" },
                            new Books { Title = "Moby-Dick", FirstName = "Herman", LastName = "Melville", TotalCopies = 30, CopiesInUse = 8, Type = "Hardcover", ISBN = "8901234567", Category = "Fiction" },
                            new Books { Title = "To Kill a Mockingbird", FirstName = "Harper", LastName = "Lee", TotalCopies = 20, CopiesInUse = 0, Type = "Paperback", ISBN = "9012345678", Category = "Non-Fiction" },
                            new Books { Title = "The Catcher in the Rye", FirstName = "J.D.", LastName = "Salinger", TotalCopies = 10, CopiesInUse = 1, Type = "Hardcover", ISBN = "0123456789", Category = "Non-Fiction" }
                        };
                dbContext?.Books.AddRange(booksToAdd);
                dbContext?.SaveChanges();
            }
        }
    }
}
