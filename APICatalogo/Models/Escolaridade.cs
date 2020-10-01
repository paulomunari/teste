using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Crud.Models
{
    [Table("Escolaridades")]
    public class Escolaridade
    {
        public Escolaridade()
        {
            Pessoas = new Collection<Pessoa>();
        }
        [Key]
        public int EscolaridadeId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Descricao { get; set; }

        public ICollection<Pessoa> Pessoas { get; set; }
    }
}
