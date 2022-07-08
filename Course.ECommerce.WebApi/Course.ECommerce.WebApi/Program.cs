using Course.ECommerce.Aplication;
using Course.ECommerce.Domain;
using Course.ECommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
