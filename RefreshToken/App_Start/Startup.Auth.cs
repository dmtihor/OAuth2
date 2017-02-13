using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RefreshToken.Models;
using RefreshToken.Providers;

namespace RefreshToken
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
        
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };
            
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "zzz",
                ClientSecret = "zzz",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleAuthOptions);

            FacebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "zzz",
                AppSecret = "zzz",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(FacebookAuthOptions);
        }
    }
}