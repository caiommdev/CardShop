﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CardShop.Data;
using CardShop.Models;

namespace CardShop.Controllers
{
    public class CardsController : Controller
    {
        private readonly CardShopContext _context;

        public CardsController(CardShopContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
              return _context.Card != null ? 
                          View(await _context.Card.ToListAsync()) :
                          Problem("Entity set 'CardShopContext.Card'  is null.");
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Game")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Game")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Card == null)
            {
                return Problem("Entity set 'CardShopContext.Card'  is null.");
            }
            var card = await _context.Card.FindAsync(id);
            if (card != null)
            {
                _context.Card.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
          return (_context.Card?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
