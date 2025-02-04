using System.IdentityModel.Tokens.Jwt;

namespace FuelGo.Services
{
    public class UserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetUserIdFromToken()
        {
            // Retrieve the JWT token from the authorization header
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return null; // or throw an exception, depending on your requirements
            }

            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);

            // Retrieve user ID claim from the token
            var userIdClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "sub");

            return userIdClaim?.Value;
        }
    }
}
