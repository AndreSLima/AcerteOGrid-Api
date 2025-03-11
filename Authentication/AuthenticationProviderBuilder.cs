using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace acerteapi.Authentication
{
    public class AuthenticationProviderBuilder : IAuthenticationProviderBuilder
    {
        public AuthenticationProviderBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

        public IServiceCollection Services { get; }

        public IConfiguration Configuration { get; }
    }
}
