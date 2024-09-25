namespace GerenciadorDeClientes.Web.Models
{
    public class AppSettings
    {
        public WebApiGerenciadorDeClientes? webApiGerenciadorDeClientes { get; set; }

        public class WebApiGerenciadorDeClientes {
            public string? url { get; set; }
            public string? metodoAuth { get; set; }
            public string? metodoClientesListar { get; set; }
        }
    }
}
