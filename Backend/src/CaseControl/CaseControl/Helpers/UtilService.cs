// modificado por el Pasante Carlos Leandy Moreno Reyes (Alea: EL Varon)
using CaseControl.DATA;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CaseControl.Domain.DTOs;
using CaseControl.Api.Interfaces;
using CaseControl.Api.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace CaseControl.Api.Helpers
{
    public class UtilService : IUtil, IDbUtil
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

        public async Task<string> encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = await Task.Run(() => sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto)));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}