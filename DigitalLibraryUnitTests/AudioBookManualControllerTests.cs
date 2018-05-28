using DigitalLibraryApplication.Controllers;
using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DigitalLibraryUnitTests
{
    public class AudioBookManualControllerTests
    {
        [Fact]
        public async Task Index_NoCondition_ViewWithInformationReturned()
        {
            var listOfBooks = new List<AudioBook>() { new AudioBook() };
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.GetAll()).Returns(Task.FromResult((IEnumerable<AudioBook>)listOfBooks));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AudioBook>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_NoCondition_ViewReturned()
        {
            var audioBooksService = new Mock<IAudioBookServiceAsync>();

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_DataPassed_ViewWithInformationReturned()
        {
            var audioBook = new AudioBook();
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Add(It.IsAny<AudioBook>())).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Create(audioBook);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Create_ModelNotValid_ViewReturned()
        {
            var audioBook = new AudioBook();
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Add(It.IsAny<AudioBook>())).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);
            controller.ModelState.AddModelError("Author", "Required");

            var result = await controller.Create(audioBook);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_NoCondition_ViewReturned()
        {
            var audioBook = new AudioBook();
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Edit(Guid.NewGuid());

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AudioBook>(
                viewResult.ViewData.Model);
            Assert.Equal(audioBook, model);
        }

        [Fact]
        public async Task Edit_NullCondition_NotFoundReturned()
        {
            var audioBooksService = new Mock<IAudioBookServiceAsync>();

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_NoAudioBook_NotFoundReturned()
        {
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<AudioBook>())).Returns(Task.FromResult((AudioBook)null));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Edit(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Edit_AudioBookPassed_RedirectToActionReturned()
        {
            var id = Guid.NewGuid();
            var audioBook = new AudioBook()
            {
                Id = id
            };

            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook))).Returns(Task.FromResult(audioBook));

            var apiController = new AudioBookManualController(audioBooksService.Object);

            var result = await apiController.Edit(id, audioBook);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Edit_ModelNotValid_ViewReturned()
        {
            var id = Guid.NewGuid();
            var audioBook = new AudioBook()
            {
                Id = id
            };
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Update(It.IsAny<Guid>(), It.Is<AudioBook>(ab => ab == audioBook))).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);
            controller.ModelState.AddModelError("Author", "Required");

            var result = await controller.Edit(id, audioBook);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_NoCondition_ViewReturned()
        {
            var audioBook = new AudioBook();
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Edit(Guid.NewGuid());

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AudioBook>(
                viewResult.ViewData.Model);
            Assert.Equal(audioBook, model);
        }

        [Fact]
        public async Task Delete_NullCondition_NotFoundReturned()
        {
            var audioBook = new AudioBook();
            var audioBooksService = new Mock<IAudioBookServiceAsync>();
            audioBooksService.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(audioBook));

            var controller = new AudioBookManualController(audioBooksService.Object);

            var result = await controller.Delete(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_GoodIdPassed_ProperFunctionsCalled()
        {
            var id = Guid.NewGuid();

            var audioBooksService = new Mock<IAudioBookServiceAsync>();

            var apiController = new AudioBookManualController(audioBooksService.Object);

            await apiController.DeleteConfirmed(id);

            audioBooksService.Verify(x => x.Delete(It.Is<Guid>(guid => guid == id)));
        }
        
        [Fact]
        public async Task Delete_BadIdPassed_ProperFunctionsCalled()
        {
            var id = Guid.NewGuid();

            var audioBooksService = new Mock<IAudioBookServiceAsync>();

            var apiController = new AudioBookManualController(audioBooksService.Object);
            audioBooksService.Setup(x => x.Delete(It.IsAny<Guid>())).Throws(new InvalidOperationException());

            await Assert.ThrowsAsync<InvalidOperationException>(() => apiController.DeleteConfirmed(id));
        }
    }
}
