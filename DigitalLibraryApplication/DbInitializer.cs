using System;
using System.Linq;
using DigitalLibraryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryApplication
{
    public class DbInitializer
    {
        public static void Initialize(DigitalLibraryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var author = new Author()
            {
                FirstName = "Nikola",
                LastName = "Zivkovic",
                Age = 30,
                Id = Guid.NewGuid(),
                IsNarator = true
            };
            context.Authors.Add(author);
            context.SaveChanges();

            var publisher = new Publisher(){Id = Guid.NewGuid(), Name = "Pack Publishing"};
            context.Publishers.Add(publisher);
            context.SaveChanges();

            var audioBook = new AudioBook()
            {
                Id = Guid.NewGuid(),
                Author = author,
                Publisher = publisher,
                Narator = author,
                Title = "Deep Learning in JavaScript",
                Subtitle = "Tensorflow.js",
                Summary = ""
            };
            context.AudioBooks.Add(audioBook);
            context.SaveChanges();
        }

    }
}
