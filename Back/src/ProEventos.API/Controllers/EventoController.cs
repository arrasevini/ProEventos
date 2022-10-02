using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EventoController : ControllerBase
	{
		private readonly ILogger<EventoController> _logger;

		private IEnumerable<Evento> _eventos = new Evento[] {
				new Evento() {EventoId = 1, Tema = "Evento"},
				new Evento() {EventoId = 2, Tema = "Evento2"}
			};

		public EventoController()
		{
		}

		[HttpGet]
		public IEnumerable<Evento> Get()
		{
			return _eventos;
		}

		[HttpGet("{id}")]
		public IEnumerable<Evento> GetById(int id)
		{
			return _eventos.Where(a => a.EventoId == id);
		}

		[HttpPost]
		public string Post()
		{
			return "value";
		}

		[HttpPut("{id}")]
		public string Put(int id)
		{
			return "value";
		}

		[HttpDelete("{id}")]
		public string Delete(int id)
		{
			return "value";
		}
	}
}
