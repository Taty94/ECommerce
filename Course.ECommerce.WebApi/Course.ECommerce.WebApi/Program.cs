using Course.ECommerce.Aplication;
using Course.ECommerce.Domain;
using Course.ECommerce.Infrastructure;
using Course.ECommerce.WebApi.Classes;
using Course.ECommerce.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //Aplicar filter globalmente a todos los controller
    options.Filters.Add<ApiExceptionFilterAttribute>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Authentication
//1. Configurar el esquema de Autentificacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

//configuracion el option para el token controller
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDomain(builder.Configuration);
builder.Services.AddAplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

// Add CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Politica global CORS Middleware
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // Permitir cualquier origen
    .AllowCredentials());

//2. registra el middleware que usa los esquemas de autenticación registrados
//el middleware autentificacion debe estar antes de cualquier componente que requiere autentificacion
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
