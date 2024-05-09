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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            ViewData["CodItem"] = new SelectList(_context.Items, "CodItem", "CodItem", detallePedido.CodItem);
            ViewData["CodPedido"] = new SelectList(_context.Pedidos, "CodPedido", "CodPedido", detallePedido.CodPedido);
            return View(detallePedido);
        }

        // POST: DetallePedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodItem,CodPedido,Cantidad,Direccion")] DetallePedido detallePedido)
        {
            if (id != detallePedido.CodItem)
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
                    if (!DetallePedidoExists(detallePedido.CodItem))
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
        public async Task<IActionResult> Delete(string id)
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

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallePedidos.Remove(detallePedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidoExists(string id)
        {
            return _context.DetallePedidos.Any(e => e.CodItem == id);
        }
    }
}
