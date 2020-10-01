using API_Crud.Models;
using System.Collections.Generic;

namespace API_Crud.Repository
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        IEnumerable<Pessoa> GetPessoasPorEscolaridade();
    }
}
