using CaseControl.Domain.Entities;

namespace CaseControl.Api.TOKEN.DTOs
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public User? User { get; set; }
        //public AuditArea? AuditArea { get; set; }
        public List<string>? AccessList { get; set; }

    }
}
