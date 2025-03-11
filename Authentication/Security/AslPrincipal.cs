using System;
using System.Security.Principal;

namespace acerteapi.Authentication.Security
{
    public class AslPrincipal : IAslPrincipal
    {
        IIdentity identity;

        IIdentity IPrincipal.Identity => identity;

        public bool IsAuthenticated()
        {
            return identity?.IsAuthenticated ?? false;
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public void SetAuthenticated(string name, string type)
            => identity = new GenericIdentity(name, type);
    }
}
