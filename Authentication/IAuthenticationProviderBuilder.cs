using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace acerteapi.Authentication
{
    public interface IAuthenticationProviderBuilder
    {
        IServiceCollection Services { get; }
        IConfiguration Configuration { get; }
    }
}
