using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
	public class EventoService : IEventoService
	{
		private readonly IGeralPersist _geralPersist;
		private readonly IEventoPersist _eventoPersist;

		public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
		{
			_geralPersist = geralPersist;
			_eventoPersist = eventoPersist;
		}
		public async Task<Evento> AddEvento(Evento model)
		{
			try
			{
				_geralPersist.Add<Evento>(model);
				if (await _geralPersist.SaveChangesAsync())
				{
                    return await _eventoPersist.GetEventoByIdAsync(model.Id);
				}
                return null;
			}
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}

		public async Task<Evento> UpdateEvento(int eventoId, Evento model)
		{
			try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);
                if (evento == null) return null;

                model.Id = eventoId;

                _geralPersist.Update<Evento>(model);
				if (await _geralPersist.SaveChangesAsync())
				{
                    return await _eventoPersist.GetEventoByIdAsync(model.Id);
				}
                return null;
            }
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}

		public async Task<bool> DeleteEvento(int eventoId)
		{
			try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId);
                if (evento == null) throw new Exception("O evento a ser deletado não foi encontrado.");

                _geralPersist.Delete<Evento>(evento);
				return await _geralPersist.SaveChangesAsync();
            }
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}

		public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
		{
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) throw new Exception("Nenhum evento encontrado.");
                return eventos;
            }
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}

		public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
		{
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) throw new Exception("Nenhum evento encontrado.");
                return eventos;
            }
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}

		public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
		{
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) throw new Exception("Nenhum evento encontrado.");
                return evento;
            }
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		}
	}
}