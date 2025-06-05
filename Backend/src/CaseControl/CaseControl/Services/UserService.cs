// Modificado por el Pasante Carlos Leandy Moreno Reyes (Alea: EL Varon)
using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CaseControl.Api.Services
{
    public class UserService : IUser
    {
        private readonly DataContext _context;
        private readonly IUtil _util;

        public UserService(DataContext context, IUtil util)
        {
            _context = context;
            _util = util;
        }

        public async Task<List<User>> GetAllUserAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users
                .Include(a => a.Cases)
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
                        .ToListAsync();
        }

        public async Task<double> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.UserName!.ToLower().Contains(pagination.Filter.ToLower()) ||
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
                 .Include(a => a.Employee)
                 .Where(a => a.IsActive)
                 .OrderBy(a => a.UserName)
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

        public async Task<User> AddUser(User model)
        {
            var modeloUsuario = new User
            {
                UserName = model.UserName,
                IsActive = model.IsActive,
                IsAdmin = model.IsAdmin,
                WorkingGroupID = model.WorkingGroupID,
                UserLevelID = model.UserLevelID,
                IDGerencia = model.IDGerencia,
                PasswordHash = await _util.encriptarSHA256(model.PasswordHash)
            };

            await _context.Users.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            return modeloUsuario;
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

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _context.Users
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

        public async Task<User?> Authenticate(string username, string password)
        {
            try
            {
                Console.WriteLine($"Intentando autenticar usuario: {username}");
                var user = await GetUserByUserNameAsync(username);
                if (user == null)
                {
                    Console.WriteLine("Usuario no encontrado.");
                    return null;
                }
                if (!user.IsActive)
                {
                    Console.WriteLine("Usuario no está activo.");
                    return null;
                }
                if (string.IsNullOrEmpty(user.PasswordHash))
                {
                    Console.WriteLine("PasswordHash es nulo o vacío.");
                    return null;
                }

                if (user.PasswordHash.Contains(":"))
                {
                    string[] parts = user.PasswordHash.Split(':');
                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Formato de PasswordHash con salt inválido.");
                        return null;
                    }
                    string salt = parts[0];
                    string storedHash = parts[1];
                    string computedHash = await _util.encriptarSHA256(salt + password);
                    Console.WriteLine($"ComputedHash: {computedHash}, StoredHash: {storedHash}");
                    if (storedHash == computedHash) return user;
                }
                else
                {
                    string hashedPassword = await _util.encriptarSHA256(password);
                    Console.WriteLine($"ComputedHash: {hashedPassword}, StoredHash: {user.PasswordHash}");
                    if (user.PasswordHash == hashedPassword) return user;
                }
                Console.WriteLine("Credenciales no coinciden.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en autenticación: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}