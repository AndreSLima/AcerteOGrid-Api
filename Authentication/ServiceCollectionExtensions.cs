using acerteapi.Authentication.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace acerteapi.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationProvider(this IServiceCollection services, IConfiguration configuration,
            Action<IAuthenticationProviderBuilder> builder)
        {
            builder(new AuthenticationProviderBuilder(services, configuration));

            return services
                .AddScoped<IAslPrincipal, AslPrincipal>()
                .AddScoped<IAuthenticationDirector, AuthenticationDirector>()
                ;
        }
    }
}
