using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;

namespace testingOwin
{
    public class CustomAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // validate client credentials (demo)

            // should be stored securely (salted, hashed, iterated)

            string id, secret;

            if (context.TryGetBasicCredentials(out id, out secret))

            {

                if (secret == "secret")

                {
                    context.OwinContext.Set<string>("as:client_id", id);

                    context.Validated();

                }

            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // create identity
            var username = context.UserName;
            var password = context.Password;

            // Here you use db
            var userService = new UserService();
            ClientMaster user = userService.GetUserByCredentials(username, password);

            if (user == null)
            {
                if (user == null)
                {
                    context.SetError("invalid_grant", "Username and password do not match.");
                    return;
                }
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            // create metadata to pass on to refresh token provider
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "as:client_id", context.ClientId }
    });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

        }
    }
}
