using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;


namespace acerteapi.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<bool> Authenticate(string token);
    }
}
