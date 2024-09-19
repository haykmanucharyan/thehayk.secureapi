using System.Net;

namespace thehayk.secureapi.Middlewares
{
    /// <summary>
    /// Middleware for error handling.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration config;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="next">Next handler.</param>
        /// <param name="config">Configuration.</param>
        public ExceptionMiddleware(RequestDelegate next, IConfiguration config)
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
            // we should avoid async operation in catch anti-pattern

            int code = (int)HttpStatusCode.OK;
            string message = null;

            try
            {
                await next.Invoke(context);
            }
            catch (ArgumentNullException anEx)
            {
                code = (int)HttpStatusCode.BadRequest;
                message = anEx.Message;
            }
            catch (ArgumentOutOfRangeException aorEx)
            {
                code = (int)HttpStatusCode.BadRequest;
                message = aorEx.Message;
            }
            catch (ArgumentException aEx)
            {
                code = (int)HttpStatusCode.BadRequest;
                message = aEx.Message;
            }
            catch (Exception ex)
            {
                code = (int)HttpStatusCode.InternalServerError;
                message = ex.Message;
            }

            if (code != (int)HttpStatusCode.OK)
            {
                context.Response.StatusCode = code;
                await context.Response.WriteAsync(message);
            }
        }
    }
}
