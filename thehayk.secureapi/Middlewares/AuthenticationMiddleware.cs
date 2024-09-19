using System.Net;
using System.Text;
using thehayk.secureapi.Configuration;

namespace thehayk.secureapi.Middlewares
{
    /// <summary>
    /// Authentication middleware for both: swagger and API auths.
    /// </summary>
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ISecureApiConfiguration config;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="next">Next handler.</param>
        /// <param name="config">Configuration.</param>
        public AuthenticationMiddleware(RequestDelegate next, ISecureApiConfiguration config)
        {
            this.next = next;
            this.config = config;
        }

        /// <summary>
        /// Request invocation handler.
        /// </summary>
        /// <param name="context">Execution context.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            bool flagSwagger = context.Request.Path.StartsWithSegments("/swagger");

            if (!flagSwagger && !config.BasicAuthIsOn)
            {
                await next.Invoke(context);
                return;
            }

            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Basic "))
            {
                if (flagSwagger)
                    context.Response.Headers["WWW-Authenticate"] = "Basic";

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            string encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
            string decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

            string username = decodedUsernamePassword.Split(':', 2)[0];
            string password = decodedUsernamePassword.Split(':', 2)[1];

            if (flagSwagger)
            {
                if (!IsAuthorized(username, config.SwaggerUsername, password, config.SwaggerPassword))
                {
                    context.Response.Headers["WWW-Authenticate"] = "Basic";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }
            else if (!IsAuthorized(username, config.BasicAuthUsername, password, config.BasicAuthPassword))
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            await next.Invoke(context);
        }

        private bool IsAuthorized(string username1, string username2, string password1, string password2)
        {
            return username1.Equals(username2, StringComparison.InvariantCultureIgnoreCase) &&
                        password1.Equals(password2);
        }
    }
}
