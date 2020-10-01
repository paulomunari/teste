using API_Crud.Context;
using API_Crud.Models;
using System.Collections.Generic;
using System.Linq;

namespace API_Crud.Repository
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Pessoa> GetPessoasPorEscolaridade()
        {
            return Get().OrderBy(c => c.Cidade).ToList();
        }
	}
}
