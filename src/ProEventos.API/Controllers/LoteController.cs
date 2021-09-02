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
  public class LoteController : ControllerBase
  {
    private readonly ILoteService _loteService;

    public LoteController(ILoteService loteService)
    {
      _loteService = loteService;

    }

    // IActionResult permite retornar status code do http 
    [HttpGet("{eventoId}")]
    public async Task<IActionResult> Get(int eventoId)
    {
      try
      {
          var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
          if(lotes == null) return NoContent();

          return Ok(lotes);
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar lotes. Erro: {error.Message}");
      }
    }

    [HttpPut("{eventoId}")]
    public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
    {
        try
        {
            var lotes = await _loteService.SaveLotes(eventoId, models);
            if (lotes == null) return NoContent();
            return Ok(lotes);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar salvar lotes. Erro: {ex.Message}");
        }
    }

    [HttpDelete("{eventoId}/{loteId}")]
    public async Task<IActionResult> Delete(int eventoId, int loteId)
    {
      try
      {
          var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
          if(lote == null) return NoContent();

          return await _loteService.DeleteLote(lote.EventoId, lote.Id) 
            ? Ok("Lote deletado") 
            : throw new Exception("Ocorreu um problema não específico ao tentar deletar lote.");
      }
      catch (Exception error)
      {
          return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deltear lote. Erro: {error.Message}");
      }
    }
  }
}
