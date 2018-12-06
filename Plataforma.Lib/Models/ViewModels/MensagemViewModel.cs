using System;
using System.Collections.Generic;
using System.Text;

namespace Plataforma.Lib.Models.ViewModels
{
    public enum TipoMensagem
    {
        Sucesso, Erro
    }

    public class MensagemViewModel
    {
        public string Mensagem { get; set; }
        public TipoMensagem Tipo { get; set; }
    }
}
