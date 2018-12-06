using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plataforma.Lib.Models;
using Plataforma.Lib.Domain;

namespace Plataforma.Lib.Services
{
    public class AutenticacaoHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UsuarioDomain _domain;

        public AutenticacaoHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UsuarioDomain domain)
            : base(options, logger, encoder, clock)
        {
            _domain = domain;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Cabeçalho de autenticação inexistente.");

            Usuario usuario = null;
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credenciaisBytes = Convert.FromBase64String(header.Parameter);
                var credenciais = Encoding.UTF8.GetString(credenciaisBytes).Split(':');
                var login = credenciais[0];
                var senha = credenciais[1];
                usuario = await _domain.EncontrarAsync(new Usuario
                {
                    Email = login, Senha = senha, Admin = true
                });
            }
            catch
            {
                return AuthenticateResult.Fail("Cabeçalho de autenticação inválido.");
            }

            if (usuario == null)
                return AuthenticateResult.Fail("Login ou senha inválidos");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Email),
            };
            var identidade = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identidade);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}