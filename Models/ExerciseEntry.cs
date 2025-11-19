using System.ComponentModel.DataAnnotations;

namespace beFit.Models
{
    public class ExerciseEntry
    {
        public int Id { get; set; }
        [Display(Name = "Exercise Type")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }
        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }
        public virtual TrainingSession? TrainingSession { get; set; }

        [Range(0, 1000,ErrorMessage = "Load must be between 0 and 1000")]
        [Display(Name = "Load (kg)")]
        public double Load { get; set; }

        [Range(1, 20, ErrorMessage = "Sets must be between 1 and 20")]
        [Display(Name = "Sets")]
        public int Sets { get; set; }
        [Range(1, 100, ErrorMessage = "Repetitions must be between 1 and 100")]
        [Display(Name = "Reps in set")]
        public int Reps { get; set; }

       
    }
}
