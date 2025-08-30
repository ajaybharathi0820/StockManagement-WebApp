using Identity.Domain.Entities;

namespace Identity.Domain.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
