using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibraryApplication.Models
{
    public class DigitalBook
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Summary { get; set; }
    }
}
