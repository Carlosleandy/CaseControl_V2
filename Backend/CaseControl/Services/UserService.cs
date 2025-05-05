using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CaseControl.Api.Services
{
    public class UserService : IUser
    {
        private readonly DataContext _context;
        private readonly IKeyExistsServices _keyExistsServices;

        public UserService(DataContext context, IKeyExistsServices keyExistsServices)
        {
            _context = context;
            _keyExistsServices = keyExistsServices;
        }


        public async Task<List<User>> GetAllUserAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users
                .Include(a => a.Cases)
                .Include(a => a.Binnacles)
                .Include(a => a.WorkingGroup)
                .Include(a => a.UserLevel)
                .Include(a => a.Recommendations)
                  .AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.UserName!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Nombre_Completo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Codigo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Identificacion!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Puesto!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            return await queryable
                        .OrderBy(x => x.UserName)
                        //.Paginate(pagination)
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.UserName!.ToLower().Contains(pagination.Filter.ToLower()) ||
                //x.Employee!.Nombre_Completo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                //x.Employee!.Codigo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                //x.Employee!.Identificacion!.ToLower().Contains(pagination.Filter.ToLower()) ||
                //x.Employee!.Puesto!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return totalPages;
        }

        public async Task<List<User>> GetAllUserOnlyAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users
                  .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.UserName!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Nombre_Completo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Codigo!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Identificacion!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.Employee!.Puesto!.ToLower().Contains(pagination.Filter.ToLower()) ||
                x.ID.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }

            var users = await queryable
                        .OrderBy(x => x.UserName)
                        //.Paginate(pagination)
                        .ToListAsync();

            if (users != null)
            {
                foreach (var item in users)
                {
                    var employee = await _context.vwEmployees
                   .Where(a => a.Usuario == item.UserName && !string.IsNullOrEmpty(a.Correo) && a.Correo.Contains("@"))
                    .FirstOrDefaultAsync();

                    item.Employee = employee;
                }
            }

            return users!;
        }

        public async Task<User> GetUserByIDAsync(int id)
        {
            var user = await _context.Users
                .Include(a => a.Cases)
                .Include(a => a.Binnacles)
                .Include(a => a.WorkingGroup)
                .Include(a => a.UserLevel)
                .Include(a => a.Recommendations)
                .Where(a => a.ID == id)
                 .FirstOrDefaultAsync();

            if (user != null)
            {
                var employee = await _context.vwEmployees
                   .Where(a => a.Usuario == user.UserName)
                    .FirstOrDefaultAsync();

                user.Employee = employee;
            }

            return user!;
        }

        public async Task<User> AddUserAsync(User model)
        {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<User> EditUserAsync(User model)
        {
            model.WorkingGroup = null;
            model.UserLevel = null;
            
            _context.Users.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
            _context.Remove(user!);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        //public async Task<List<string>> GetAccessByUserAsync(int userid)
        //{
        //    var access = await _context.Access_Users
        //        .Include(a => a.Access)
        //        .Where(a => a.UserID == userid)
        //        .Select(a => a.Access!.Key)
        //        .ToListAsync();

        //    return access!;
        //}

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _context.Users
                .Include(a => a.Cases)
                .Include(a => a.Binnacles)
                .Include(a => a.WorkingGroup)
                .Include(a => a.UserLevel)
                .Include(a => a.Recommendations)
                .Where(a => a.UserName == userName)
                 .FirstOrDefaultAsync();

            if (user != null)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                var employee = await _context.vwEmployees
                   .Where(a => a.Usuario == user.UserName)
                    .FirstOrDefaultAsync();

                user.Employee = employee;
            }

            return user!;
        }

        public async Task<User?> AuthenticateUser(string key, string username)
        {
            var keyIsValid = await _keyExistsServices.KeyExists(key);

            if (!keyIsValid) return null;

            var user = await GetUserByUserNameAsync(username);
            if (user == null) return null;

            if (!user.IsActive) return null;

            return user;
        }


    }
}
