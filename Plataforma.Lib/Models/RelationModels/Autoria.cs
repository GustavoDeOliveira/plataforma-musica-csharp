using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public class Autoria
    {
        public int MusicaId { get; set; }
        public virtual Musica Musica { get; set; }

        public int AutorId { get; set; }
        public virtual Usuario Autor { get; set; }
    }
}
