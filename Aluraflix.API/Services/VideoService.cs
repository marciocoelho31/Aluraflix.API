using Aluraflix.API.Data;
using Aluraflix.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aluraflix.API.Services
{
    public class VideoService : IVideoService
    {
        private readonly VideosContext _context;

        public VideoService(VideosContext context)
        {
            _context = context;
        }

        public Video Add(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
            return video;
        }

        public IEnumerable<Video> GetAllItems()
        {
            return _context.Videos;
        }

        public Video GetById(int id)
        {
            return _context.Videos.FirstOrDefault(filme => filme.Id == id);
        }

        public void Remove(int id)
        {
            Video video = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (video == null)
            {
                return;
            }
            _context.Remove(video);
            _context.SaveChanges();
        }

        public void Update(Video videoBD, Video video)
        {
            videoBD.Titulo = video.Titulo;
            videoBD.Descricao = video.Descricao;
            videoBD.Url = video.Url;

            _context.SaveChanges();
        }
    }
}
