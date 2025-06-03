using CaseControl.DATA;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CaseControl.Domain.DTOs;
using CaseControl.Api.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CaseControl.Api.Helpers
{
    public class UtilService : IUtil
    {
        private readonly DataContext _context;

        public UtilService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<vwOficinas>> GetAllOfficesAsync()
        {
            return await _context.VwOficinas
                .ToListAsync();
        }

        public async Task<List<vwCostCenter>> GetAllCostCentersAsync()
        {
            return await _context.VwCostCenters
                .ToListAsync();
        }

        public async Task<vwEmployee> GetEmployeeByCodeIdentAsync(string ident)
        {
            ident = ident.Trim().Replace("-", "");
            var emp = await _context.vwEmployees
                .Where(a => a.Codigo == ident || a.Identificacion == ident)
                .FirstOrDefaultAsync();

            return emp!;
        }

        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        //public string generarJWT(User modelo)
        //{
        //    //crear la informacion del usuario para token
        //    var userClaims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, modelo.UserName.ToString()),
        //        new Claim(ClaimTypes., modelo.Correo!)
        //    };

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //    //crear detalle del token
        //    var jwtConfig = new JwtSecurityToken(
        //        claims: userClaims,
        //        expires: DateTime.UtcNow.AddMinutes(10),
        //        signingCredentials: credentials
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        //}

    }
}
