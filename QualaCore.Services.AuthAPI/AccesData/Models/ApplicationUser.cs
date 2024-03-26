using Microsoft.AspNetCore.Identity;

namespace QualaCore.Services.AuthAPI.AccesData.Models
{
    /// <summary>
    /// Representa un usuario de la aplicación.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Name { get; set; }
    }
}
