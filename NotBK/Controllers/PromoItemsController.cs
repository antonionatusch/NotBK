﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotBK.Models;

namespace NotBK.Controllers
{
    public class PromoItemsController : Controller
    {
        private readonly NotBkContext _context;

        public PromoItemsController(NotBkContext context)
        {
            _context = context;
        }

        // GET: PromoItems
        public async Task<IActionResult> Index()
        {
            var notBkContext = _context.PromoItems.Include(p => p.CodItemNavigation).Include(p => p.CodPromoNavigation);
            return View(await notBkContext.ToListAsync());
        }

        // GET: PromoItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems
                .Include(p => p.CodItemNavigation)
                .Include(p => p.CodPromoNavigation)
                .FirstOrDefaultAsync(m => m.CodItem == id);
            if (promoItem == null)
            {
                return NotFound();
            }

            return View(promoItem);
        }

        // GET: PromoItems/Create
        public IActionResult Create()
        {
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "Nombre");
            ViewData["CodPromo"] = new SelectList(_context.Promocions, "CodPromo", "Nombre");
            return View();
        }

        // POST: PromoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodItem,CodPromo,FechaInicio,FechaFin")] PromoItem promoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", promoItem.CodItem);
            ViewData["CodPromo"] = new SelectList(_context.Promocions, "CodPromo", "CodPromo", promoItem.CodPromo);
            return View(promoItem);
        }

        // GET: PromoItems/Edit/5
        public async Task<IActionResult> Edit(string id, string id2)
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems.FindAsync(id, id2);
            if (promoItem == null)
            {
                return NotFound();
            }

            ViewBag.Id = id;
            ViewBag.Id2 = id2;

            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", promoItem.CodItem);
            ViewData["CodPromo"] = new SelectList(_context.Promocions, "CodPromo", "CodPromo", promoItem.CodPromo);
            return View(promoItem);
        }



        // POST: PromoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(string id, string id2, [Bind("CodItem,CodPromo,FechaInicio,FechaFin")] PromoItem promoItem)
        {
            if (id != promoItem.CodItem || id2 != promoItem.CodPromo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromoItemExists(promoItem.CodItem, promoItem.CodPromo))
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
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", promoItem.CodItem);
            ViewData["CodPromo"] = new SelectList(_context.Promocions, "CodPromo", "CodPromo", promoItem.CodPromo);
            return View(promoItem);
        }


        // GET: PromoItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoItem = await _context.PromoItems
                .Include(p => p.CodItemNavigation)
                .Include(p => p.CodPromoNavigation)
                .FirstOrDefaultAsync(m => m.CodItem == id);
            if (promoItem == null)
            {
                return NotFound();
            }

            return View(promoItem);
        }

        // POST: PromoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var promoItem = await _context.PromoItems.FindAsync(id);
            if (promoItem != null)
            {
                _context.PromoItems.Remove(promoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromoItemExists(string id)
        {
            return _context.PromoItems.Any(e => e.CodItem == id);
        }

        private bool PromoItemExists(string id, string id2)
        {
            return _context.PromoItems.Any(e => e.CodItem == id) && _context.PromoItems.Any(e => e.CodPromo == id2);
        }
    }
}