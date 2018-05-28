using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLibraryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryApplication.Services
{
    public class AudioBookServiceAsync : IAudioBookServiceAsync
    {
        private DigitalLibraryContext _context;

        public AudioBookServiceAsync(DigitalLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AudioBook>> GetAll()
        {
            return await _context.AudioBooks
                .Include(x => x.Author)
                .Include(x => x.Narator)
                .Include(x => x.Publisher)
                .ToListAsync();
        }

        public async Task<AudioBook> GetById(Guid id)
        {
            return await _context.AudioBooks
                .Include(x => x.Author)
                .Include(x => x.Narator)
                .Include(x => x.Publisher)
                .SingleAsync(x => x.Id == id);
        }

        public async Task<AudioBook> Add(AudioBook audioBook)
        {
            audioBook.Id = Guid.NewGuid();

            var audioBookReturnValue = _context.AudioBooks.Add(audioBook);

            await _context.SaveChangesAsync();
            return audioBookReturnValue.Entity;
        }

        public async Task<AudioBook> Update(Guid id, AudioBook audioBook)
        {
            if (!_context.AudioBooks.Any(x => x.Id == id))
            {
                throw new InvalidOperationException();
            }

            var audioBookReturnValue = _context.AudioBooks.Update(audioBook);

            await _context.SaveChangesAsync();
            return audioBookReturnValue.Entity;
        }

        public async Task Delete(Guid id)
        {
            var audioBook = _context.AudioBooks.FirstOrDefault(x => x.Id == id);

            if (audioBook == null)
            {
                throw new InvalidOperationException();
            }

            _context.AudioBooks.Remove(audioBook);

            await _context.SaveChangesAsync();
        }
    }
}
