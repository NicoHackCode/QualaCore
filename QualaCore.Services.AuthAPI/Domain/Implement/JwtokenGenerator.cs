using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QualaCore.Services.AuthAPI.AccesData.Models;
using QualaCore.Services.AuthAPI.Domain.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QualaCore.Services.AuthAPI.Domain.Implement
{
    /// <summary>
    /// Clase que genera tokens JWT.
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="JwtTokenGenerator"/>.
        /// </summary>
        /// <param name="jwtOptions">Las opciones de configuración JWT.</param>
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Genera un token JWT para el usuario de la aplicación.
        /// </summary>
        /// <param name="applicationUser">El usuario de la aplicación.</param>
        /// <param name="roles">Los roles del usuario.</param>
        /// <returns>El token JWT generado.</returns>
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimsList = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                    new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName.ToString()),
                };

            claimsList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
