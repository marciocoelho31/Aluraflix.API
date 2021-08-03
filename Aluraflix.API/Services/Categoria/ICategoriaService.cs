using Aluraflix.API.Models;
using System.Collections.Generic;

namespace Aluraflix.API.Services
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAllItems();
        IEnumerable<Categoria> GetAllItemsPaginated(int page, int page_size);
        Categoria Add(Categoria novoItem);
        Categoria GetById(int id);
        void Remove(int id);
        void Update(Categoria categoriaBD, Categoria categoria);
    }
}
