using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
	public class EventoDto
	{
		public int Id { get; set; }
		public string Local { get; set; }
		public string DataEvento { get; set; }
		[
			Required(ErrorMessage = "O campo {0} é obrigatório"), // O ErrorMessage substitui a mensagem de error original (que não é ruim) por outra personalizada.
																  // MinLength(3, ErrorMessage = "O {0} precisa ter no mínimo 3 caracteres"),
																  // MaxLength(3, ErrorMessage = "O {0} precisa ter no máximo 50 caracteres")
			StringLength(50, MinimumLength = 3) // alternativa que substitui o min e max length (que estão comentados acima)
		]
		public string Tema { get; set; }
		[
			Display(Name = "Quantidade de pessoas"),
			Range(1, 120000, ErrorMessage = "{0} só poderá estar entre 1 a 120 mil.")
		]
		public int QtdPessoas { get; set; }
		public string Lote { get; set; }
		[
			RegularExpression(
				@".*\.(gif|jpe?g|bmp|png)$",
				ErrorMessage = "Imagem inválida (tipos aceitos: gif, jpeg, bmp, png).")
		]
		public string ImagemURL { get; set; }
		[
			Required,
			Phone(ErrorMessage = "O campo {0} está com número inválido.")
		]
		public string Telefone { get; set; }
		[
			Required(ErrorMessage = "O campo {0} é obrigatório."),
			EmailAddress,
			Display(Name = "e-mail")    // O nome mostrado no display torna-se este (o nome que é mostrado em {0} nos errors)
		]
		public string Email { get; set; }

		public IEnumerable<LoteDto> Lotes { get; set; }
		public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
		public IEnumerable<PalestranteDto> Palestrantes { get; set; }
	}
}