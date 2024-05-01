using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorcBooks.Data.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("book_id ")]
        public int BookId { get; private set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; private set; }

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; private set; }

        [Required]
        [Column("total_copies")]
        public int TotalCopies { get; private set; }

        [Required]
        [Column("copies_in_use")]
        public int CopiesInUse { get; private set; }

        [MaxLength(50)]
        public string Type { get; private set; }

        [MaxLength(80)]
        public string ISBN { get; private set; }

        [MaxLength(50)]
        public string Category { get; private set; }

        public Books(string title, string firstName, string lastName, int totalCopies, int copiesInUse, string type, string ISBN, string category)
        {
            this.Title = title;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TotalCopies = totalCopies;
            this.CopiesInUse = copiesInUse;
            this.Type = type;
            this.ISBN = ISBN;
            this.Category = category;
            var validations = GetValidationResults();
            if(validations != null && validations.Any())
            {
                string?[] errorMessages = validations.Select(x => x.ErrorMessage).ToArray();
                throw new Exception(string.Join(" - ", errorMessages));
            }
        }

        //For EF Purposes
        protected Books()
        {
            
        }

        private IEnumerable<ValidationResult> GetValidationResults()
        {
            var validationResult = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, validationContext, validationResult, true);
            return validationResult;
        }
    }
}
