using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EventoController : ControllerBase
  {
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
      _eventoService = eventoService;

    }

    // IActionResult permite retornar status code do http 
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
          var eventos = await _eventoService.GetAllEventosAsync(true);
          if(eventos == null) return NoContent();

          return Ok(eventos);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
          var evento = await _eventoService.GetEventoByIdAsync(id, true);
          if(evento == null) return NoContent();
          return Ok(evento);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
      }
    }

    [HttpGet("tema/{tema}")]
    public async Task<IActionResult> GetByTema(string tema)
    {
      try
      {
          var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
          if(evento == null) return NoContent();
          return Ok(evento);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
          var evento = await _eventoService.AddEvento(model);
          if(evento == null) return BadRequest("Erro ao tentar adicionar evento.");
          return Ok(evento);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {error.Message}");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EventoDto model)
    {
      try
      {
          var evento = await _eventoService.UpdateEvento(id, model);
          if(evento == null) return BadRequest("Erro ao tentar atualizar evento.");
          return Ok(evento);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {error.Message}");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
          var evento = await _eventoService.GetEventoByIdAsync(id, true);
          if(evento == null) return NoContent();

          return await _eventoService.DeleteEvento(id) 
            ? Ok("Deletado") 
            : throw new Exception("Ocorreu um problema não específico ao tentar deletar Evento.");
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deltear evento. Erro: {error.Message}");
      }
    }
  }
}
