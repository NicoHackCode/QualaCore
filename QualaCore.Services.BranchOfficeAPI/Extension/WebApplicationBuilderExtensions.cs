﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace QualaCore.Services.BranchOfficeAPI.Extension
{
    /// <summary>
    /// Clase de extensión para la configuración de autenticación de la aplicación.
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Agrega la autenticación de la aplicación.
        /// </summary>
        /// <param name="builder">El objeto WebApplicationBuilder.</param>
        /// <returns>El objeto WebApplicationBuilder con la autenticación agregada.</returns>
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("ApiSettings");

            var secret = settingsSection.GetValue<string>("Secret");
            var issuer = settingsSection.GetValue<string>("Issuer");
            var audience = settingsSection.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;

        }
    }
}
