namespace QualaCore.Services.AuthAPI.AccesData.DTO
{
    /// <summary>
    /// Representa la respuesta de inicio de sesión.
    /// </summary>
    public class LoginResponseDTO
    {
        public required UserDTO User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
