using acerteapi.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace acerteapi.Authentication.FixedToken
{
    public static class AuthenticationProviderBuilderExtensions
    {
        private const string DefaultScheme = "fixed";

        public static IAuthenticationProviderBuilder AddFixedTokenProvider(this IAuthenticationProviderBuilder authenticationProviderBuilder)
        {
            authenticationProviderBuilder.Services
                .AddScoped<IAuthenticationProvider, FixedTokenAuthenticationProvider>()
                .AddSingleton(authenticationProviderBuilder.Configuration.GetFixedTokenAuthentication())
                .AddAuthentication(DefaultScheme)
                .AddScheme<FixedTokenAuthenticationHandlerOptions, FixedTokenAuthenticationHandler>(DefaultScheme, null);

            return authenticationProviderBuilder;
        }
    }
}
