using Microsoft.EntityFrameworkCore;
using MyAirbnb.Models;

namespace MyAirbnb.Data
{
    public class AirbnbDbContext : DbContext
    {
        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options)
        {
        }

        public DbSet<Imovel> Imoveis { get; set; }
        
        public DbSet<Reserva> Reservas { get; set; }

    }
}
