using System.ComponentModel.DataAnnotations;

namespace beFit.Models.DTO
{
    public class TrainingSessionDTO
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
