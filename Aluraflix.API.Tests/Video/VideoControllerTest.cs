using Aluraflix.API.Controllers;
using Aluraflix.API.Entities;
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
        IVideoService _videoService;
        ICategoriaService _categoriaService;

        public VideoControllerTest()
        {
            _videoService = new VideoServiceFake();
            _categoriaService = new CategoriaServiceFake();
            _controller = new VideosController(_videoService, _categoriaService);
        }

        [Fact]
        public void GetVideos_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetVideos_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Video>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetVideosFromQueryString_WhenCalled_ReturnsOkResult()
        {
            // Act
            var statusCode200Result = _controller.Get("teste");
            // Assert
            Assert.IsType<OkObjectResult>(statusCode200Result.Result);
        }

        [Fact]
        public void GetVideosPaginated_WhenCalled_ReturnsOkResult()
        {
            // Act
            var statusCode200Result = _controller.Get("", 1);
            // Assert
            Assert.IsType<OkObjectResult>(statusCode200Result.Result);
        }

        [Fact]
        public void GetVideoById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetById(99);
            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetVideoById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.GetById(testeId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetVideoById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.GetById(testeId).Result as OkObjectResult;
            // Assert
            Assert.IsType<Video>(okResult.Value);
            Assert.Equal(testeId, (okResult.Value as Video).Id);
        }

        [Fact]
        public void AddVideo_InvalidObjectPassed_ReturnsBadRequest()
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
        public void AddVideo_ValidObjectPassed_ReturnsCreatedResponse()
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
        public void AddVideo_ValidObjectPassed_ReturnedResponseHasCreatedItem()
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
        public void RemoveVideo_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var IdInexistente = 99;
            // Act
            var badResponse = _controller.Remove(IdInexistente);
            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact]
        public void RemoveVideo_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void RemoveVideo_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.Equal(2, _videoService.GetAllItems().Count());
        }

        [Fact]
        public void UpdateVideo_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Video()
            {
                Descricao = "Descricao de um filme",
                Url = "http://www.teste.com"
            };
            _controller.ModelState.AddModelError("Titulo", "Required");

            var testeId = 1;
            var okResult = _controller.GetById(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var badResponse = _controller.UpdateVideo(existingItem.Id, nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void UpdateVideo_ValidObjectPassed_ReturnsNoContent()
        {
            // Arrange
            Video testeItem = new Video()
            {
                Titulo = "Teste titulo",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };

            var testeId = 1;
            var okResult = _controller.GetById(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var noContentResponse = _controller.UpdateVideo(existingItem.Id, testeItem);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public void UpdateVideo_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var testItem = new Video()
            {
                Titulo = "Teste titulo alterado",
                Descricao = "Teste descricao",
                Url = "http://www.teste.com"
            };

            var testeId = 1;
            var okResult = _controller.GetById(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Video;

            // Act
            var response = _controller.UpdateVideo(existingItem.Id, testItem);

            var updatedItem = (_controller.GetById(testeId).Result as OkObjectResult).Value as Video;

            // Assert
            Assert.IsType<Video>(updatedItem);
            Assert.Equal("Teste titulo alterado", updatedItem.Titulo);
        }

    }
}
