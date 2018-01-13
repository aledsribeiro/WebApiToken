using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SamplesWEBAPI.Token
{
    public class ProviderTokenAcess: OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Método é responsável por realizar validações extras quando o usuário
        /// se autentica com o token.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Método recebe o contexto e você terá acesso ao usuário e a senha
        /// através dos objetos contexto.UserName e contexto.Password,
        /// com esses dados você deverá efetuar a validação em suas regras 
        /// de negocio, ou no banco, ou em um Active Directory, 
        /// emfim aqui a validação fica a seu critério.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = Users().FirstOrDefault(u => u.Nome == context.UserName
                && u.Password == context.Password);

            //Caso não encontre o usuário devemos setar o objeto contexto.SetError com uma chave/valor e sair do método.
            if (user == null)
            {
                context.SetError("Usuario Inválido", "Usuário não encontrado ou a senha esta incorreta");
            }

            //Caso encontre emitimos o token com o objeto ClaimsIdentity e o contexto.Validated.
            var identityUser = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identityUser);
        }

        public static IEnumerable<User> Users()
        {
            return new List<User> {
            
             new User { Nome = "Alessandra", Password = "Admin"},
              new User { Nome = "Jeffersonn", Password = "Admin123"},
            };
        }
    }

    
}
