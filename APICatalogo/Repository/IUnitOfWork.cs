namespace API_Crud.Repository
{
    public interface IUnitOfWork
    {
        IPessoaRepository PessoaRepository { get; }
        IEscolaridadeRepository EscolaridadeRepository { get; }
        void Commit();
    }
}
