using API_Crud.Models;
using System.Collections.Generic;

namespace API_Crud.Repository
{
    public interface IEscolaridadeRepository : IRepository<Escolaridade>
    {
        IEnumerable<Escolaridade> GetEscolaridadesPessoas();
    }
}
