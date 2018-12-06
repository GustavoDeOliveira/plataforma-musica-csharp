using System;

namespace Plataforma.Admin.Models
{
    public class ErroViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}