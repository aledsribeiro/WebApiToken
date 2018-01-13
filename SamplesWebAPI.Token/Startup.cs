using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SamplesWEBAPI.Token;

[assembly: OwinStartup(typeof(SamplesWebAPI.Token.Startup))]

namespace SamplesWebAPI.Token
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //configuração WebApi
            var config = new HttpConfiguration();

            //configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            ConfigureAcessToken(app);

            // ativando configuração WebApi
            app.UseWebApi(config);
        }

        private void ConfigureAcessToken(IAppBuilder app)
        {
            var optionsConfigurationToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, //defina esse parâmetro como true, mas lembre-se que em produção deverá ser false;
                TokenEndpointPath = new PathString("/token"), //este é o endereço que irá fornecer o token de acesso;
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5), //é o valor em tempo que o token irá expirar;
                Provider = new ProviderTokenAcess() //aqui vamos definir a classe que irá efetivamente validar o usuário e devolver o token.
            };

            app.UseOAuthAuthorizationServer(optionsConfigurationToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

  
        
}

