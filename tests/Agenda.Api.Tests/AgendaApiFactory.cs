using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Agenda.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Api.Tests;

public class AgendaApiFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection conexao;
    public AgendaApiFactory()
    {

        conexao = new SqliteConnection("Data Source=:memory:");
        conexao.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<AgendaDbContext>();
            services.RemoveAll<DbContextOptions<AgendaDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<AgendaDbContext>>();
            services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlite(conexao));
        });
    }

    public void InicializarBanco()
    {
        using var escopo = Services.CreateScope();

        var contexto = escopo.ServiceProvider
            .GetRequiredService<AgendaDbContext>();

        contexto.Database.Migrate();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            conexao.Dispose();
        }
    }
}