using Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Api.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public IdentityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost(Routes.Identity.GetJwt)]
        public async Task<IActionResult> GetJwt(
            JwtGenerationRequest jwtGenerationRequest,
            CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:TokenSecret")!);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, jwtGenerationRequest.Email),
                new(JwtRegisteredClaimNames.Email, jwtGenerationRequest.Email),
                new("userid", jwtGenerationRequest.UserId.ToString()),
            };

            foreach (var claimPair in jwtGenerationRequest.CustomClaims)
            {
                var jsonElement = (JsonElement)claimPair.Value;
                var valueType = jsonElement.ValueKind switch
                {
                    JsonValueKind.True => ClaimValueTypes.Boolean,
                    JsonValueKind.False => ClaimValueTypes.Boolean,
                    JsonValueKind.Number => ClaimValueTypes.Double,
                    _ => ClaimValueTypes.String
                };

                var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);
                claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:TokenLifetimeHours")!),
                Issuer = _configuration.GetValue<string>("Jwt:Issuer")!,
                Audience = _configuration.GetValue<string>("Jwt:Audience")!,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}
