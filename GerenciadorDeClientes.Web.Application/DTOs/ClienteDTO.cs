using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Web.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Logotipo { get; set; }
        public string? LogotipoModificado { get; set; }
        public int IsNovo { get; set; }

        public IEnumerable<EnderecoDTO> Enderecos { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new ArgumentException("Informe o campo Nome");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentException("Informe o campo E-mail");
        }
    }
}
