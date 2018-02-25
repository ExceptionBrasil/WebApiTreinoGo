using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiTreinoGo.Providers;

[assembly: OwinStartup(typeof(WebApiTreinoGo.App_Start.Startup))]

namespace WebApiTreinoGo.App_Start
{
    partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }/*agrupa as opções de configuração do fornecimento de tokens de acesso*/

        public void Configuration(IAppBuilder app)
        {

            //Habilita o Cors
            app.UseCors(CorsOptions.AllowAll);
            
            //Resgata a Configuração do WebApi
            HttpConfiguration config = new HttpConfiguration();

            //Faz a configuração da Aplicação com o Token 
            ConfigureToken(app);

            //Configurações Extras
            ConfigureOAuth(app);

            //Habilita o WebAPi
            app.UseWebApi(config);

        }

        /// <summary>
        /// Configures the token.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureToken(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions();

            //Configuração
            OAuthOptions.AllowInsecureHttp = true;
            OAuthOptions.TokenEndpointPath = new PathString("/Token");
            OAuthOptions.AccessTokenExpireTimeSpan = TimeSpan.FromHours(1);
            OAuthOptions.Provider = new ProviderToken(); //Provider da aplicação 
            
            
            //Ativação
            app.UseOAuthAuthorizationServer(OAuthOptions);
            
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //Ativa o Bearer Token 
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            
        }

        /// <summary>
        /// Configurações extras de autenticação
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions()); 
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


        }
    }
}