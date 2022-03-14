using System.Threading.Tasks;
using TweeterBook.Domain;

namespace TweeterBook.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        
        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        
    }
}
