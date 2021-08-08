using Aluraflix.API.Entities;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Aluraflix.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly ICategoriaService _categoriaService;

        public VideosController(IVideoService videoService, ICategoriaService categoriaService)
        {
            _videoService = videoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Video>> Get([FromQuery] string search = "", [FromQuery] int page = 0)
        {
            if (page > 0)
            {
                // default solicitado na regra de negócio - "paginação que retorne 5 itens por página"
                return Ok(_videoService.GetAllItemsPaginated(page, 5));
            }
            else if (!string.IsNullOrEmpty(search))
            {
                return Ok(_videoService.GetItemsFromQueryString(search));
            }
            else
            {
                return Ok(_videoService.GetAllItems());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Video> GetById(int id)
        {
            var item = _videoService.GetById(id);
            if (item == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_categoriaService.GetById(video.CategoriaId) == null)
            {
                return BadRequest($"A categoria {video.CategoriaId} não foi encontrada.");
            }
            var item = _videoService.Add(video);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem = _videoService.GetById(id);
            if (existingItem == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }
            _videoService.Remove(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id, [FromBody] Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_categoriaService.GetById(video.CategoriaId) == null)
            {
                return BadRequest($"A categoria {video.CategoriaId} não foi encontrada.");
            }

            var videoBD = _videoService.GetById(id);
            if (videoBD == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }
            _videoService.Update(videoBD, video);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("free")]
        public ActionResult<IEnumerable<Video>> GetThreeFirstFreeVideos()
        {
            return Ok(_videoService.GetThreeFirstFreeVideos());
        }

    }
}
