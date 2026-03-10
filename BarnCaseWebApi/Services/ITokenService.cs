using BarnCaseWebApi.Models;

namespace BarnCaseWebApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
