using DigitalLibraryApplication.Controllers;
using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace DigitalLibraryUnitTests
{
    public class AudioBookApiControllerTests
    {
        [Fact]
        public void GetAll_NoCondition_ReturnsAllAudioBooks()
        {
            var audioServiceMock = new Mock<IAudioBookService>();
            var apiController = new AudioBookApiController(audioServiceMock.Object);

            apiController.Get();

            audioServiceMock.Verify(x => x.GetAll());
        }

        [Fact]
        public void Get_IdPassed_ReturnsProperAudioBook()
        {
            var audioBook = new AudioBook();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Get(Guid.NewGuid());

            Assert.Equal((result as ObjectResult)?.Value, audioBook);
        }

        [Fact]
        public void Get_NoRequestedAudioBook_ReturnsEmptyResponseAudioBook()
        {
            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((AudioBook)null);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Get(Guid.NewGuid());

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Create_AudioBookPassed_ProperResponseReturned()
        {
            var audioBook = new AudioBook();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Add(It.Is<AudioBook>(y => y == audioBook))).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Create(audioBook);

            audioServiceMock.Verify(x => x.Add(It.Is<AudioBook>(y => y == audioBook)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Create_NullPassed_BadResponseReturned()
        {
            var audioBook = new AudioBook();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Add(It.Is<AudioBook>(y => y == audioBook))).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Create(null);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_AudioBookPassed_ReturnedProperAudioBook()
        {
            var id = Guid.NewGuid();
            var audioBook = new AudioBook()
            {
                Id = id
            };

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook))).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Update(id, audioBook);

            audioServiceMock.Verify(x => x.Update(It.Is<Guid>(guid => guid == id), It.Is<AudioBook>(ab => ab == audioBook)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Update_WrongIDPassed_BadRequestReturned()
        {
            var audioBook = new AudioBook();
            var id = Guid.NewGuid();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook))).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Update(id, audioBook);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_NullPassed_BadRequestReturned()
        {
            var audioBook = new AudioBook();
            var id = Guid.NewGuid();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook))).Returns(audioBook);

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Update(id, null);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_ExceptionTrowed_NotFoundReturned()
        {
            var id = Guid.NewGuid();
            var audioBook = new AudioBook()
            {
                Id = id
            };

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock
                .Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook)))
                .Throws(new Exception());

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Update(id, audioBook);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Delete_GoodIdPassed_ProperFunctionsCalled()
        {
            var id = Guid.NewGuid();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Delete(It.IsAny<Guid>()));

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Delete(id);

            audioServiceMock.Verify(x => x.Delete(It.Is<Guid>(guid => guid == id)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Delete_BadIdPassed_NotFoundReturned()
        {
            var id = Guid.NewGuid();

            var audioServiceMock = new Mock<IAudioBookService>();
            audioServiceMock.Setup(x => x.Delete(It.IsAny<Guid>())).Throws(new Exception());

            var apiController = new AudioBookApiController(audioServiceMock.Object);

            var result = apiController.Delete(id);

            audioServiceMock.Verify(x => x.Delete(It.Is<Guid>(guid => guid == id)));
            Assert.True(result is NotFoundResult);
        }
    }
}
