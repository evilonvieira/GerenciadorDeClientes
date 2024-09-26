namespace GerenciadorDeClientes.Infra.CrossCutting
{
    public class ResultadoOperacao
    {
        protected bool _sucesso;

        public IEnumerable<string> Erros { get; set; }

        public string MensagemDeErro
        {
            get
            {
                if (Erros == null)
                    return string.Empty;

                return string.Join(",", Erros);
            }
        }

        public bool Sucesso => _sucesso;
        public bool Falha => !_sucesso;

        protected ResultadoOperacao(bool sucesso) { _sucesso = sucesso; }
        protected ResultadoOperacao() { _sucesso = true; }

        public static ResultadoOperacao CriarResultadoComSucesso()
        {
            return new ResultadoOperacao(true);
        }

        public static ResultadoOperacao CriarResultadoComFalha(params string[] erros)
        {
            return new ResultadoOperacao(false)
            {
                Erros = erros
            };
        }

        public static ResultadoOperacao CriarResultadoComFalha(IEnumerable<string> erros)
        {
            return new ResultadoOperacao(false)
            {
                Erros = erros
            };
        }
        public static ResultadoOperacao CriarResultadoComFalha(string erro)
        {
            return new ResultadoOperacao(false)
            {
                Erros = new string[] { erro }
            };
        }
    }

    public class ResultadoOperacao<TRetorno> : ResultadoOperacao
    {
        public TRetorno Retorno { get; set; }

        private ResultadoOperacao(bool sucesso) : base(sucesso) { }
        private ResultadoOperacao() : base(true) { }


        public static ResultadoOperacao<TRetorno> CriarResultadoComSucesso(TRetorno retorno)
        {
            return new ResultadoOperacao<TRetorno>(true)
            {
                Retorno = retorno
            };
        }

        public static new ResultadoOperacao<TRetorno> CriarResultadoComFalha(params string[] erros)
        {
            return new ResultadoOperacao<TRetorno>(false)
            {
                Erros = erros
            };
        }
        public static new ResultadoOperacao<TRetorno> CriarResultadoComFalha(string erro)
        {
            return new ResultadoOperacao<TRetorno>(false)
            {
                Erros = new string[] { erro }
            };
        }
    }
}
