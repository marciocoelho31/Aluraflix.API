using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aluraflix.API.Entities
{
    public class Categoria
    {
        [Key]
        [Required(ErrorMessage = "O id da categoria é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título da categoria é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do título da categoria é de 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A cor da categoria é obrigatória.")]
        [MaxLength(15, ErrorMessage = "O tamanho máximo da cor da categoria é de 15 caracteres.")]
        public string Cor { get; set; }
    }
}
