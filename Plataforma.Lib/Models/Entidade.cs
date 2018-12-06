using System;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Lib.Models
{
    public abstract class Entidade
    {
        public int Id { get; set; }

        [Display(Name = "Modificado Em")]
        [ConcurrencyCheck]
        public DateTime? ModificadoEm { get; set; }

        [Display(Name = "Criado Em")]
        public DateTime CriadoEm { get; set; }

        public bool Ativo { get; set; }

        [Timestamp]
        public byte[] VersaoRegistro { get; set; }
    }
}
