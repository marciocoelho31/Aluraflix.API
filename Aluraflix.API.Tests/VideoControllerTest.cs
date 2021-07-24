using Aluraflix.API.Controllers;
using Aluraflix.API.Models;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aluraflix.API.Tests
{
    public class VideoControllerTest
    {
        VideosController _controller;
        IVideoService _service;

        public VideoControllerTest()
        {
            _service = new VideoServiceFake();
            _controller = new VideosController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Video>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(99);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            // Assert
            Assert.IsType<Video>(okResult.Value);
            Assert.Equal(testeId, (okResult.Value as Video).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Video()
            {
                Descricao = "Descricao de um filme",
                Url = "http://www.teste.com"
            };
            _controller.ModelState.AddModelError("Titulo", "Required");
            // Act
            var badResponse = _controller.Post(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Video testeItem = new Video()
            {
                Titulo = "Teste titulo",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };
            // Act
            var createdResponse = _controller.Post(testeItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Video()
            {
                Titulo = "Teste titulo",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Video;

            // Assert
            Assert.IsType<Video>(item);
            Assert.Equal("Teste titulo", item.Titulo);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var IdInexistente = 99;
            // Act
            var badResponse = _controller.Remove(IdInexistente);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.Equal(2, _service.GetAllItems().Count());
        }

        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Video()
            {
                Descricao = "Descricao de um filme",
                Url = "http://www.teste.com"
            };
            _controller.ModelState.AddModelError("Titulo", "Required");

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var badResponse = _controller.UpdateVideo(existingItem.Id, nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsNoContent()
        {
            // Arrange
            Video testeItem = new Video()
            {
                Titulo = "Teste titulo",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var noContentResponse = _controller.UpdateVideo(existingItem.Id, testeItem);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var testItem = new Video()
            {
                Titulo = "Teste titulo alterado",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var response = _controller.UpdateVideo(existingItem.Id, testItem);

            var updatedItem = (_controller.Get(testeId).Result as OkObjectResult).Value as Video;

            // Assert
            Assert.IsType<Video>(updatedItem);
            Assert.Equal("Teste titulo alterado", updatedItem.Titulo);
        }

    }
}
