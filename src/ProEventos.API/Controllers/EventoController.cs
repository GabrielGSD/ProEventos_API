using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
    private readonly IWebHostEnvironment _hostEnvironment;

    public EventoController(IEventoService eventoService, IWebHostEnvironment hostEnvironment)
    {
      _eventoService = eventoService;
      _hostEnvironment = hostEnvironment;
    }

    // IActionResult permite retornar status code do http 
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
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
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
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
      }
    }

    [HttpGet("tema/{tema}")]
    public async Task<IActionResult> GetByTema(string tema)
    {
      try
      {
        var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
        if (evento == null) return NoContent();
        return Ok(evento);
      }
      catch (Exception error)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {error.Message}");
      }
    }

    [HttpPost("upload-image/{eventoId}")]
    public async Task<IActionResult> UploadImage(int eventoId)
    {
      try
      {
        var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
        if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

        var file = Request.Form.Files[0];
        if (file.Length > 0)
        {
          DeleteImage(evento.ImagemURL);
          evento.ImagemURL = await SaveImage(file);
        }
        var eventoRetorno = await _eventoService.UpdateEvento(eventoId, evento);

        return Ok(eventoRetorno);
      }
      catch (Exception error)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar imagem. Erro: {error.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
        var evento = await _eventoService.AddEvento(model);
        if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");
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
        if (evento == null) return BadRequest("Erro ao tentar atualizar evento.");
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
        if (evento == null) return NoContent();

        if(await _eventoService.DeleteEvento(id)){
          DeleteImage(evento.ImagemURL);
          return Ok("Deletado");
        }
        else {
          throw new Exception("Ocorreu um problema não específico ao tentar deletar Evento.");
        }
      }
      catch (Exception error)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deltear evento. Erro: {error.Message}");
      }
    }

    [NonAction]
    public async Task<string> SaveImage(IFormFile imageFile) {
      string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
      imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

      var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

      using(var fileStream = new FileStream(imagePath, FileMode.Create)){
        await imageFile.CopyToAsync(fileStream);
      }

      return imageName;
    }

    [NonAction]
    public void DeleteImage(string imageName) {
      var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
      if(System.IO.File.Exists(imagePath)){
        System.IO.File.Delete(imagePath);
      }
    }


  }
}
