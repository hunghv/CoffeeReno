using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;

namespace CoffeeReno.Controllers
{
    public class AdsFormsController : Controller
    {
        private readonly CoffeeRenoContext _context;

        public AdsFormsController(CoffeeRenoContext context)
        {
            _context = context;
        }

        // GET: AdsForms
        public async Task<IActionResult> Index()
        {
            var coffeeRenoContext = _context.AdsForms.Include(a => a.AdsType);
            return View(await coffeeRenoContext.ToListAsync());
        }

        // GET: AdsForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsForm = await _context.AdsForms
                .Include(a => a.AdsType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adsForm == null)
            {
                return NotFound();
            }

            return View(adsForm);
        }

        // GET: AdsForms/Create
        public IActionResult Create()
        {
            ViewData["AdsTypeId"] = new SelectList(_context.AdsTypes, "Id", "Name");
            return View();
        }

        // POST: AdsForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,AdsTypeId,Id,CreatedBy")] AdsForm adsForm)
        {
            if (ModelState.IsValid)
            {
                adsForm.CreatedDate = DateTime.Now;

                _context.Add(adsForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdsTypeId"] = new SelectList(_context.AdsTypes, "Id", "Name", adsForm.AdsTypeId);
            return View(adsForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialCreate(int adsTypeId, [Bind("Name,Description,Id,CreatedBy")] AdsForm adsForm)
        {
            if (ModelState.IsValid)
            {
                adsForm.CreatedDate = DateTime.Now;
                adsForm.AdsTypeId = adsTypeId;

                _context.Add(adsForm);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                //return to Edit view in AdsTypesController with the same AdsType selected to be edited
                return RedirectToAction("Edit", "AdsTypes", new { id = adsTypeId });
            }
            ViewData["AdsTypeId"] = new SelectList(_context.AdsTypes, "Id", "Name", adsForm.AdsTypeId);
            //return View(adsForm);

            //return to Edit view in AdsTypesController with the same AdsType selected to be edited
            return RedirectToAction("Edit", "AdsTypes", new { id = adsTypeId });
        }

        // GET: AdsForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsForm = await _context.AdsForms.FindAsync(id);
            if (adsForm == null)
            {
                return NotFound();
            }
            ViewData["AdsTypeId"] = new SelectList(_context.AdsTypes, "Id", "Name", adsForm.AdsTypeId);
            return View(adsForm);
        }

        // POST: AdsForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,AdsTypeId,Id,IsDeleted,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,DeletedDate,DeletedBy")] AdsForm adsForm)
        {
            if (id != adsForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adsForm.ModifiedDate = DateTime.Now;

                    _context.Update(adsForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsFormExists(adsForm.Id))
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
            ViewData["AdsTypeId"] = new SelectList(_context.AdsTypes, "Id", "Name", adsForm.AdsTypeId);
            return View(adsForm);
        }

        // GET: AdsForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsForm = await _context.AdsForms
                .Include(a => a.AdsType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adsForm == null)
            {
                return NotFound();
            }

            return View(adsForm);
        }

        // POST: AdsForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adsForm = await _context.AdsForms.FindAsync(id);
            _context.AdsForms.Remove(adsForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdsFormExists(int id)
        {
            return _context.AdsForms.Any(e => e.Id == id);
        }
    }
}
