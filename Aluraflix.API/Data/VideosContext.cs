using Aluraflix.API.Entities;
using Aluraflix.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluraflix.API.Data
{
    public class VideosContext : DbContext
    {
        public VideosContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
