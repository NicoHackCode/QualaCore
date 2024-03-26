using QualaCore.Services.AuthAPI.AccesData.Models;

namespace QualaCore.Services.AuthAPI.Domain.Interface
{
    /// <summary>
    /// Interfaz para generar tokens JWT.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Genera un token JWT para el usuario de la aplicación.
        /// </summary>
        /// <param name="applicationUser">El usuario de la aplicación.</param>
        /// <param name="roles">Los roles del usuario.</param>
        /// <returns>El token JWT generado.</returns>
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
