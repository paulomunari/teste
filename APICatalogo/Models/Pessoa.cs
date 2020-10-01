using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Crud.Models
{
	[Table("Pessoas")]
    public class Pessoa //: IValidatableObject
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="O nome é obrigatório")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e no mínimo {2} caracteres",
            MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo {1} e no mínimo {2} caracteres", MinimumLength = 11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Deve ser informada uma data de nascimento válida no formado yyyy/MM/dd")]
        public DateTime Nascimento { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "Telefone deve possuir entre {1} e {2} caracteres", MinimumLength = 11)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "A cidade deve possuir no máximo {1} caracteres")]
        public string Cidade { get; set; }

        public Escolaridade Escolaridade { get; set; }
        public int EscolaridadeId { get; set; }
    }
}
