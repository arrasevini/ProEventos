using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EventosController : ControllerBase
	{
		private readonly IEventoService _eventoService;

		public EventosController(IEventoService eventoService)
		{
			_eventoService = eventoService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var eventos = await _eventoService.GetAllEventosAsync(true);
				if (eventos == null) return NotFound("Nenhum evento encontrado.");
				return Ok(eventos);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos.<br>Erro: {error}");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var evento = await _eventoService.GetEventoByIdAsync(id, true);
				if (evento == null) return NotFound("Nenhum evento encontrado com o id informado.");
				return Ok(evento);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento.<br>Erro: {error}");
			}
		}

		[HttpGet("tema/{tema}")]
		public async Task<IActionResult> GetByTema(string tema)
		{
			try
			{
				var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
				if (eventos == null) return NotFound("Nenhum evento encontrado com o tema informado.");
				return Ok(eventos);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos.<br>Erro: {error}");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(Evento model)
		{
			try
			{
				var evento = await _eventoService.AddEvento(model);
				if (evento == null) return BadRequest("Não foi possível adicionar evento.");
				return Ok(evento);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar evento.<br>Erro: {error}");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Evento model)
		{
			try
			{
				var evento = await _eventoService.UpdateEvento(id, model);
				if (evento == null) return BadRequest("Não foi possível atualizar evento.");
				return Ok(evento);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento.<br>Erro: {error}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				if(await _eventoService.DeleteEvento(id)) return Ok("Deletado");
				else return BadRequest("Evento não deletado");
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento.<br>Erro: {error}");
			}
		}
	}
}
