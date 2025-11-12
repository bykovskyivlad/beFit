using System.ComponentModel.DataAnnotations;

namespace beFit.Models
{
    public class ExerciseEntry
    {
        public int Id { get; set; }
        [Display(Name = "Exercise Type")]
        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }
        [Display(Name = "Training Session")]
        public int TrainingSessionId { get; set; }
        public virtual TrainingSession? TrainingSession { get; set; }
        public double Load { get; set; } 
        public TimeSpan Duration { get; set; } // duration of the exercise
    }
}
