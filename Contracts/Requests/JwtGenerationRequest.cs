namespace Contracts.Requests
{
    public class JwtGenerationRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public Dictionary<string, object> CustomClaims { get; set; } = new Dictionary<string, object>();
    }
}
