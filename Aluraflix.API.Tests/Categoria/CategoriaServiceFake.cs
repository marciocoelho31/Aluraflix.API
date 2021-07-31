using Aluraflix.API.Models;
using Aluraflix.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluraflix.API.Tests
{
    public class CategoriaServiceFake : ICategoriaService
    {
        private readonly List<Categoria> _categorias;

        public CategoriaServiceFake()
        {
            _categorias = new List<Categoria>()
            {
                new Categoria() { Id = 1, Titulo = "Categoria 1",
                                    Cor = "Descrição Categoria 1" },
                new Categoria() { Id = 2, Titulo = "Categoria 2",
                                    Cor = "Descrição Categoria 2" }
            };
        }

        public IEnumerable<Categoria> GetAllItems()
        {
            return _categorias;
        }

        public Categoria Add(Categoria novoItem)
        {
            novoItem.Id = GeraId();
            _categorias.Add(novoItem);
            return novoItem;
        }

        public Categoria GetById(int id)
        {
            return _categorias.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(int id)
        {
            var item = _categorias.First(a => a.Id == id);
            _categorias.Remove(item);
        }

        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        public void Update(Categoria categoriaBD, Categoria categoria)
        {
            var item = _categorias.First(a => a.Id == categoriaBD.Id);

            item.Titulo = categoria.Titulo;
            item.Cor = categoria.Cor;
        }

    }
}
