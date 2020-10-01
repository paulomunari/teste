using API_Crud.Context;

namespace API_Crud.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PessoaRepository _pessoaRepo;
        private EscolaridadeRepository _escolaridadeRepo;
        public AppDbContext _context;
        public UnitOfWork(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IPessoaRepository PessoaRepository
        {
            get
            {
                return _pessoaRepo = _pessoaRepo ?? new PessoaRepository(_context);
            }
        }

        public IEscolaridadeRepository EscolaridadeRepository
        {
            get
            {
                return _escolaridadeRepo = _escolaridadeRepo ?? new EscolaridadeRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
