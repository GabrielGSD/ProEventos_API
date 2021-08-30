using System;
using System.Collections.Generic;

namespace ProEventos.Domain
{
  public class Evento
  {
    public int Id { get; set; }
    public string Local { get; set; }
    // o ? no DateTime é para informar que esse dado pode ser null
    public DateTime? DataEvento { get; set; }
    public string Tema { get; set; }
    public int QdtPessoas { get; set; }
    public string ImagemURL { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public IEnumerable<Lote> Lotes { get; set; }
    public IEnumerable<RedeSocial> RedesSociais { get; set; }
    public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
  }
}