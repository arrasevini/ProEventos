using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

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
				if (eventos == null) return NoContent();
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
				if (evento == null) return NoContent();
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
				if (eventos == null) return NoContent();
				return Ok(eventos);
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos.<br>Erro: {error}");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(EventoDto model)
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
		public async Task<IActionResult> Put(int id, EventoDto model)
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
				var evento = await _eventoService.GetEventoByIdAsync(id, true);
				if (evento == null) return NoContent();

				if (await _eventoService.DeleteEvento(id)) return Ok(new { message = "Deletado" });
				else return BadRequest("Evento não deletado");
			}
			catch (Exception error)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar evento.<br>Erro: {error}");
			}
		}
	}
}
