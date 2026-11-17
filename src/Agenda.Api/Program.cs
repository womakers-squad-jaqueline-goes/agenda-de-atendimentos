using Agenda.Api.Middlewares;
using Agenda.Api.Data;
using Microsoft.EntityFrameworkCore;
using Agenda.Api.Repositories;
using Agenda.Api.Models;
using Microsoft.AspNetCore.Identity;
using Agenda.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AgendaDbContext>(options =>
options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
builder.Services.AddScoped<IPasswordHasher<Profissional>, PasswordHasher<Profissional>>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();