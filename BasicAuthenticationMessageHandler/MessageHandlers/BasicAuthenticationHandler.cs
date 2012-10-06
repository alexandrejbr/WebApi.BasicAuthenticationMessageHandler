using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MessageHandlers
{
    public class BasicAuthenticationHandler: DelegatingHandler
    {
        private const string WwwAuthenticateHeader = "WWW-Authenticate";
        private const string Basic = "Basic";

        private readonly ICredentialsValidator _credentialsValidator;

        public BasicAuthenticationHandler(ICredentialsValidator credentialsValidator)
        {
            this._credentialsValidator = credentialsValidator;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Credentials credentials = GetCredentialsFromAuthorizationHeader(request);
            if (credentials != null)
            {
                if (this._credentialsValidator.Validate(credentials))
                {
                    var identity = new GenericIdentity(credentials.Username, Basic);
                    var genericPrincipal = new GenericPrincipal(identity, null);
                    HttpContext.Current.User = genericPrincipal;
                    Thread.CurrentPrincipal = genericPrincipal;
                }
            }

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized
                        && !response.Headers.Contains(WwwAuthenticateHeader))
                    {
                        response.Headers.Add(WwwAuthenticateHeader, Basic);
                    }
                    return response;
                });
        }

        private static Credentials GetCredentialsFromAuthorizationHeader(HttpRequestMessage request)
        {
            AuthenticationHeaderValue authenticationHeaderValue = request.Headers.Authorization;

            if (authenticationHeaderValue != null && !String.IsNullOrEmpty(authenticationHeaderValue.Scheme)
                && !String.IsNullOrEmpty(authenticationHeaderValue.Parameter)
                && authenticationHeaderValue.Scheme.Equals(Basic))
            {
                string authValue =
                    System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter));
                string[] authParts = authValue.Split(':');

                if (authParts.Length == 2)
                {
                    return new Credentials { Username = authParts[0], Password = authParts[1] };
                }
            }
            return null;
        }
    }
}
