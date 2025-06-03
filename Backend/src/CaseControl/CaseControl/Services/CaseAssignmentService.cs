using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CaseControl.Domain.DTOs;

namespace CaseControl.Api.Services
{
    public class CaseAssignmentService : ICaseAssignment
    {
        private readonly DataContext _context;

        public CaseAssignmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CaseAssignment>> GetAllCaseAssignmentAsync(PaginationDTO pagination)
        {
             var queryable = _context.CaseAssignments
                .Include(a => a.Case)
                .Include(a => a.User)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.userNameRegistered!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.userNameRegistered)
                        .ToListAsync();
        }


        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.CaseAssignments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.userNameRegistered!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }


        public async Task<CaseAssignment> GetCaseAssignmentByIDAsync(int id)
        {
            var ass = await _context.CaseAssignments
            .Include(a => a.Case)
            .Include(a => a.User)
            .Where(a => a.ID == id)
             .FirstOrDefaultAsync();

            return ass!;
        }


        public async Task<CaseAssignment> AddCaseAssignmentAsync(CaseAssignment model)
        {
            model.UserID = 1;
            model.DateRegistered = DateTime.Now;

            // Break circular references
            model.User = null;
            model.Case = null;

            _context.CaseAssignments.Add(model);
            await _context.SaveChangesAsync();
            return model;

        }

        public async Task<CaseAssignment> EditCaseAssignmentAsync(CaseAssignment model)
        {
            model.User = null;
            model.Case = null;

            _context.CaseAssignments.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteCaseAssignmentAsync(int id)
        {
            var bin = await _context.CaseAssignments.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CaseAssignmentExists(int id)
        {
            return (_context.CaseAssignments?.Any(e => e.ID == id)).GetValueOrDefault();
        }       
              
    }
}
