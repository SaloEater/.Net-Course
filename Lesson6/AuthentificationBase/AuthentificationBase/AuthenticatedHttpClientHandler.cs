using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AuthentificationBase
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private const string AuthorizationHeaderKey = "Authorization";
        private const string Bearer = "Bearer";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiSettings _tokenSettings;

        public AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor,
            IOptions<ApiSettings> tokenSettings)
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            CheckCertificateRevocationList = false;

            _httpContextAccessor = httpContextAccessor;
            _tokenSettings = tokenSettings.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.Write("Going to add header, ");
            var authHeader = request.Headers.Authorization;

            if (authHeader is null)
            {
                Console.Write("creating new, ");
                AddAuthorizationHeader(request);
            }

            Console.Write($"auth={request.Headers.Authorization.Parameter}");
            return await base.SendAsync(request, cancellationToken);
        }

        private void AddAuthorizationHeader(HttpRequestMessage request)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(AuthorizationHeaderKey, out var value))
            {
                var token = value.ToString().Split(" ").LastOrDefault();
                Console.Write($"token exists={token}, ");
                request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, token);
            }
            else if (!string.IsNullOrWhiteSpace(_tokenSettings.Token))
            {
                Console.Write($"token exists in config={_tokenSettings.Token}, ");
                request.Headers.Authorization = new AuthenticationHeaderValue(Bearer, _tokenSettings.Token);
            } else {
                Console.Write($"token does not exists, ");
            }
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             