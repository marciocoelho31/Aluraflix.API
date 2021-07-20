using Aluraflix.API.Data;
using Aluraflix.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluraflix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly VideosContext _context;

        public VideosController(VideosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Video> GetVideos()
        {
            return _context.Videos;
        }

        [HttpPost]
        public IActionResult AddVideo([FromBody] Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetVideoById), new { Id = video.Id }, video);
        }

        [HttpGet("{id}")]
        public IActionResult GetVideoById(int id)
        {
            Video video = _context.Videos.FirstOrDefault(filme => filme.Id == id);
            if (video != null)
            {
                return Ok(video);
            }
            return NotFound($"O vídeo de id {id} não foi encontrado.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id, [FromBody] Video video)
        {
            Video videoBD = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (videoBD == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }
            videoBD.Titulo = video.Titulo;
            videoBD.Descricao = video.Descricao;
            videoBD.Url = video.Url;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveVideo(int id)
        {
            Video video = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (video == null)
            {
                return NotFound($"O vídeo de id {id} não foi encontrado.");
            }

            _context.Remove(video);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
