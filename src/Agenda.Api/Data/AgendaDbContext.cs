using Microsoft.EntityFrameworkCore;
using Agenda.Api.Models;

namespace Agenda.Api.Data;

public class AgendaDbContext : DbContext
{
    public AgendaDbContext(DbContextOptions<AgendaDbContext> options)
     : base(options) { }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servico> Servicos {get; set;}
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Profissional>()
        .HasIndex(profissional => profissional.Email)
        .IsUnique();

        modelBuilder.Entity<Cliente>()
        .HasOne<Profissional>()
        .WithMany()
        .HasForeignKey(cliente => cliente.ProfissionalId);

        modelBuilder.Entity<Servico>()
        .HasOne<Profissional>()
        .WithMany()
        .HasForeignKey(servico => servico.ProfissionalId);

        modelBuilder.Entity<Expediente>()
        .HasOne<Profissional>()
        .WithMany()
        .HasForeignKey(expediente => expediente.ProfissionalId);

        modelBuilder.Entity<Expediente>()
        .HasIndex(expediente => new
        {
            expediente.ProfissionalId,
            expediente.DiaDaSemana
        })
        .IsUnique();

        modelBuilder.Entity<Agendamento>()
        .HasOne<Profissional>()
        .WithMany()
        .HasForeignKey(agendamento => agendamento.ProfissionalId);

        modelBuilder.Entity<Agendamento>()
        .HasOne<Cliente>()
        .WithMany()
        .HasForeignKey(agendamento => agendamento.ClienteId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Agendamento>()
        .HasOne<Servico>()
        .WithMany()
        .HasForeignKey(agendamento => agendamento.ServicoId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Agendamento>()
        .HasIndex( agendamento => new
        {
            agendamento.ProfissionalId,
            agendamento.Inicio
        });

    }
}

