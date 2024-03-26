using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QualaCore.Services.BranchOfficeAPI.AccessData.Implement;
using QualaCore.Services.BranchOfficeAPI.AccessData.Interface;
using QualaCore.Services.BranchOfficeAPI.Domain.Implements;
using QualaCore.Services.BranchOfficeAPI.Domain.Interface;
using QualaCore.Services.BranchOfficeAPI.Domain.Mappers;
using QualaCore.Services.BranchOfficeAPI.Extension;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS para permitir orígenes específicos
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:4200", "http://127.0.0.1:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Agregar servicios al contenedor.

builder.Services.AddDbContext<BranchOfficeContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuración de DI para mi Tres Capas
builder.Services.AddScoped<IBranchOfficeContext, BranchOfficeContext>();
builder.Services.AddScoped<IBranchOfficeBl, BranchOfficeBl>();
builder.Services.AddAutoMapper(typeof(MappingBranchOffice));

builder.Services.AddControllers();
// Obtenga más información sobre la configuración de Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese la cadena de autorización Bearer de la siguiente manera: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[] { }
        }
    });
});

builder.AddAppAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar la política CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<BranchOfficeContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
