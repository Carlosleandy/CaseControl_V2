using CaseControl.Api.TOKEN.DTOs;
using CaseControl.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CaseControl.Api.TOKEN.Interfaces
{
    public interface IToken
    {
        Claim? IsTokenValidation(string token);
        Task<TokenResponse> GenerateJwtToken(User user);
        JwtSecurityToken DeserializeToken(string token);


        Task<TokenResponse?> RegenerateJwtToken(string token);
    }
}
