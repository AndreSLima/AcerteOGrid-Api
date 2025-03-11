using System.Threading.Tasks;
using acerteapi.Authentication.Security;

namespace acerteapi.Authentication
{
    public class AuthenticationDirector : IAuthenticationDirector
    {
        private readonly IAuthenticationProvider provider;
        private readonly IAslPrincipal principal;

        public AuthenticationDirector(IAuthenticationProvider provider, IAslPrincipal principal)
        {
            this.provider = provider;
            this.principal = principal;
        }

        public async Task<bool> ExecuteProvider(string token)
        {
            await provider.Authenticate(token);
            return principal?.IsAuthenticated() ?? false;
        }
    }
}