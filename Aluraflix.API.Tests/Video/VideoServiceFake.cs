using Aluraflix.API.Entities;
using Aluraflix.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluraflix.API.Tests
{
    public class VideoServiceFake : IVideoService
    {
        private readonly List<Video> _videos;

        public VideoServiceFake()
        {
            _videos = new List<Video>()
            {
                new Video() { Id = 1, Titulo = "Filme 1",
                                    Descricao ="Descrição Filme 1",
                                    Url= "http://www.filme1.com" },
                new Video() { Id = 2, Titulo = "Filme 2",
                                    Descricao ="Descrição Filme 2",
                                    Url= "http://www.filme2.com" },
                new Video() { Id = 3, Titulo = "Filme 3",
                                    Descricao ="Descrição Filme 3",
                                    Url= "http://www.filme3.com" }
            };
        }

        public IEnumerable<Video> GetAllItems()
        {
            return _videos;
        }

        public Video Add(Video novoItem)
        {
            novoItem.Id = GeraId();
            _videos.Add(novoItem);
            return novoItem;
        }

        public Video GetById(int id)
        {
            return _videos.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(int id)
        {
            var item = _videos.First(a => a.Id == id);
            _videos.Remove(item);
        }

        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        public void Update(Video videoBD, Video video)
        {
            var item = _videos.First(a => a.Id == videoBD.Id);

            item.Titulo = video.Titulo;
            item.Descricao = video.Descricao;
            item.Url = video.Url;
        }

        public IEnumerable<Video> GetItemsFromQueryString(string search)
        {
            return _videos
                .Where(
                    v => v.Titulo.ToUpper().Contains(search.ToUpper())
                    || v.Descricao.ToUpper().Contains(search.ToUpper())
                );
        }

        public IEnumerable<Video> GetItemsByCategoriaId(int id)
        {
            return _videos
                .Where(
                    v => v.CategoriaId == id
                );
        }

        public IEnumerable<Video> GetAllItemsPaginated(int page, int page_size)
        {
            return _videos
                .Skip((page - 1) * page_size)
                    .OrderBy(v => v.Id)
                    .Take(page_size);
        }

        public IEnumerable<Video> GetThreeFirstFreeVideos()
        {
            return _videos
                .OrderBy(v => v.Id)
                .Take(3);
        }
    }
}
