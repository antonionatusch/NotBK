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
    public class DetallePedidosController : Controller
    {
        private readonly NotBkContext _context;

        public DetallePedidosController(NotBkContext context)
        {
            _context = context;
        }

        // GET: DetallePedidos
        public async Task<IActionResult> Index()
        {
            var notBkContext = _context.DetallePedidos.Include(d => d.CodItemNavigation).Include(d => d.CodPedidoNavigation);
            return View(await notBkContext.ToListAsync());
        }

        // GET: DetallePedidos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.CodItemNavigation)
                .Include(d => d.CodPedidoNavigation)
                .FirstOrDefaultAsync(m => m.CodItem == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // GET: DetallePedidos/Create
        public IActionResult Create()
        {
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem");
            ViewData["CodPedido"] = new SelectList(_context.Pedidos, "CodPedido", "CodPedido");
            return View();
        }

        // POST: DetallePedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodItem,CodPedido,Cantidad,Direccion")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", detallePedido.CodItem);
            ViewData["CodPedido"] = new SelectList(_context.Pedidos, "CodPedido", "CodPedido", detallePedido.CodPedido);
            return View(detallePedido);
        }

        // GET: DetallePedidos/Edit/5
        public async Task<IActionResult> Edit(string id, string id2)
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos.FindAsync(id, id2);
            if (detallePedido == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            ViewBag.Id2 = id2;

            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", detallePedido.CodItem);
            ViewData["CodPedido"] = new SelectList(_context.Pedidos, "CodPedido", "CodPedido", detallePedido.CodPedido);
            return View(detallePedido);
        }

        // POST: DetallePedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string id2, [Bind("CodItem,CodPedido,Cantidad,Direccion")] DetallePedido detallePedido)
        {
            if (id != detallePedido.CodItem || id2 != detallePedido.CodPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidoExists(detallePedido.CodItem, detallePedido.CodPedido))
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
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", detallePedido.CodItem);
            ViewData["CodPedido"] = new SelectList(_context.Pedidos, "CodPedido", "CodPedido", detallePedido.CodPedido);
            return View(detallePedido);
        }

        // GET: DetallePedidos/Delete/5
        public async Task<IActionResult> Delete(string id, string id2)
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos
                .Include(d => d.CodItemNavigation)
                .Include(d => d.CodPedidoNavigation)
                .FirstOrDefaultAsync(m => m.CodItem == id && m.CodPedido == id2);


            ViewBag.Id = id;
            ViewBag.Id2 = id2;

            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string id2)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id, id2);
            if (detallePedido != null)
            {
                _context.DetallePedidos.Remove(detallePedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool DetallePedidoExists(string id, string id2)
        {
            return _context.DetallePedidos.Any(e => e.CodItem == id && e.CodPedido == id2);
        }
    }
}
