using Microsoft.Extensions.Configuration;

namespace acerteapi.Authentication.FixedToken
{
    public static class ConfigurationExtensions
    {
        public const string ConfigurationKey = "Authentication:FixedProvider";
        public static FixedTokenAuthenticationProviderOptions GetFixedTokenAuthentication(this IConfiguration configuration)
        {
            var options = new FixedTokenAuthenticationProviderOptions();
            configuration.Bind(ConfigurationKey, options);
            return options;
        }
    }
}
