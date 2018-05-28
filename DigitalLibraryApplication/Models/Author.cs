﻿using System;

namespace DigitalLibraryApplication.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsNarator { get; set; }
    }
}
