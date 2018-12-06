using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plataforma.Lib.Models;

namespace Plataforma.Admin.Models
{
    public class UsuarioEditarViewModel
    {
        public Usuario Usuario { get; set; }

        /// <summary>
        /// String serializada das preferências do usuário
        /// </summary>
        public string Preferencias { get; set; }

        /// <summary>
        /// String serializada das etiquetas existentes no banco
        /// </summary>
        public string Etiquetas { get; set; }
    }
}
