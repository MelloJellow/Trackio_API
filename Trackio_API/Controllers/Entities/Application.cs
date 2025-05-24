using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackio_API.Controllers.Entities
{
    
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int JobId { get; set; }

        public string Status { get; set; } = "Pending";

        //Navigate 
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [ForeignKey(nameof(JobId))]
        public Jobs? Job { get; set; }

    }
}
