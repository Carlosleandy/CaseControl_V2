using CaseControl.Api.Interfaces;
using CaseControl.Api.TOKEN.DTOs;
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
        //private readonly IAuditAreas _auditAreas;

        public TokenService(IOptions<JWTSettings> jwtSettings, IUser users) //, IAuditAreas auditAreas)
        {
            _jwtSettings = jwtSettings.Value;
            _users = users;
            
        }

        //Metodo que genera el token
        public async Task<TokenResponse> GenerateJwtToken(User user) //Recibe un objeto con los datos del usuario autenticado.
        {
            var usuario = await _users.GetUserByIDAsync(user.ID);
            //var accesos = await _users.GetAccessByUserAsync(usuario.ID);
            //var area = await _auditAreas.GetAuditAreaByUserNameAsync(usuario.UserName);

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                    new Claim("userId", usuario.ID.ToString()), //Agrega el Id del usuario.
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName!), //Agrega el nombre del usuario.
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Employee!.Correo!),//Extra...                    
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Identificador unico del Token ID
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes).ToLocalTime(),
               signingCredentials: signingCredentials
               );

            //return tokenHandler.WriteToken(jwtToken); //Retorna el token creado.

            return new TokenResponse
            {
                Token = tokenHandler.WriteToken(jwtToken),
                Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes).ToLocalTime(),
                User = usuario,
        
            };
        }

        //Metodo que verifica si el token es valido
        public Claim? IsTokenValidation(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
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
                return await GenerateJwtToken(user);
            }
            else
            {
                return null;
            }
        }


        //Metodo para decodificar o deserializar el token.
        public JwtSecurityToken DeserializeToken(string token)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            return jwtTokenhandler.ReadJwtToken(token); //Retorna el token decodificado.          
        }


    }
}
