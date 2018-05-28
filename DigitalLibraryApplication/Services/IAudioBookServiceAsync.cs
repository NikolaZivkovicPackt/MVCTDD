using DigitalLibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalLibraryApplication.Services
{
    public interface IAudioBookServiceAsync
    {
        Task<IEnumerable<AudioBook>> GetAll();
        Task<AudioBook> GetById(Guid id);
        Task<AudioBook> Add(AudioBook audioBook);
        Task<AudioBook> Update(Guid id, AudioBook audioBook);
        Task Delete(Guid id);
    }
}
