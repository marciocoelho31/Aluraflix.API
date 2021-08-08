using Aluraflix.API.Data;
using Aluraflix.API.Entities;
using Microsoft.EntityFrameworkCore;
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
            videoBD.CategoriaId = video.CategoriaId;

            _context.SaveChanges();
        }

        public IEnumerable<Video> GetItemsFromQueryString(string search)
        {
            return _context.Videos
                .Where(
                    v => v.Titulo.ToUpper().Contains(search.ToUpper())
                    || v.Descricao.ToUpper().Contains(search.ToUpper())
                );
        }

        public IEnumerable<Video> GetItemsByCategoriaId(int id)
        {
            return _context.Videos
                .Where(
                    v => v.CategoriaId == id
                );
        }

        public IEnumerable<Video> GetAllItemsPaginated(int page, int page_size)
        {
            if (page > 0)
            {
                return _context.Videos
                    .Skip((page - 1) * page_size)
                    .OrderBy(v => v.Id)
                    .Take(page_size);
            }
            else
            {
                return _context.Videos;
            }
        }

        public IEnumerable<Video> GetThreeFirstFreeVideos()
        {
            return _context.Videos.Take(3);
        }

    }
}
