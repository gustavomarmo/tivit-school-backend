using edu_connect_backend.Configuration;
using edu_connect_backend.Context;
using edu_connect_backend.Repository;
using edu_connect_backend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConnectionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCorsConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AlunoRepository>();
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ProfessorRepository>();
builder.Services.AddScoped<ProfessorService>();
builder.Services.AddScoped<AcademicoRepository>();
builder.Services.AddScoped<AcademicoService>();
builder.Services.AddScoped<DashboardRepository>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<NotaRepository>();
builder.Services.AddScoped<NotaService>();
builder.Services.AddScoped<NotificacaoRepository>();
builder.Services.AddScoped<NotificacaoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
