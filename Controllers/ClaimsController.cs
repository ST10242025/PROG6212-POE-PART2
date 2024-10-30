using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.SignalR; 
using CMCS.Hubs;
using Microsoft.AspNet.SignalR;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ClaimHub> _hubContext;

        public ClaimsController(ApplicationDbContext context, IHubContext<ClaimHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext; 
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            return View(await _context.Claims.ToListAsync());
        }

        public async Task<IActionResult> Status()
        {
            return View(await _context.Claims.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.id == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Surname,Email,HoursWorked,HourlyRate,ClaimDate,Description,Documentation")] Claims claims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claims);
                await _context.SaveChangesAsync();
                return RedirectToAction("Submit");
            }
            return View(claims);
        }

        public IActionResult Submit()
        {
            return View();
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims.FindAsync(id);
            if (claims == null)
            {
                return NotFound();
            }
            return View(claims);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Surname,Email,HoursWorked,HourlyRate,ClaimDate,Description,Documentation")] Claims claims)
        {
            if (id != claims.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimsExists(claims.id))
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
            return View(claims);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claims = await _context.Claims
                .FirstOrDefaultAsync(m => m.id == id);
            if (claims == null)
            {
                return NotFound();
            }

            return View(claims);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claims = await _context.Claims.FindAsync(id);
            if (claims != null)
            {
                _context.Claims.Remove(claims);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimsExists(int id)
        {
            return _context.Claims.Any(e => e.id == id);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                claim.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                // Notify clients about the status change
                await NotifyClients(claim);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                // Notify clients about the status change
                await NotifyClients(claim);
            }
            return RedirectToAction("Index");
        }

        private async Task NotifyClients(Claims claim)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ClaimHub>();
            await hubContext.Clients.All.SendAsync("ReceiveClaimUpdate", claim);
        }
    }
}