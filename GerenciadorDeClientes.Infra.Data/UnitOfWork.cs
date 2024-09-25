using GerenciadorDeClientes.Infra.Data.Context;
using GerenciadorDeClientes.Infra.Data.Repositories;
using GerenciadorDeClientes.WebApi.Domain.Core.Interfaces.Repositories;

namespace GerenciadorDeClientes.Infra.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            UsuarioRepository = new UsuarioRepository(_context);
            ClienteRepository = new ClienteRepository(_context);
        }

        public IUsuarioRepository UsuarioRepository { get; private set; }
        public IClienteRepository ClienteRepository { get; private set; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose(); // Libera os recursos
        }
    }
}
