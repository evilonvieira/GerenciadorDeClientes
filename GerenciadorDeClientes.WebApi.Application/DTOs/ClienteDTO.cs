using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Application.DTOs
{
    public class ClienteDTO
    {
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? Logotipo { get; set; }
        public string? LogotipoModificado { get; set; }
        public long Id { get; set; }
    }
}
