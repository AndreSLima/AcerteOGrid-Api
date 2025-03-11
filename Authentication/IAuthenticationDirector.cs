using System.Threading.Tasks;

namespace acerteapi.Authentication
{
    public interface IAuthenticationDirector
    {
        Task<bool> ExecuteProvider(string token);
    }
}
