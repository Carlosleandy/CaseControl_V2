using System.Collections.Generic;
using System.Threading.Tasks;
using CaseControl.Core.Entities;

namespace CaseControl.Core.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetByNameAsync(string name);
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);
    }
}
