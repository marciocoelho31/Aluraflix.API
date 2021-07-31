using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aluraflix.API.Models
{
    public class Video
    {
        [Key]
        [Required(ErrorMessage = "O id do vídeo é obrigatório.")]
        public int Id { get; set; }

        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; } = 1;

        [Required(ErrorMessage = "O título do vídeo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do título do vídeo é de 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição do vídeo é obrigatória.")]
        [MaxLength(500, ErrorMessage = "O tamanho máximo da descrição do vídeo é de 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A URL do vídeo é obrigatória.")]
        [MaxLength(200, ErrorMessage = "O tamanho máximo da URL do vídeo é de 200 caracteres.")]
        [Url(ErrorMessage = "URL do vídeo inválida")]
        public string Url { get; set; }
    }
}
