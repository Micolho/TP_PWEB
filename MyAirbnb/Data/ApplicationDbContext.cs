using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Models;

namespace MyAirbnb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<DoneChecklist> DoneChecklists { get; set; }
        public DbSet<Imagens> Imagens { get; set; }
        public DbSet<Classificacao> Classificacaos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

    }
}
