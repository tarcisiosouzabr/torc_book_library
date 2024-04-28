

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorcBooks.Data.Entities;

namespace TorcBooks.Integration.Tests.Models
{
    public class GetBooksResponse
    {
        public string bookTitle { get; set; }
        public string authors { get; set; }
        public string type { get; set; }
        public string isbn { get; set; }
        public string category { get; set; }
        public string avaliableCopies { get; set; }
    }
}
