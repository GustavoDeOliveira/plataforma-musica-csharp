using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public class Musica : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual IEnumerable<Etiquetagem> Etiquetas { get; set; }

        public virtual IEnumerable<Comentario> Comentarios { get; set; }

        public virtual IEnumerable<Denuncia> Denuncias { get; set; }
        
        public virtual IEnumerable<Autoria> Autores { get; set; }

        public virtual IEnumerable<Favorita> Favoritos { get; set; }

    }
}
