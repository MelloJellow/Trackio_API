using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackio_API.Controllers.Entities
{
    public enum ApplicationStatus
    {
        Pending, 
        Accepted, 
        Rejected
    }
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        [EnumDataType(typeof(ApplicationStatus))]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        //Navigate 
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [ForeignKey(nameof(JobId))]
        public Jobs? Job { get; set; }

    }
}
