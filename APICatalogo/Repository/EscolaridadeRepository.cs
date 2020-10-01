using API_Crud.Context;
using API_Crud.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_Crud.Repository
{
    public class EscolaridadeRepository : Repository<Escolaridade>, IEscolaridadeRepository
    {
        public EscolaridadeRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Escolaridade> GetEscolaridadesPessoas()
        {
            return Get().Include(x => x.Pessoas);
        }
    }
}
