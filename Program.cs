using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

//var mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST");
//var mysqlDatabase = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
//var mysqlUser = Environment.GetEnvironmentVariable("MYSQL_USER");
//var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
//var connectionString = $"server={mysqlHost};database={mysqlDatabase};uid={mysqlUser};pwd={mysqlPassword}";


// Definir las credenciales de la base de datos de forma fija
var mysqlHost = "localhost";
var mysqlDatabase = "Restaurante_DB";
var mysqlUser = "root";
var mysqlPassword = "P@ssWord.123";
var connectionString = $"server={mysqlHost};port=3306;database={mysqlDatabase};uid={mysqlUser};pwd={mysqlPassword}";
//Construir el ConnectionString para MySQL (incluyendo el puerto 3306)




var hondurasTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
TimeZoneInfo.ClearCachedData();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-HN");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-HN");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContextInventario>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://carwash-front-end.vercel.app")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbContextInventario>();

    try
    {
        Console.WriteLine("Aplicando migraciones...");
        context.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente.");

        // Ejecutar el seeder para poblar datos (si es necesario)
        //var seeder = new Seeder(context, true);
        //seeder.Seed();
        Console.WriteLine("Seeding completo.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al aplicar migraciones o al ejecutar el seeder: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseIpRateLimiting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () =>
{
    var utcNow = DateTime.UtcNow;
    var hondurasTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, hondurasTimeZone);

    return Results.Ok(new
    {
        message = "API Restaurante levantada correctamente",
        utcDate = utcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
        localDate = hondurasTime.ToString("yyyy-MM-ddTHH:mm:ss")
    });
});
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
