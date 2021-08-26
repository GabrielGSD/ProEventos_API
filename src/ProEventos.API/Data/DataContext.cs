using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext
    {
        //:base é para passar o parametro para o pai (DbContext)
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Evento> Eventos {get; set;}
    }
}