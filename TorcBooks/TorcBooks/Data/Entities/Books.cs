using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorcBooks.Data.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("book_id ")]
        public int BookId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("total_copies")]
        public int TotalCopies { get; set; }

        [Required]
        [Column("copies_in_use")]
        public int CopiesInUse { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [MaxLength(80)]
        public string ISBN { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }
    }
}
