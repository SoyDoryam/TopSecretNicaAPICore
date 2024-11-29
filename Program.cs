var builder = WebApplication.CreateBuilder(args);

// Definir la política CORS
var misReglasCores = "misReglasCores";

// Agregar CORS (Con la política definida)
builder.Services.AddCors(options =>
{
    options.AddPolicy(misReglasCores, builder =>
    {
        builder
            .WithOrigins("http://localhost:3000") // Asegúrate de que el frontend esté en este origen
            .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, PUT, DELETE)
            .AllowAnyHeader(); // Permitir cualquier encabezado
    });
});

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
