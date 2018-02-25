using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApiTreinoGo.Providers
{
    public class ProviderToken: OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are
        /// present on the request. If the web application accepts Basic authentication credentials,
        /// context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. If the web
        /// application accepts "client_id" and "client_secret" as form encoded POST parameters,
        /// context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body.
        /// If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
           

            IOwinRequest request = context.Request;
            IReadableStringCollection parametros = context.Parameters;
            /*
             * Teste de usuário e senhas antes da validação final
             Parametros["username"]=>            "daniel"
             Parametros["password"]=>            "123456"
             Parametros["grant_type"]=>          "password"
             */


            //Validação do Agent
            string agent = request.Headers["User-Agent"].ToUpper();

            if(!( agent.Contains("INSOMNIA") ||
                agent.Contains("CHROME") ||
                agent.Contains("MOZILA") ||
                agent.Contains("WINDOWS")))
            {
                
                context.SetError("Invalid User Agent");
                context.Rejected();
            }
            context.Validated();


        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user has provided name and password
        /// credentials directly into the client application's user interface, and the client application is using those to acquire an "access_token" and
        /// optional "refresh_token". If the web application supports the
        /// resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To issue an
        /// access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated
        /// with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        /// The default behavior is to reject this grant type.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //Validar Usuário e senha            
            string usuário =context.UserName;
            string senha = context.Password;

            //Cria uma nova identidade
            ClaimsIdentity identidade = new ClaimsIdentity(context.Options.AuthenticationType);            

            
            //Retorna a Identidade, o Token propriamente dito
            context.Validated(identidade);           
            
        }
    }
}