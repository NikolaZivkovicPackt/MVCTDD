using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DigitalLibraryUnitTests
{
    public class AudioBooksServiceTests
    {
        [Fact]
        public void GetAll_NoConditions_ListReturned()
        {
            var audioBook = new AudioBook();
            var resultList = new List<AudioBook>() { audioBook };

            var dbSetMock = new Mock<DbSet<AudioBook>>();
            dbSetMock.As<IQueryable<AudioBook>>().Setup(x => x.Provider).Returns(resultList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<AudioBook>>().Setup(x => x.Expression).Returns(resultList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<AudioBook>>().Setup(x => x.ElementType).Returns(resultList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<AudioBook>>().Setup(x => x.GetEnumerator()).Returns(resultList.AsQueryable().GetEnumerator());

            var dbContextMock = new Mock<DigitalLibraryContext>();
            dbContextMock.Setup(x => x.AudioBooks).Returns(dbSetMock.Object);

            var audioBooksService = new AudioBookService(dbContextMock.Object);
            var results = audioBooksService.GetAll();

            Assert.Equal(resultList, results.ToList());
        }
    }
}
