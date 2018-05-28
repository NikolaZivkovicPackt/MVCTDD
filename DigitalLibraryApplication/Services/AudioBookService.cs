using System;
using System.Collections.Generic;
using System.Linq;
using DigitalLibraryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryApplication.Services
{
    public class AudioBookService : IAudioBookService
    {
        private DigitalLibraryContext _context;

        public AudioBookService(DigitalLibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<AudioBook> GetAll()
        {
            return _context.AudioBooks
                .Include(x => x.Author)
                .Include(x => x.Narator)
                .Include(x => x.Publisher)
                .ToList();
        }

        public AudioBook GetById(Guid id)
        {
            return _context.AudioBooks
                .Include(x => x.Author)
                .Include(x => x.Narator)
                .Include(x => x.Publisher)
                .Single(x => x.Id == id);
        }

        public AudioBook Add(AudioBook audioBook)
        {
            audioBook.Id = Guid.NewGuid();

            var audioBookReturnValue = _context.AudioBooks.Add(audioBook);

            _context.SaveChanges();
            return audioBookReturnValue.Entity;
        }

        public AudioBook Update(Guid id, AudioBook audioBook)
        {
            if (!_context.AudioBooks.Any(x => x.Id == id))
            {
                throw new InvalidOperationException();
            }

            var audioBookReturnValue = _context.AudioBooks.Update(audioBook);

            _context.SaveChanges();
            return audioBookReturnValue.Entity;
        }

        public void Delete(Guid id)
        {
            var audioBook = _context.AudioBooks.FirstOrDefault(x => x.Id == id);

            if (audioBook == null)
            {
                throw new InvalidOperationException();
            }

            _context.AudioBooks.Remove(audioBook);

            _context.SaveChanges();
        }
    }
}
