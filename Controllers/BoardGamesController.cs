using System;
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
    public class BoardGamesController : Controller
    {
        private readonly CardShopContext _context;

        public BoardGamesController(CardShopContext context)
        {
            _context = context;
        }

        // GET: BoardGames
        public async Task<IActionResult> Index()
        {
              return _context.BoardGame != null ? 
                          View(await _context.BoardGame.ToListAsync()) :
                          Problem("Entity set 'CardShopContext.BoardGame'  is null.");
        }

        // GET: BoardGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BoardGame == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGame
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // GET: BoardGames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoardGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Game")] BoardGame boardGame)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardGame);
        }

        // GET: BoardGames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BoardGame == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGame.FindAsync(id);
            if (boardGame == null)
            {
                return NotFound();
            }
            return View(boardGame);
        }

        // POST: BoardGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Game")] BoardGame boardGame)
        {
            if (id != boardGame.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameExists(boardGame.Id))
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
            return View(boardGame);
        }

        // GET: BoardGames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BoardGame == null)
            {
                return NotFound();
            }

            var boardGame = await _context.BoardGame
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: BoardGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BoardGame == null)
            {
                return Problem("Entity set 'CardShopContext.BoardGame'  is null.");
            }
            var boardGame = await _context.BoardGame.FindAsync(id);
            if (boardGame != null)
            {
                _context.BoardGame.Remove(boardGame);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardGameExists(int id)
        {
          return (_context.BoardGame?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
