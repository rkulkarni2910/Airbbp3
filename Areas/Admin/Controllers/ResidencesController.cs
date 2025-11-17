using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBB.Models;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidencesController : Controller
    {
        private readonly AirBBContext _context;

        public ResidencesController(AirBBContext context)
        {
            _context = context;
        }

        // GET: Admin/Residences
        public async Task<IActionResult> Index()
        {
            var residences = await _context.Residences
                .Include(r => r.Location)
                .Include(r => r.Owner)
                .ToListAsync();
            return View(residences);
        }

        // GET: Admin/Residences/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Locations = await _context.Locations.ToListAsync();
            ViewBag.Owners = await _context.Users.Where(u => u.UserType == UserType.Owner).ToListAsync();
            return View();
        }

        // POST: Admin/Residences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidenceId,Name,ResidencePicture,LocationId,OwnerId,GuestNumber,BedroomNumber,BathroomNumber,BuiltYear,PricePerNight")] Residence residence)
        {
            // Log ModelState errors for debugging
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(residence);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Residence created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during Create: {ex.Message}");
                    ModelState.AddModelError("", $"Error creating residence: {ex.Message}");
                }
            }

            ViewBag.Locations = await _context.Locations.ToListAsync();
            ViewBag.Owners = await _context.Users.Where(u => u.UserType == UserType.Owner).ToListAsync();
            return View(residence);
        }

        // GET: Admin/Residences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }

            ViewBag.Locations = await _context.Locations.ToListAsync();
            ViewBag.Owners = await _context.Users.Where(u => u.UserType == UserType.Owner).ToListAsync();
            return View(residence);
        }

        // POST: Admin/Residences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResidenceId,Name,ResidencePicture,LocationId,OwnerId,GuestNumber,BedroomNumber,BathroomNumber,BuiltYear,PricePerNight")] Residence residence)
        {
            if (id != residence.ResidenceId)
            {
                return NotFound();
            }

            // Log ModelState errors for debugging
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residence);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Residence updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceExists(residence.ResidenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during Edit: {ex.Message}");
                    ModelState.AddModelError("", $"Error updating residence: {ex.Message}");
                }
            }

            ViewBag.Locations = await _context.Locations.ToListAsync();
            ViewBag.Owners = await _context.Users.Where(u => u.UserType == UserType.Owner).ToListAsync();
            return View(residence);
        }

        // GET: Admin/Residences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences
                .Include(r => r.Location)
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(m => m.ResidenceId == id);
            if (residence == null)
            {
                return NotFound();
            }

            return View(residence);
        }

        // POST: Admin/Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence != null)
            {
                _context.Residences.Remove(residence);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Residence deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceExists(int id)
        {
            return _context.Residences.Any(e => e.ResidenceId == id);
        }
    }
}