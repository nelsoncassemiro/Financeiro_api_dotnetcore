using Entities.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuracao
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase( DbContextOptions options ) : base (options)
        {

        }

        public DbSet<SistemaFinanceiro> SistemaFinanceiro { get; set; }
        public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Despesa> Despesa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        public string ObterStringConexao()
        {
            //return "Data Source=localhost;Initial Catalog=FINANCEIRO_2024;Integrated Security=True";
            return "Data source=localhost;database=FINANCEIRO_2024;Trusted_connection=true; Encrypt=false; TrustServerCertificate=true";
        }
    


    }
}
