using DigitalLibraryApplication.Models;
using System;
using System.Collections.Generic;

namespace DigitalLibraryApplication.Services
{
    public interface IAudioBookService
    {
        IEnumerable<AudioBook> GetAll();
        AudioBook GetById(Guid id);
        AudioBook Add(AudioBook audioBook);
        AudioBook Update(Guid id, AudioBook audioBook);
        void Delete(Guid id);
    }
}
