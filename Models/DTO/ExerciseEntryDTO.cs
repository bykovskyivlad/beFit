using System.ComponentModel.DataAnnotations;

namespace beFit.Models.DTO
{
    public class ExerciseEntryDTO
    {
        [Required]
        public int ExerciseTypeId { get; set; }

        [Required]
        public int TrainingSessionId { get; set; }

        [Range(0, 1000)]
        public double Load { get; set; }

        [Range(1, 20)]
        public int Sets { get; set; }

        [Range(1, 100)]
        public int Reps { get; set; }
    }
}

