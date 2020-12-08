using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Models
{
    public class InfotecContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public InfotecContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<ClientesOmni> ClientesOmni { get; set; }
        public DbSet<ClientesOmniMsg> ClientesOmniMsg { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        
        public DbSet<CampanhasClientes> CampanhasClientes { get; set; }
        public DbSet<Operadores> Operadores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sconexao = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySQL(sconexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.CODIGO);
                entity.Property(e => e.CODIGO).IsRequired();
            });
            
            modelBuilder.Entity<CampanhasClientes>(entity =>
            {
                entity.HasKey(e => e.CODIGO);
                entity.Property(e => e.CODIGO).IsRequired();
            });
            
            modelBuilder.Entity<Operadores>(entity =>
            {
                entity.HasKey(e => e.CODIGO);
                entity.Property(e => e.CODIGO).IsRequired();
            });

            modelBuilder.Entity<ClientesOmni>(entity =>
            {
                entity.HasKey(e => e.talk_id);
                entity.Property(e => e.customer_id).IsRequired();
            });

            modelBuilder.Entity<ClientesOmniMsg>(entity =>
            {
                entity.HasKey(e => e.id);
            });
        }
    }
}