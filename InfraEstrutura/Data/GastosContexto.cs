using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Data
{
    public class GastosContexto:DbContext
    {
        public GastosContexto(
            DbContextOptions<GastosContexto> options):base(options) { 
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuário -> Transações (1:N)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Transacoes)
                .WithOne(t => t.Usuario)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuário -> Orçamentos (1:N)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Orcamentos)
                .WithOne(o => o.Usuario)
                .HasForeignKey(o => o.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Categoria -> Transações (1:N)
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Categoria)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orcamento>()
                .HasMany(o => o.Transacoes)
                .WithOne(t => t.Orcamento)
                .HasForeignKey(t => t.OrcamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurações extras
            modelBuilder.Entity<Transacao>()
                .Property(t => t.Valor)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nome)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .HasMaxLength(100)
                .IsRequired();
        }

    }
}
