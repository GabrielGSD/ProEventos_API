using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        //  EVENTOS:

        /// <summary>
        /// Método Get que retornará uma lista de Lotes por eventoId.
        /// </summary>
        /// <param name="eventoId">Código chave para tabela Evento</param>
        /// <returns>Lista de Lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        /// Metódo Get que retornará apenas um lote
        /// </summary>
        /// <param name="eventoId">Código chave para tabela Evento</param>
        /// <param name="id">Código chave da tabela Lote</param>
        /// <returns>Apenas um lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}