using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Aluraflix.API.Entities;
using Aluraflix.API.Services;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace Aluraflix.API.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsuarioService _userService;
        private string failReason;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUsuarioService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                failReason = "Credenciais inválidas";
                return AuthenticateResult.Fail(failReason);
            }

            Usuario user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _userService.Authenticate(username, password);
            }
            catch
            {
                failReason = "Credenciais inválidas";
                return AuthenticateResult.Fail(failReason);
            }

            if (user == null)
            {
                failReason = "Usuário e senha inválido";
                return AuthenticateResult.Fail(failReason);
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;

            if (failReason != null)
            {
                //Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = failReason;

                string result;
                Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                result = JsonConvert.SerializeObject(new { error = failReason });
                Context.Response.ContentType = "application/json";
                Context.Response.WriteAsync(result);
            }

            return Task.CompletedTask;
        }

    }
}