using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

using Microsoft.AspNetCore.Authorization; //Added for access to [Authorize]   
using System.Drawing.Drawing2D;
using System.Drawing; //Added for access to the Image class



namespace StoreFront.UI.MVC.Controllers
{
    public class ProductStockStatusController : Controller
    {
        private readonly StoreFrontContext _context;

        public ProductStockStatusController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: ProductStockStatus
        public async Task<IActionResult> Index()
        {
              return View(await _context.ProductStockStatuses.ToListAsync());
        }

        // GET: ProductStockStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductStockStatuses == null)
            {
                return NotFound();
            }

            var productStockStatus = await _context.ProductStockStatuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (productStockStatus == null)
            {
                return NotFound();
            }

            return View(productStockStatus);
        }

        // GET: ProductStockStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductStockStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,LimitedStock,ReStocking,Discontinued")] ProductStockStatus productStockStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productStockStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productStockStatus);
        }

        // GET: ProductStockStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductStockStatuses == null)
            {
                return NotFound();
            }

            var productStockStatus = await _context.ProductStockStatuses.FindAsync(id);
            if (productStockStatus == null)
            {
                return NotFound();
            }
            return View(productStockStatus);
        }

        // POST: ProductStockStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,LimitedStock,ReStocking,Discontinued")] ProductStockStatus productStockStatus)
        {
            if (id != productStockStatus.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productStockStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductStockStatusExists(productStockStatus.StatusId))
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
            return View(productStockStatus);
        }

        // GET: ProductStockStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductStockStatuses == null)
            {
                return NotFound();
            }

            var productStockStatus = await _context.ProductStockStatuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (productStockStatus == null)
            {
                return NotFound();
            }

            return View(productStockStatus);
        }

        // POST: ProductStockStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductStockStatuses == null)
            {
                return Problem("Entity set 'StoreFrontContext.ProductStockStatuses'  is null.");
            }
            var productStockStatus = await _context.ProductStockStatuses.FindAsync(id);
            if (productStockStatus != null)
            {
                _context.ProductStockStatuses.Remove(productStockStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductStockStatusExists(int id)
        {
          return _context.ProductStockStatuses.Any(e => e.StatusId == id);
        }
    }
}
