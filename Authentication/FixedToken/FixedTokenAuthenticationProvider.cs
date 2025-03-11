using System.Threading.Tasks;
using acerteapi.Authentication.Security;
using acerteapi.Authentication;

namespace acerteapi.Authentication.FixedToken
{
    public class FixedTokenAuthenticationProvider : IAuthenticationProvider
    {
        private const string GenericUser = "Asl.generic.user";
        private readonly IAslPrincipal principal;
        private readonly FixedTokenAuthenticationProviderOptions options;

        public FixedTokenAuthenticationProvider(IAslPrincipal principal, FixedTokenAuthenticationProviderOptions options)
        {
            this.principal = principal;
            this.options = options;
        }

        public Task<bool> Authenticate(string token)
        {
            if (options.Token != token)
                return Task.FromResult(false);

            principal.SetAuthenticated(GenericUser, nameof(FixedTokenAuthenticationProvider));
            return Task.FromResult(true);
        }
    }
}
