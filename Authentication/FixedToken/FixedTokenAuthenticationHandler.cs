using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System;
using acerteapi.Authentication;
using acerteapi.Authentication.Security;

namespace acerteapi.Authentication.FixedToken
{
    public class FixedTokenAuthenticationHandler : AuthenticationHandler<FixedTokenAuthenticationHandlerOptions>
    {
        private const string AuthorizationKey = "Authorization";
        private const string BearerKey = "bearer";
        private const string FailureMessage = "Unauthorized";
        private readonly IAuthenticationDirector authenticationDirector;
        private readonly IAslPrincipal principal;

        public FixedTokenAuthenticationHandler(IOptionsMonitor<FixedTokenAuthenticationHandlerOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IAuthenticationDirector authenticationDirector, IAslPrincipal principal)
            : base(options, logger, encoder, clock)
        {
            this.authenticationDirector = authenticationDirector;
            this.principal = principal;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationKey))
                return AuthenticateResult.Fail(FailureMessage);

            string authorizationHeader = Request.Headers[AuthorizationKey];
            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.NoResult();

            if (!authorizationHeader.StartsWith(BearerKey, StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail(FailureMessage);

            string token = authorizationHeader.Substring(BearerKey.Length).Trim();

            return await ValidateToken(token);
        }

        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            if (!await authenticationDirector.ExecuteProvider(token))
                return AuthenticateResult.Fail("Unauthorized");

            var identity = new ClaimsIdentity(principal.Identity);
            var claimsPrincipal = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
