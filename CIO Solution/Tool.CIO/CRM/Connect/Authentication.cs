using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
//using Tools.CIO.Tools;

namespace Tool.CIO.CRM.Connect
{
    public class Authentication
    {
        private Configuration _config = null;
        private HttpMessageHandler _clientHandler = null;
        private AuthenticationContext _context = null;
        private string _authority = null;

        #region Constructors
        public Authentication(Configuration config)
        {
            if (config == null)
                throw new Exception("Configuration cannot be null.");

            _config = config;

            // Check the Authority to determine if OAuth authentication is used.
            if (String.IsNullOrEmpty(Authority))
            {
                if (_config.Username != String.Empty)
                {
                    _clientHandler = new HttpClientHandler()
                    { Credentials = new NetworkCredential(_config.Username, _config.Password, _config.Domain) };
                }
                else
                // No username is provided, so try to use the default domain credentials.
                {
                    _clientHandler = new HttpClientHandler()
                    { UseDefaultCredentials = true };
                }
            }
            else
            {
                _clientHandler = new OAuthMessageHandler(this, new HttpClientHandler());
                _context = new AuthenticationContext(Authority, false);
            }
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// The authentication context.
        /// </summary>
        public AuthenticationContext Context
        { get { return _context; } }

        /// <summary>
        /// The HTTP client message handler.
        /// </summary>
        public HttpMessageHandler ClientHandler
        { get { return _clientHandler; } }

        /// <summary>
        /// The URL of the authority to be used for authentication.
        /// </summary>
        public string Authority
        {
            get
            {
                if (_authority == null)
                    _authority = DiscoverAuthority(_config.ServiceUrl);

                return _authority;
            }

            set { _authority = value; }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Returns the authentication result for the configured authentication context.
        /// </summary>
        /// <returns>The refreshed access token.</returns>
        /// <remarks>Refresh the access token before every service call to avoid having to manage token expiration.</remarks>
        public AuthenticationResult AcquireToken()
        {
            if (_config != null && (!string.IsNullOrEmpty(_config.Username) && _config.Password != null))
            {
                // WebAPP
                ClientCredential CliCredential = new ClientCredential(_config.ClientId, _config.SecretKey);
                var result = _context.AcquireTokenAsync(_config.ServiceUrl, CliCredential).Result;
                //var result = _context.AcquireTokenSilentAsync(_config.ServiceUrl, CliCredential, UserIdentifier.AnyUser).Result;

                //Native
                //UserPasswordCredential cred = new UserPasswordCredential(_config.Username, _config.Password);
                //var result = _context.AcquireTokenAsync(_config.ServiceUrl, _config.ClientId, cred).Result;

                return result;
            }
            // return _context.AcquireTokenAsync(_config.ServiceUrl, _config.ClientId, new Uri(_config.RedirectUrl), PromptBehavior.Auto);
            return _context.AcquireTokenAsync(_config.ServiceUrl, _config.ClientId, new Uri(_config.RedirectUrl), null).Result;
        }

        /// <summary>
        /// Returns the authentication result for the configured authentication context.
        /// </summary>
        /// <param name="username">The username of a CRM system user in the target organization. </param>
        /// <param name="password">The password of a CRM system user in the target organization.</param>
        /// <returns>The authentication result.</returns>
        /// <remarks>Setting the username or password parameters to null results in the user being prompted to
        /// enter log-on credentials. Refresh the access token before every service call to avoid having to manage
        /// token expiration.</remarks>
        public AuthenticationResult AcquireToken(string username, SecureString password)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && password != null)
                {
                    UserCredential cred = new UserPasswordCredential(username, password);
                    return _context.AcquireTokenAsync(_config.ServiceUrl, _config.ClientId, cred).Result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Authentication failed. Verify the configuration values are correct.", e);
            }
            return null;
        }


