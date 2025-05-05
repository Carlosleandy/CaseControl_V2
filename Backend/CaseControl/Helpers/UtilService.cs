using CaseControl.DATA;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    }
}
