using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class WorkingGroupService : IWorkingGroup
    {
        private readonly DataContext _context;

        public WorkingGroupService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<WorkingGroup>> GetAllWorkingGroupAsync(PaginationDTO pagination)
        {
            var queryable = _context.WorkingGroups
                .Include(a => a.Users)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.Name)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.WorkingGroups.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<WorkingGroup> GetWorkingGroupByIDAsync(int id)
        {
            var wgroup = await _context.WorkingGroups
                .Include(a => a.Users)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return wgroup!;
        }

        public async Task<WorkingGroup> AddWorkingGroupAsync(WorkingGroup model)
        {
            _context.WorkingGroups.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<WorkingGroup> EditWorkingGroupAsync(WorkingGroup model)
        {
            _context.WorkingGroups.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteWorkingGroupAsync(int id)
        {
            var wgroup = await _context.WorkingGroups.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(wgroup!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool WorkingGroupExists(int id)
        {
            return (_context.WorkingGroups?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
