using CaseControl.Api.Interfaces;
using CaseControl.Api.TOKEN.DTOs; // Cambiado de Models a DTOs
using CaseControl.Api.TOKEN.Interfaces;
using CaseControl.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseControl.Api.TOKEN.Services
{
    public class TokenService : IToken
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IUser _users;

        public TokenService(IOptions<JWTSettings> jwtSettings, IUser users)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings), "La configuración JWT no puede ser nula.");
            _users = users ?? throw new ArgumentNullException(nameof(users), "El servicio de usuarios no puede ser nulo.");
        }

        public async Task<TokenResponse> GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo para generar el token JWT.");
            }

            var usuario = await _users.GetUserByIDAsync(user.ID);
            if (usuario == null)
            {
                throw new ArgumentException($"No se encontró un usuario con ID {user.ID}.");
            }

            if (string.IsNullOrEmpty(usuario.UserName))
            {
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(usuario.UserName));
            }

            if (string.IsNullOrEmpty(_jwtSettings.Key))
            {
                throw new ArgumentException("La clave secreta del JWT no está configurada en las opciones de configuración.");
            }

            var claims = new List<Claim>
            {
                new Claim("userId", usuario.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Employee?.Correo ?? "sin_correo"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes).ToLocalTime(),
                signingCredentials: signingCredentials
            );

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes).ToLocalTime(),
                User = usuario,
            };
        }

        public Claim? IsTokenValidation(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            if (string.IsNullOrEmpty(_jwtSettings.Key))
            {
                return null; // O lanzar una excepción si prefieres
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            try
            {
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidAudience = _jwtSettings.Audience,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return claims.Claims.FirstOrDefault(a => a.Issuer == _jwtSettings.Issuer);
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResponse?> RegenerateJwtToken(string token)
        {
            var valid = IsTokenValidation(token);

            if (valid != null)
            {
                var user = await _users.GetUserByIDAsync(Convert.ToInt32(valid.Value));
                if (user == null)
                {
                    return null;
                }
                return await GenerateJwtToken(user);
            }
            else
            {
                return null;
            }
        }

        public JwtSecurityToken DeserializeToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var jwtTokenhandler = new JwtSecurityTokenHandler();
            return jwtTokenhandler.ReadJwtToken(token);
        }
    }
}