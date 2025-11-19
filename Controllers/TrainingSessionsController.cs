using beFit.Data;
using beFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beFit.Controllers
{
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TrainingSessionsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User);
        }

        // GET: TrainingSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingSessions.ToListAsync());
        }

        // GET: TrainingSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        // GET: TrainingSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(beFit.Models.DTO.TrainingSessionDTO dto)
        {
            if (ModelState.IsValid)
            {
                var trainingSession = new TrainingSession
                {
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    UserId = GetUserId()
                };

                _context.Add(trainingSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // GET: TrainingSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingSession = await _context.TrainingSessions.FindAsync(id);
            if (trainingSession == null)
            {
                return NotFound();
            }
            return View(trainingSession);
        }

        // POST: TrainingSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime")] TrainingSession trainingSession)
        {
            if (id != trainingSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingSessionExists(trainingSession.Id))
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
            return View(trainingSession);
        }

        // GET: TrainingSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        // POST: TrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingSession = await _context.TrainingSessions.FindAsync(id);
            if (trainingSession != null)
            {
                _context.TrainingSessions.Remove(trainingSession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSessions.Any(e => e.Id == id);
        }
    }
}
