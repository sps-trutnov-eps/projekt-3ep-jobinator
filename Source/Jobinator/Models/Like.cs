using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jobinator.Models
{
    public class Like
    {
        public int Id { get; set; }

        // liker
        public int LikerId { get; set; }

        // receiver
        public int LikedUserId { get; set; }
    }
}
