using System.ComponentModel.DataAnnotations;
using StreamNest.Domain.Entities.Models;

namespace StreamNest.Domain.Entities.Models
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public Guid VideoId { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Video Video { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}