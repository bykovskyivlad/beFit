using beFit.Data;
using beFit.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beFit.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var monthAgo = DateTime.Now.AddDays(-28);

            var stats = _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Where(e => e.TrainingSession.StartTime >= monthAgo)
                .GroupBy(e => e.ExerciseType.Name)
                .Select(g => new ExerciseStats
                {
                    ExerciseName = g.Key,
                    TotalSessions = g.Count(),
                    TotalReps = g.Sum(x => x.Sets * x.Reps),
                    AverageLoad = g.Average(x => x.Load),
                    MaxLoad = g.Max(x => x.Load)
                })
                .ToList();

            return View(stats);
        }

    }
}
