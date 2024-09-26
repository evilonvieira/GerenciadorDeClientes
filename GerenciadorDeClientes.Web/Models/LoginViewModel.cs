namespace GerenciadorDeClientes.Web.Models
{
    public class LoginViewModel
    {
        public  string? Email { get; set; }
        public string? Senha { get; set; }


        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentException("Informe o campo E-mail");
            if (string.IsNullOrEmpty(Senha))
                throw new ArgumentException("Informe o campo Senha");
        }
    }
}
