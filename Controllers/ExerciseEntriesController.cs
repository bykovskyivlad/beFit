using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using beFit.Data;
using beFit.Models;

namespace beFit.Controllers
{
    public class ExerciseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExerciseEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExerciseEntries.Include(e => e.ExerciseType).Include(e => e.TrainingSession);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExerciseEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Create
        public IActionResult Create()
        {
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "StartTime");
            return View();
        }

        // POST: ExerciseEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExerciseTypeId,TrainingSessionId,Load,Sets,Reps")] ExerciseEntry exerciseEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exerciseEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "StartTime", exerciseEntry.TrainingSessionId);
            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries.FindAsync(id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "StartTime", exerciseEntry.TrainingSessionId);
            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExerciseTypeId,TrainingSessionId,Load,Sets,Reps")] ExerciseEntry exerciseEntry)
        {
            if (id != exerciseEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseEntryExists(exerciseEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions, "Id", "StartTime", exerciseEntry.TrainingSessionId);
            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseEntry = await _context.ExerciseEntries.FindAsync(id);
            if (exerciseEntry != null)
            {
                _context.ExerciseEntries.Remove(exerciseEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseEntryExists(int id)
        {
            return _context.ExerciseEntries.Any(e => e.Id == id);
        }
    }
}
