using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Jobinator.Models;

namespace Jobinator.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public OfferRequest TypOfferRequest { get; set; }

        public JobCategory Category { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }

        //public User User { get; set; }

        public enum OfferRequest
        {
            Offer = 1,
            Request = 2
        }

        public enum JobCategory
        {
            IT = 1,
            Marketing = 2,
            Finance = 3
        }
    }
}
