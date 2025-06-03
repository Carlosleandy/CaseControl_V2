using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Services
{
    public class InterviewService : IInterview
    {
        private readonly DataContext _context;

        public InterviewService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Interview>> GetAllInterviewAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interviews
                .Include(a => a.Case)
                .Include(a => a.Linked)
                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.Description)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Interviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<Interview> GetInterviewByIDAsync(int id)
        {
            var Interview = await _context.Interviews
                .Include(a => a.Case)
                .Include(a => a.Linked)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            return Interview!;
        }

        public async Task<Interview> AddInterviewAsync(Interview model)
        {
            model.DateRegistered = DateTime.Now;

            _context.Interviews.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Interview> EditInterviewAsync(Interview model)
        {
            _context.Interviews.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteInterviewAsync(int id)
        {
            var bin = await _context.Interviews.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(bin!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool InterviewExists(int id)
        {
            return (_context.Interviews?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        public async Task<List<IntervieweeType>> GetAllIntervieweeTypesAsync()
        {
            var queryable = await _context.IntervieweeTypes
                .OrderBy(x => x.Name)
                .ToListAsync();

            return queryable;
        }

    }
}
