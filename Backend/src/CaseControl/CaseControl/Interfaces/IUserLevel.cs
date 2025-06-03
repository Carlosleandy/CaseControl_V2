using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IUserLevel
    {
        Task<List<UserLevel>> GetAllUserLevelAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.UserLevel> GetUserLevelByIDAsync(int id);
        Task<UserLevel> AddUserLevelAsync(UserLevel model);
        Task<UserLevel> EditUserLevelAsync(UserLevel model);
        Task<bool> DeleteUserLevelAsync(int id);
        bool UserLevelExists(int id);
    }
}
