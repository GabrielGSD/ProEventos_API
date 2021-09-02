using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         StringLength(50, MinimumLength = 0, ErrorMessage = "O campo {0} deve ter de 3 a 50 caracteres.")]
        public string Tema { get; set; }

        [Display(Name = "Qtd Pessoas."),
         Range(1, 12000, ErrorMessage = "{0} não pode ser menor que 1 nem maior que 120.000.")]
        public int QdtPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpeg, jpg, bmp, png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
         Phone(ErrorMessage = "O campo {0}  está com o número inválido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
         Display(Name = "e-mail"),
         EmailAddress(ErrorMessage = "O campo {0} precisa ser válido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }
    }
}