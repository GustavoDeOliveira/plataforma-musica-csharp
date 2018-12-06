using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public class Etiquetagem
    {
        public int EtiquetaId { get; set; }
        public virtual Etiqueta Etiqueta { get; set; }

        public int MusicaId { get; set; }
        public virtual Musica Musica { get; set; }
    }
}
