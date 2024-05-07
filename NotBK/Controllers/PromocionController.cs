using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotBK.Models;

namespace NotBK.Controllers
{
    public class PromocionController : Controller
    {
        private readonly NotBkContext _context;

        public PromocionController(NotBkContext context)
        {
            _context = context;
        }

        // GET: Promocion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promocions.ToListAsync());
        }

        // GET: Promocion/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocions
                .FirstOrDefaultAsync(m => m.CodPromo == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // GET: Promocion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promocion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodPromo,Nombre,Descuento")] Promocion promocion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocion);
        }

        // GET: Promocion/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocions.FindAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }
            return View(promocion);
        }

        // POST: Promocion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodPromo,Nombre,Descuento")] Promocion promocion)
        {
            if (id != promocion.CodPromo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocionExists(promocion.CodPromo))
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
            return View(promocion);
        }

        // GET: Promocion/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocion = await _context.Promocions
                .FirstOrDefaultAsync(m => m.CodPromo == id);
            if (promocion == null)
            {
                return NotFound();
            }

            return View(promocion);
        }

        // POST: Promocion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var promocion = await _context.Promocions.FindAsync(id);
            if (promocion != null)
            {
                _context.Promocions.Remove(promocion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocionExists(string id)
        {
            return _context.Promocions.Any(e => e.CodPromo == id);
        }
    }
}
