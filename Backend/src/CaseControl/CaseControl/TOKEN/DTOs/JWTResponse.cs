namespace CaseControl.Api.TOKEN.DTOs
{
    public class JWTResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
