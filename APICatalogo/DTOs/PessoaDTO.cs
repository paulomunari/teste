using System;

namespace API_Crud.DTOs
{
    public class PessoaDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public int EscolaridadeId { get; set; }
    }
}
