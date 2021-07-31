using Aluraflix.API.Controllers;
using Aluraflix.API.Models;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aluraflix.API.Tests
{
    public class CategoriaControllerTest
    {
        CategoriasController _controller;
        ICategoriaService _categoriaService;
        IVideoService _videoService;

        public CategoriaControllerTest()
        {
            _categoriaService = new CategoriaServiceFake();
            _videoService = new VideoServiceFake();
            _controller = new CategoriasController(_categoriaService, _videoService);
        }

        [Fact]
        public void GetCategorias_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        
        [Fact]
        public void GetCategorias_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Categoria>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetCategoriaById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(99);
            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetCategoriaById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCategoriaById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            // Assert
            Assert.IsType<Categoria>(okResult.Value);
            Assert.Equal(testeId, (okResult.Value as Categoria).Id);
        }

        [Fact]
        public void AddCategoria_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Categoria()
            {
                Cor = "Cor teste"
            };
            _controller.ModelState.AddModelError("Titulo", "Required");
            // Act
            var badResponse = _controller.Post(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void AddCategoria_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Categoria testeItem = new Categoria()
            {
                Titulo = "Teste titulo",
                Cor = "Teste cor"
            };
            // Act
            var createdResponse = _controller.Post(testeItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void AddCategoria_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Categoria()
            {
                Titulo = "Teste titulo",
                Cor = "Teste cor"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Categoria;

            // Assert
            Assert.IsType<Categoria>(item);
            Assert.Equal("Teste titulo", item.Titulo);
        }

        [Fact]
        public void RemoveCategoria_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var IdInexistente = 99;
            // Act
            var badResponse = _controller.Remove(IdInexistente);
            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact]
        public void RemoveCategoria_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void RemoveCategoria_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var Id_Existente = 2;
            // Act
            var okResponse = _controller.Remove(Id_Existente);
            // Assert
            Assert.Single(_categoriaService.GetAllItems());
        }

        [Fact]
        public void UpdateCategoria_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Categoria()
            {
                Cor = "Descricao de cor"
            };
            _controller.ModelState.AddModelError("Titulo", "Required");

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Categoria;

            // Act
            var badResponse = _controller.UpdateCategoria(existingItem.Id, nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void UpdateCategoria_ValidObjectPassed_ReturnsNoContent()
        {
            // Arrange
            Categoria testeItem = new Categoria()
            {
                Titulo = "Teste titulo",
                Cor = "Teste cor"
            };

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Categoria;

            // Act
            var noContentResponse = _controller.UpdateCategoria(existingItem.Id, testeItem);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public void UpdateCategoria_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var testItem = new Categoria()
            {
                Titulo = "Teste titulo alterado",
                Cor = "Teste cor"
            };

            var testeId = 1;
            var okResult = _controller.Get(testeId).Result as OkObjectResult;
            var existingItem = okResult.Value as Categoria;

            // Act
            var response = _controller.UpdateCategoria(existingItem.Id, testItem);

            var updatedItem = (_controller.Get(testeId).Result as OkObjectResult).Value as Categoria;

            // Assert
            Assert.IsType<Categoria>(updatedItem);
            Assert.Equal("Teste titulo alterado", updatedItem.Titulo);
        }

        [Fact]
        public void GetVideosByCategoriaId_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var testeId = 1;
            // Act
            var okResult = _controller.GetVideosByCategoriaId(testeId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

    }
}
