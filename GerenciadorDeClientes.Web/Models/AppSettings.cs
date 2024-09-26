namespace GerenciadorDeClientes.Web.Models
{
    public class AppSettings
    {
        public WebApiGerenciadorDeClientes? webApiGerenciadorDeClientes { get; set; }

        public class WebApiGerenciadorDeClientes {
            public int registrosPorPaginas { get; set; }
            public string? url { get; set; }
            public string? metodoAuth { get; set; }
            public string? metodoClientesListar { get; set; }
            public string? metodoClientesPesquisarPorId { get; set; }
            public string? metodoExcluirClientePorId { get; set; }
            public string? metodoValidarDuplicidadeEmail { get; set; }
            public string? metodoSalvar { get; set; }
            public string? metodoClientesPesquisarComEnderecosPorId { get; set; }
            public string? metodoSalvarEndereco { get; set; }
            public string? metodoExcluirEnderecoPorId { get; set; }
        }
    }
}
