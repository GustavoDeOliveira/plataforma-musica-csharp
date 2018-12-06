using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public class Etiqueta : Entidade
    {
        public string Nome { get; set; }

        public virtual IEnumerable<Etiquetagem> Musicas { get; set; }

        public virtual IEnumerable<Preferencia> Usuarios { get; set; }

    }
}
