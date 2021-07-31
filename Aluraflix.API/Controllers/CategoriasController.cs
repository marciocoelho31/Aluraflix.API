﻿using Aluraflix.API.Models;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aluraflix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IVideoService _videoService;

        public CategoriasController(ICategoriaService categoriaService, IVideoService videoService)
        {
            _categoriaService = categoriaService;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var items = _categoriaService.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            var item = _categoriaService.GetById(id);
            if (item == null)
            {
                return NotFound($"A categoria de id {id} não foi encontrada.");
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _categoriaService.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem = _categoriaService.GetById(id);
            if (existingItem == null)
            {
                return NotFound($"A categoria de id {id} não foi encontrada.");
            }
            _categoriaService.Remove(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoriaBD = _categoriaService.GetById(id);
            if (categoriaBD == null)
            {
                return NotFound($"A categoria de id {id} não foi encontrada.");
            }
            _categoriaService.Update(categoriaBD, categoria);

            return NoContent();
        }

        [HttpGet("{id}/videos")]
        public ActionResult<Categoria> GetVideosByCategoriaId(int id)
        {
            var items = _videoService.GetItemsByCategoriaId(id);
            return Ok(items);
        }

    }
}
