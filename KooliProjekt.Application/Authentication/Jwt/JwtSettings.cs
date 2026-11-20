using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace KooliProjekt.Application.Authentication.Jwt
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }

        public JwtSettings(IConfiguration config)
        {
            var jwtSection = config.GetSection("Jwt");
            var jwtKey = Encoding.UTF8.GetBytes(jwtSection["Key"]);

            Key = jwtSection["Key"];
            Issuer = jwtSection["Issuer"];
            Audience = jwtSection["Audience"];
            ExpireMinutes = int.Parse(jwtSection["ExpireMinutes"] ?? "60");
        }
    }
}