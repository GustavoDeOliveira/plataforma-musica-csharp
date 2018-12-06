using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public class Comentario : Entidade
    {
        public string Conteudo { get; set; }

        [Required]
        public int MusicaId { get; set; }
        public Musica Musica { get; set; }

        [Required]
        public int AutorId { get; set; }
        public Usuario Autor { get; set; }

    }
}
