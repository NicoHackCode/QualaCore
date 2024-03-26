namespace QualaCore.Services.AuthAPI.AccesData.Models
{
    /// <summary>
    /// Clase que representa las opciones de JWT.
    /// </summary>
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;        
        public string Issuer { get; set; } = string.Empty;        
        public string Audience { get; set; } = string.Empty;
    }
}
