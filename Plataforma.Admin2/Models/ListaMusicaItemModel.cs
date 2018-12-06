using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plataforma.Admin.Models
{
    public class ListaMusicaItemModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ListaAutorItemModel> Autores { get; set; }
    }

    public class ListaAutorItemModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
