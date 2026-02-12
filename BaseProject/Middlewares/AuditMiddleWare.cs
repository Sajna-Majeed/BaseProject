using System.Security.Claims;

namespace API.Middlewares
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                context.Items["UserId"] = int.Parse(userIdClaim.Value);
            }
            else
            {
                context.Items["UserId"] = null;
            }

            await _next(context);
        }
    }


}
