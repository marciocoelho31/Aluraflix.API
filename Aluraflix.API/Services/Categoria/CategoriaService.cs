using Aluraflix.API.Data;
using Aluraflix.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aluraflix.API.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly VideosContext _context;

        public CategoriaService(VideosContext context)
        {
            _context = context;
        }

        public Categoria Add(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public IEnumerable<Categoria> GetAllItems()
        {
            return _context.Categorias;
        }

        public Categoria GetById(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return;
            }
            _context.Remove(categoria);
            _context.SaveChanges();
        }

        public void Update(Categoria categoriaBD, Categoria categoria)
        {
            categoriaBD.Titulo = categoria.Titulo;
            categoriaBD.Cor = categoria.Cor;

            _context.SaveChanges();
        }

        public IEnumerable<Categoria> GetAllItemsPaginated(int page, int page_size)
        {
            if (page > 0)
            {
                return _context.Categorias
                    .Skip((page - 1) * page_size)
                    .OrderBy(v => v.Id)
                    .Take(page_size);
            }
            else
            {
                return _context.Categorias;
            }
        }

    }
}
