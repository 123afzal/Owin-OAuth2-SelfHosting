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

                    // need to make the client_id available for later security checks

                    context.OwinContext.Set<string>("as:client_id", id);

                    context.Validated();

                }

            }
            //context.Validated();
            //return Task.FromResult(0);
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

        //public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)

        //{

        //    var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];

        //    var currentClient = context.OwinContext.Get<string>("as:client_id");



        //    // enforce client binding of refresh token

        //    if (originalClient != currentClient)

        //    {

        //        context.Rejected();

        //        return;

        //    }



        //    // chance to change authentication ticket for refresh token requests

        //    var newId = new ClaimsIdentity(context.Ticket.Identity);

        //    newId.AddClaim(new Claim("newClaim", "refreshToken"));



        //    var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);

        //    context.Validated(newTicket);

        //}











        //public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    //context.OwinContext.Response.Headers
        //    //    .Add(new KeyValuePair<string, string[]>("Access-Control-Allow-Origin", new[] { "*" }));

        //    //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //    //var user = userManager.Find(context.UserName, context.Password);
        //    //if (user == null)
        //    //{
        //    //    context.SetError("invalid_grant", "Username and password do not match.");
        //    //    return Task.FromResult(0);
        //    //}

        //    var username = context.UserName;
        //    var password = context.Password;
        //    var userService = new UserService();
        //    ClientMaster user = userService.GetUserByCredentials(username, password);

        //    if (user == null)
        //    {
        //        if (user == null)
        //        {
        //            context.SetError("invalid_grant", "Username and password do not match.");
        //            return Task.FromResult(0);
        //        }
        //    }
        //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

        //    context.Validated(identity);
        //    return Task.FromResult(0);

        //    //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        //    //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

        //    //context.Validated(identity);
        //    //return Task.FromResult(0);

        //}
    }
}
