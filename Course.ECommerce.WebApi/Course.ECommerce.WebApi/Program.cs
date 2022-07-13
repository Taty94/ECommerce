using Course.ECommerce.Aplication;
using Course.ECommerce.Domain;
using Course.ECommerce.Infrastructure;
using Course.ECommerce.WebApi.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


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

//1.Configuracin de Politicas
builder.Services.AddAuthorization(options =>
{
    //1.Politica para verificar claim
    //options.AddPolicy("EsEcuatoriano", policy => policy.RequireClaim("Ecuatoriano"));
    options.AddPolicy("EsEcuatoriano", policy => policy.RequireClaim("Ecuatoriano", true.ToString()));
    options.AddPolicy("EsAdminEc", policy =>
    {
        policy.RequireClaim("Ecuatoriano", true.ToString());
        policy.RequireRole("Admin");
    });
});


builder.Services.AddDomain(builder.Configuration);
builder.Services.AddAplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

//Inyectar o configurar las dependencias
//IRepositoryCatalogue = RepositoryCatalogue
//IApplicationCatalogue = ApplicationCatalogue

//Asp.net (es el tercer actor quien crea los objetos)
//Configurar las dependencias. Se lo realiza con IServiceCollection
//Forma. Generic
//REPOSITORIOS
//builder.Services.AddTransient<ICatalogueRepository, CatalogueRepository>();
//builder.Services.AddTransient<IProductRepository, ProductRepository>();
//builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
//builder.Services.AddTransient<IProductBrandRepository, ProductBrandRepository>();
//REPOSITORIO GENERICO
//builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddTransient<IClientRepository, ClientRepository>();
//Forma. Methods
//SERVICIOS DE APLICACION
//builder.Services.AddTransient(typeof(ICatalogueApplication),typeof(CatalogueApplication));
//builder.Services.AddTransient(typeof(IProductApplication),typeof(ProductApplication));
//builder.Services.AddTransient(typeof(IProductTypeApplication),typeof(ProductTypeApplication));
//builder.Services.AddTransient(typeof(IProductBrandApplication),typeof(ProductBrandApplication));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//2. registra el middleware que usa los esquemas de autenticación registrados
//el middleware autentificacion debe estar antes de cualquier componente que requiere autentificacion
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
