namespace beFit.Models.ViewModels
{
    public class ExerciseStats
    {
        public string ExerciseName { get; set; }
        public int TotalSessions { get; set; }
        public int TotalReps { get; set; }
        public double AverageLoad { get; set; }
        public double MaxLoad { get; set; }
    }
}
