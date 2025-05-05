using System.Threading.Tasks;
using CaseControl.Core.DTOs;
using CaseControl.Core.Entities;

namespace CaseControl.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> AuthenticateAsync(LoginUserDTO loginDto);
        string GenerateJwtToken(User user);
        bool ValidateToken(string token);
    }
}
