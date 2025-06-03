using CaseControl.Domain.Entities;
using CaseControl.Token.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Token.Interfaces
{
    public interface IToken
    {
        Claim? IsTokenValidation(string token);
        Task<TokenResponse> GenerateJwtToken(User user);
        JwtSecurityToken DeserializeToken(string token);


        Task<TokenResponse?> RegenerateJwtToken(string token);
    }
}
