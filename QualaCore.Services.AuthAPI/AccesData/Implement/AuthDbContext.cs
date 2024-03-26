using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QualaCore.Services.AuthAPI.AccesData.Models;

namespace QualaCore.Services.AuthAPI.AccesData.Implement
{
    /// <summary>
    /// Representa el contexto de base de datos para la autenticación.
    /// </summary>
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AuthDbContext"/>.
        /// </summary>
        /// <param name="options">Las opciones de configuración del contexto.</param>
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Obtiene o establece el conjunto de entidades de usuarios de la aplicación.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Se llama para configurar el modelo de datos utilizado por el contexto.
        /// </summary>
        /// <param name="modelBuilder">El generador de modelos que se utiliza para construir el modelo de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
