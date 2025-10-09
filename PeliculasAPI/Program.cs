using PeliculasAPI;
using PeliculasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//servicio para la cadena de conexion
var config = builder.Configuration;
var cadenaConexionsql = new ConexionBD(config.GetConnectionString("SQL"));
builder.Services.AddSingleton(cadenaConexionsql);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuracion AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Servicio para la inyeccion de depenedencias
builder.Services.AddSingleton<IRepositorio ,RepositorioEnMemoria>();

//obtenemos los origenes permitidos defindos en el appsettings
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

//configuracion de CORS para que acepte accesos de otras fuentes externas
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        //permite que cualquiera se comunique con la API
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("cantidad-total-registros");//exponer la cabecera de catindad-total-registros
    });
});

builder.Services.AddSingleton<IServicioGenero, ServicioGenero>();
builder.Services.AddSingleton<IServicioActor, ServicioActor>();
//servicio para almacenador de archivos local
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//middleware para archivos estaticos
app.UseStaticFiles();

//indicamos que se usara el CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
