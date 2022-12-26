using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
	//[Table(name: "evento")] Para colocar o nome diferente da table no banco de dados
	public class Evento
	{
		public int Id { get; set; }
		// [NotMapped] O NotMapped evita do item ser criado no banco de dados durante a migração, geralmente quando o item é necessário apenas para regra de negócio da aplicação, mas que não é necessário te-lo como coluna no banco de dados.
		[Required] // faz criar "not null" no banco de dados
		public string Local { get; set; }
		public DateTime? DataEvento { get; set; }
		public string Tema { get; set; }
		public int QtdPessoas { get; set; }
		public string Lote { get; set; }
		public string ImagemURL { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }

		public IEnumerable<Lote> Lotes { get; set; }
		public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
	}
}