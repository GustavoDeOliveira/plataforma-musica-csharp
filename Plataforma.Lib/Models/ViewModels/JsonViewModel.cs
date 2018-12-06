using System;

namespace Plataforma.Lib.ViewModels
{
    public class JsonViewModel<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }
    }
}