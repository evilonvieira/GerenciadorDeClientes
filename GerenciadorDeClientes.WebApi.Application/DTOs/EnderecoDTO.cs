using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Application.DTOs
{
    public class EnderecoDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Uf { get; set; }
        public int IsNovo { get; set; }
    }
}