        /// <summary>
        /// Discover the authentication authority.
        /// </summary>
        /// <returns>The URL of the authentication authority on the specified endpoint address, or an empty string
        /// if the authority cannot be discovered.</returns>
        public static string DiscoverAuthority(string serviceUrl)
        {
            try
            {
                //Passage en TLS 12 OBLIGATOIRE
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                AuthenticationParameters ap = AuthenticationParameters.CreateFromResourceUrlAsync(new Uri(serviceUrl + "api/data/")).Result;
                return ap.Authority;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("An HTTP request exception occurred during authority discovery.", e);
            }
            catch (System.Exception e)
            {
                // This exception ocurrs when the service is not configured for OAuth.
                if (e.HResult == -2146233088)
                {
                    return String.Empty;
                }
                else
                {
                    throw e;
                }
            }
        }
        #endregion Methods

        /// <summary>
        /// Custom HTTP client handler that adds the Authorization header to message requests. This
        /// is required for IFD and Online deployments.
        /// </summary>
        class OAuthMessageHandler : DelegatingHandler
        {
            Authentication _auth = null;

            public OAuthMessageHandler(Authentication auth, HttpMessageHandler innerHandler)
                : base(innerHandler)
            {
                _auth = auth;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                // It is a best practice to refresh the access token before every message request is sent. Doing so
                // avoids having to check the expiration date/time of the token. This operation is quick.
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCIsImtpZCI6IkhCeGw5bUFlNmd4YXZDa2NvT1UyVEhzRE5hMCJ9.eyJhdWQiOiJodHRwczovL2lvY2R3dGVzdDIuY3JtNC5keW5hbWljcy5jb20vIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvMGY1Y2UyNzktMDM3Zi00NmE4LTliNTgtODM2YTM0NDJlMTMyLyIsImlhdCI6MTU1NzgzNzMzNCwibmJmIjoxNTU3ODM3MzM0LCJleHAiOjE1NTc4NDEyMzQsImFjciI6IjEiLCJhaW8iOiJBU1FBMi84TEFBQUFzMi9GSUI5SStGdlNHZGM0QThvUWEvbGt4MU1RMnp4aDc1Q1VGbFRJNGJjPSIsImFtciI6WyJwd2QiXSwiYXBwaWQiOiJiNGI0ZTgzOC1lYzA0LTRiMTUtYjk1MS0xMTczMzA0OTUyNWMiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6IkFDQyIsImdpdmVuX25hbWUiOiJDb3JlIFNlcnZpY2UiLCJpcGFkZHIiOiIxNzguMjM3LjEwMS4xOSIsIm5hbWUiOiJDb3JlIFNlcnZpY2UgQUNDIiwib2lkIjoiMDk5OTEzMjctMzY4ZS00MDA2LWFlNDQtMTE2YTI0M2FkM2I3IiwicHVpZCI6IjEwMDMyMDAwNDExRkM2NEYiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJKaTVLQ1g2cnBoajROLVp0bjVjN2pmODJHdEtGYzBkMVZLM2hrSU40QzQ0IiwidGlkIjoiMGY1Y2UyNzktMDM3Zi00NmE4LTliNTgtODM2YTM0NDJlMTMyIiwidW5pcXVlX25hbWUiOiJjb3JlLnNlcnZpY2UuYWNjQG9saW1waWNvLmJpeiIsInVwbiI6ImNvcmUuc2VydmljZS5hY2NAb2xpbXBpY28uYml6IiwidXRpIjoiaENseFFHSHNHVWVaYlZOU0tFS2dBQSIsInZlciI6IjEuMCJ9.Ok6mrmOTogSkd9UUWzfJqgYnPqj8nLe1zdUEs4w63Hou-2hOkMKjvQ8zYmNn4-61h-ICItrExuKTcmoYVi7NcYa-C80WRcnEUlxT1O5U46yOsNmlExwAHQgRgM_Cv3rb_Cwq8QDbIrmpG-EO75rGKkGnNT02aJQttTSfDT-zCzey6Zgt0lOkERYmtjwbHZ_ny8utXeksit-rCeEQiK6jg0ko_5l3lPnsGwpry6SJfc4XmROj-6J3vWh1dlqk9Y7w_0cIJTxC-DCtQx-VYq-rdpzR9GCif_jgjvVmvfMmPYAn1EuVJ5S_tVK_JN64kZdJWR5dbL83WuTdBVG1E2Mz6A");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _auth.AcquireToken().AccessToken);

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
