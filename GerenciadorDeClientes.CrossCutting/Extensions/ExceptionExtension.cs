using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Infra.CrossCutting.Extensions
{
    public static class ExceptionExtension
    {
        public static string ErroTratado(this Exception ex) {
            return "Ops! Algo deu errado. Nossa equipe já está trabalhando para resolver o problema. Aguarde, ou entre em contato com o suporte.";
        }
    }
}
