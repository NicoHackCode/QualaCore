using System.Reflection.Metadata.Ecma335;

namespace QualaCore.Services.AuthAPI.AccesData.DTO
{
    /// <summary>
    /// Representa los datos de solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequestDTO
    {
        
        public string UserName { get; set; } = string.Empty;        
        public string Password { get; set; } = string.Empty;
    }
}
