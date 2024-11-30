using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;
using TopSecretNicaAPICore.Repository;
using Microsoft.EntityFrameworkCore;
using TopSecretNicaAPICore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Definir la política CORS
var misReglasCores = "misReglasCores";

// Agregar CORS (Con la política definida)
builder.Services.AddCors(options =>
{
    options.AddPolicy(misReglasCores, builder =>
    {
        builder
            .WithOrigins("http://localhost:3000", "http://192.168.0.2:3000") // Asegúrate de que el frontend esté en este origen
            .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, PUT, DELETE)
            .AllowAnyHeader(); // Permitir cualquier encabezado
    });
});


// Configurar el DbContext para que use la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"))
);

// Registrar el repositorio
builder.Services.AddScoped<IMarcasRepository, MarcasRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar CORS antes de otras configuraciones, como UseAuthorization y MapControllers
app.UseCors(misReglasCores); // Aquí se aplica CORS

// Usar Swagger solo en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar HTTPS, redirigiendo HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar la autorización si es necesario
app.UseAuthorization();

// Mapear los controladores de la API
app.MapControllers();

// Ejecutar la aplicación
app.Run();
