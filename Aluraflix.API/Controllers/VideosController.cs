using Aluraflix.API.Models;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aluraflix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _service;

        public VideosController(IVideoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Video>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<Video> Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Video value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem = _service.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _service.Remove(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id, [FromBody] Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var videoBD = _service.GetById(id);
            if (videoBD == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }
            _service.Update(videoBD, video);

            return NoContent();
        }

    }
}
