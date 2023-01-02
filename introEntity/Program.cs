using introEntity;
using introEntity.Repositorios;
using introEntity.UoW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=SQL"));

    builder.Services.AddTransient<IPeliculaRepository, PeliculaRepository>();
    builder.Services.AddTransient<IGeneroRepository, GeneroRepository>();
    builder.Services.AddTransient<IComentarioRepository, ComentarioRepository>();
    builder.Services.AddTransient<IActoresRepository, ActorRepository>();
    builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o=>
        {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            //ValidIssuer = builder.Configuration["JWT:Issuer"],
            //ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

    builder.Services.AddAuthorization(options => {
        options.AddPolicy("Administrador", policy => policy.RequireClaim("RolType", "admin"));
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    logger.Error(e,"excepcion en la ejecucion");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}