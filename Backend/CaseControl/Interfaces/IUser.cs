using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IUser
    {
        Task<List<User>> GetAllUserAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<List<User>> GetAllUserOnlyAsync(PaginationDTO pagination);
        Task<Domain.Entities.User> GetUserByIDAsync(int id);
        Task<User> AddUserAsync(User model);
        Task<User> EditUserAsync(User model);
        Task<bool> DeleteUserAsync(int id);
        bool UserExists(int id);


        //Task<List<string>> GetAccessByUserAsync(int userid);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User?> AuthenticateUser(string key, string username);
    }
}
