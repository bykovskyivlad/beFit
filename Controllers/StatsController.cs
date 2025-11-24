using beFit.Data;
using beFit.Models.ViewModels;
using beFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StatsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private string GetUserId()
        {
            return _userManager.GetUserId(User);
        }

        public IActionResult Index()
        {
            var userId = GetUserId();
            var monthAgo = DateTime.Now.AddDays(-28);

            var stats = _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == userId)
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
