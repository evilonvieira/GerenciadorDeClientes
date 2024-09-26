namespace GerenciadorDeClientes.WebApi.Application
{
    public class AppSettings
    {
        public Jwt? jwt { get; set; }

        public class Jwt {
            public string Key { get; set; }     
            public string Issuer { get; set; }     
            public string Audience { get; set; }     
            public int ExpireMinutes { get; set; }     
        }
    }
}
