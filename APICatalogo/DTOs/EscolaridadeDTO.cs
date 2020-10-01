using System.Collections.Generic;

namespace API_Crud.DTOs
{
    public class EscolaridadeDTO
    {
        public int EscolaridadeId { get; set; }
        public string Descricao { get; set; }
        public ICollection<PessoaDTO> Pessoas { get; set; }
    }
}
