using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TorcBooks.Integration
{
    public class CreateBookEvent
    {
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int TotalCopies { get; private set; }
        public int CopiesInUse { get; private set; }
        public string Type { get; private set; }
        public string ISBN { get; private set; }
        public string Category { get; private set; }
    }
}
