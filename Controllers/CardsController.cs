using CardShop.Data;
using CardShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Controllers
{
    public class CardsController : Controller
    {
        private readonly CardShopContext _context;
        private readonly IWebHostEnvironment _webHostEnv;

        public CardsController(CardShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnv = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Card != null ? 
                          View(await _context.Card.ToListAsync()) :
                          Problem("Entity set 'CardShopContext.Card'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Card == null)
                return NotFound();

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
                return NotFound();

            return View(card);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name,Game,CreationDate,ImageFile,Upload")] Card card)
        {
            if(!ModelState.IsValid)
                return View(card);
            
            if (card.Upload is not null)
            {
                card.ImageFileName = card.Upload.FileName;
                var file = Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/images", card.ImageFileName);
               
                using (var fileStream = new FileStream(file, FileMode.Create))
                    card.Upload.CopyTo(fileStream);
            }
            card.CreationDate = DateTime.Now;
            _context.Add(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Card == null)
                return NotFound();

            var card = await _context.Card.FindAsync(id);
            if (card == null)
                return NotFound();

            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name,Game,CreationDate,ImageFile,Upload")] Card card)
        {
            if (id != card.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (card.Upload is not null)
                    {
                        card.ImageFileName = card.Upload.FileName;
                        var file = Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/images", card.ImageFileName);

                        using (var fileStream = new FileStream(file, FileMode.Create))
                            card.Upload.CopyTo(fileStream);
                    }

                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Card == null)
                return NotFound();

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
                return NotFound();

            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Card == null)
                return Problem("Entity set 'CardShopContext.Card'  is null.");

            var card = await _context.Card.FindAsync(id);
            if (card != null)
                _context.Card.Remove(card);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
          return (_context.Card?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
