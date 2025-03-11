using System.Security.Principal;

namespace acerteapi.Authentication.Security
{
    public interface IAslPrincipal : IPrincipal
    {
        void SetAuthenticated(string name, string type);
        bool IsAuthenticated();
    }
}
