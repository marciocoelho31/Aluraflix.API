using Aluraflix.API.Models;
using System.Collections.Generic;

namespace Aluraflix.API.Services
{
    public interface IVideoService
    {
        IEnumerable<Video> GetAllItems();
        IEnumerable<Video> GetItemsFromQueryString(string search);
        IEnumerable<Video> GetItemsByCategoriaId(int id);
        IEnumerable<Video> GetAllItemsPaginated(int page, int page_size);
        Video Add(Video novoItem);
        Video GetById(int id);
        void Remove(int id);
        void Update(Video videoBD, Video video);
    }
}
