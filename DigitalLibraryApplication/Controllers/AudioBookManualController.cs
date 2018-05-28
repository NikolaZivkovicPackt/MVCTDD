using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigitalLibraryApplication.Controllers
{
    public class AudioBookManualController : Controller
    {
        private IAudioBookServiceAsync _audioBookService;

        public AudioBookManualController(IAudioBookServiceAsync audioBookService)
        {
            _audioBookService = audioBookService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _audioBookService.GetAll());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Subtitle,Summary")] AudioBook audioBook)
        {
            if (ModelState.IsValid)
            {
                await _audioBookService.Add(audioBook);
                return RedirectToAction(nameof(Index));
            }
            return View(audioBook);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioBook = await _audioBookService.GetById(id.Value);
            if (audioBook == null)
            {
                return NotFound();
            }
            return View(audioBook);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Subtitle,Summary")] AudioBook audioBook)
        {
            if (audioBook == null || audioBook.Id != id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _audioBookService.Update(id, audioBook);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioBook = await _audioBookService.GetById(id.Value);
            if (audioBook == null)
            {
                return NotFound();
            }
            return View(audioBook);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _audioBookService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
