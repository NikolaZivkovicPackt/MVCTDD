using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalLibraryApplication.Models;

namespace DigitalLibraryApplication.Controllers
{
    public class AudioBooksController : Controller
    {
        private readonly DigitalLibraryContext _context;

        public AudioBooksController(DigitalLibraryContext context)
        {
            _context = context;
        }

        // GET: AudioBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AudioBooks.ToListAsync());
        }

        // GET: AudioBooks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioBook = await _context.AudioBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (audioBook == null)
            {
                return NotFound();
            }

            return View(audioBook);
        }

        // GET: AudioBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudioBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Subtitle,Summary")] AudioBook audioBook)
        {
            if (ModelState.IsValid)
            {
                audioBook.Id = Guid.NewGuid();
                _context.Add(audioBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audioBook);
        }

        // GET: AudioBooks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioBook = await _context.AudioBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (audioBook == null)
            {
                return NotFound();
            }
            return View(audioBook);
        }

        // POST: AudioBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Subtitle,Summary")] AudioBook audioBook)
        {
            if (id != audioBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audioBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioBookExists(audioBook.Id))
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
            return View(audioBook);
        }

        // GET: AudioBooks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioBook = await _context.AudioBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (audioBook == null)
            {
                return NotFound();
            }

            return View(audioBook);
        }

        // POST: AudioBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var audioBook = await _context.AudioBooks.SingleOrDefaultAsync(m => m.Id == id);
            _context.AudioBooks.Remove(audioBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioBookExists(Guid id)
        {
            return _context.AudioBooks.Any(e => e.Id == id);
        }
    }
}
