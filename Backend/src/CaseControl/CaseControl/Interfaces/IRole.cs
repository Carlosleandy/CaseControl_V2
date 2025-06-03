using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;

namespace CaseControl.Api.Interfaces
{
    public interface IRole
    {
        Task<List<Role>> GetAllRoleAsync(PaginationDTO pagination);
        Task<double> GetTotalPagesAsync(PaginationDTO pagination);
        Task<Domain.Entities.Role> GetRoleByIDAsync(int id);
        Task<Role> AddRoleAsync(Role model);
        Task<Role> EditRoleAsync(Role model);
        Task<bool> DeleteRoleAsync(int id);
        bool RoleExists(int id);
    }
}
