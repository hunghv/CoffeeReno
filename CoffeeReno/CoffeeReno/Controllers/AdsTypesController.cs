using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;

namespace CoffeeReno.Controllers
{
    public class AdsTypesController : Controller
    {
        private readonly CoffeeRenoContext _context;

        public AdsTypesController(CoffeeRenoContext context)
        {
            _context = context;
        }

        // GET: AdsTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdsTypes.ToListAsync());
        }

        // GET: AdsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsType = await _context.AdsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adsType == null)
            {
                return NotFound();
            }

            return View(adsType);
        }

        // GET: AdsTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id,CreatedBy")] AdsType adsType)
        {
            if (ModelState.IsValid)
            {
                adsType.CreatedDate = DateTime.Now;

                _context.Add(adsType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adsType);
        }

        // GET: AdsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsType = await _context.AdsTypes.FindAsync(id);
            if (adsType == null)
            {
                return NotFound();
            }

            //support partial view _AdsFormPartial_Index.cshtml
            adsType.AdsFroms = _context.AdsForms?.Where(x => x.AdsTypeId == id)?.ToList();
            return View(adsType);
        }

        // POST: AdsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id,IsDeleted,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,DeletedDate,DeletedBy")] AdsType adsType)
        {
            if (id != adsType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adsType.ModifiedDate = DateTime.Now;

                    _context.Update(adsType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsTypeExists(adsType.Id))
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
            return View(adsType);
        }

        // GET: AdsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsType = await _context.AdsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adsType == null)
            {
                return NotFound();
            }

            return View(adsType);
        }

        // POST: AdsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adsType = await _context.AdsTypes.FindAsync(id);
            _context.AdsTypes.Remove(adsType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdsTypeExists(int id)
        {
            return _context.AdsTypes.Any(e => e.Id == id);
        }
    }
}
