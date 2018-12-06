using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plataforma.Lib.Models
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; }

        public virtual IEnumerable<Preferencia> Preferencias { get; set; }

        public virtual IEnumerable<Comentario> Comentarios { get; set; }

        public virtual IEnumerable<Denuncia> Denuncias { get; set; }

        public virtual IEnumerable<Autoria> Musicas { get; set; }

        public virtual IEnumerable<Favorita> Favoritas { get; set; }
    }
}
