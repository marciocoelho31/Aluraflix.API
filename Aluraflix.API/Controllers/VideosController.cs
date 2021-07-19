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
        public IActionResult AdicionaVideo([FromBody] Video video)
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
            return NotFound();
        }

        //[HttpPut("{id}")]
        //public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDto)
        //{
        //    Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        //    if (filme == null)
        //    {
        //        return NotFound();
        //    }
        //    //filme.Titulo = filmeDto.Titulo;
        //    //filme.Diretor = filmeDto.Diretor;
        //    //filme.Genero = filmeDto.Genero;
        //    //filme.Duracao = filmeDto.Duracao;
        //    _mapper.Map(filmeDto, filme);
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeletaFilme(int id)
        //{
        //    Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        //    if (filme == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Remove(filme);
        //    _context.SaveChanges();

        //    return NoContent();



        //}


    }
}
