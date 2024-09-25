using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Web.Application.DTOs
{
    public class EnderecoDTO
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Uf { get; set; }
        public int IsNovo { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Logradouro)) throw new ArgumentException("Informe o campo Logradouro");
            if (string.IsNullOrEmpty(Numero))throw new ArgumentException("Informe o campo Número");
            if (string.IsNullOrEmpty(Bairro))throw new ArgumentException("Informe o campo Bairro");
            if (string.IsNullOrEmpty(Cidade))throw new ArgumentException("Informe o campo Cidade");
        }
    }
}
