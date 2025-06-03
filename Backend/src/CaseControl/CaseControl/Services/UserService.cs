using CaseControl.Api.Interfaces;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Security.Cryptography;
using System.Text;



namespace CaseControl.Api.Services
{
    public class UserService : IUser
    {
        private readonly DataContext _context;
    
        public UserService(DataContext context)
        {
            _context = context;
           
        }


        public async Task<List<User>> GetAllUserAsync(PaginationDTO pagination)
        {
            var queryable = _context.Users
                .Include(a => a.Cases)
                  // .Include(a => a.Binnacles)
                  // .Include(a => a.WorkingGroup)
                  // .Include(a => a.UserLevel)
                  // .Include(a => a.Recommendations)
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
                 // .Include(a => a.Cases)
                 // .Include(a => a.Binnacles)
                 // .Include(a => a.WorkingGroup)
                 // .Include(a => a.UserLevel)
                 // .Include(a => a.Recommendations)
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
                PasswordHash = await encriptarSHA256(model.PasswordHash)
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
                // .Include(a => a.Cases)
                // .Include(a => a.Binnacles)
                // .Include(a => a.WorkingGroup)
                // .Include(a => a.UserLevel)
                // .Include(a => a.Recommendations)
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


        public async Task<string> encriptarSHA256(string texto)
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

        public async Task<User?> Authenticate(string username, string password)
        {
            try
            {
                // Obtener el usuario por nombre de usuario
                var user = await GetUserByUserNameAsync(username);
                
                // Si el usuario no existe o no está activo, retornar null
                if (user == null || !user.IsActive) 
                    return null;

                // Verificar si el PasswordHash es nulo
                if (string.IsNullOrEmpty(user.PasswordHash))
                    return null;
                
                // Comprobar si el hash tiene el formato con salt (salt:hash)
                if (user.PasswordHash.Contains(":"))
                {
                    // Formato nuevo con salt
                    string[] parts = user.PasswordHash.Split(':');
                    if (parts.Length != 2)
                        return null;
                        
                    string salt = parts[0];
                    string storedHash = parts[1];
                    
                    // Generar hash con el salt almacenado
                    string computedHash = await encriptarSHA256(salt + password);
                    
                    if (storedHash == computedHash)
                        return user;
                }
                else
                {
                    // Formato antiguo sin salt
                    string hashedPassword = await encriptarSHA256(password);
                    
                    if (user.PasswordHash == hashedPassword)
                        return user;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                // Registrar el error
                Console.WriteLine($"Error en autenticación: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                
                // Re-lanzar la excepción para que sea manejada por el controlador
                throw;
            }
        }
    }
}
