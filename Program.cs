using edu_connect_backend.Configuration;
using edu_connect_backend.Context;
using edu_connect_backend.Exceptions;
using edu_connect_backend.Mapper;
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

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddCorsConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioMapper>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AlunoRepository>();
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<AlunoMapper>();
builder.Services.AddScoped<ProfessorRepository>();
builder.Services.AddScoped<ProfessorService>();
builder.Services.AddScoped<ProfessorMapper>();
builder.Services.AddScoped<TopicoService>();
builder.Services.AddScoped<DashboardRepository>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<NotaRepository>();
builder.Services.AddScoped<NotaService>();
builder.Services.AddScoped<NotaMapper>();
builder.Services.AddScoped<NotificacaoRepository>();
builder.Services.AddScoped<NotificacaoService>();
builder.Services.AddScoped<FrequenciaRepository>();
builder.Services.AddScoped<FrequenciaMapper>();
builder.Services.AddScoped<FrequenciaService>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<EventoMapper>();
builder.Services.AddScoped<BoletimPdfService>();
builder.Services.Configure<EduConnectVariables>(
    builder.Configuration.GetSection("edu-connect-variables"));
builder.Services.AddScoped<AtividadeService>();
builder.Services.AddScoped<AtividadeRepository>();
builder.Services.AddScoped<AtividadeMapper>();
builder.Services.AddScoped<DisciplinaService>();
builder.Services.AddScoped<DisciplinaRepository>();
builder.Services.AddScoped<DisciplinaMapper>();;
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<MaterialRepository>();
builder.Services.AddScoped<MaterialMapper>();
builder.Services.AddScoped<TopicoService>();
builder.Services.AddScoped<TopicoRepository>();
builder.Services.AddScoped<TopicoMapper>();
builder.Services.AddScoped<TurmaService>();
builder.Services.AddScoped<TurmaRepository>();
builder.Services.AddScoped<TurmaMapper>();
builder.Services.AddScoped<MatriculaRepository>();
builder.Services.AddScoped<MatriculaService>();
builder.Services.AddScoped<MatriculaMapper>();
builder.Services.AddScoped<BlobService>();
builder.Services.AddHttpClient<AiService>();
builder.Services.AddScoped<AiService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
