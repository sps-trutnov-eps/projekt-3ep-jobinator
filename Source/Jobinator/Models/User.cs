using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Jobinator.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Jmeno { get; set; }
        [Required]
        public string? Heslo { get; set; }
        [AllowNull]
        public string? RequestId { get; set; }
        [AllowNull]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
