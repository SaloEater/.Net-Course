using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DebugMiddleware
{
    public class DebugMiddleware
    {
        private readonly RequestDelegate _next;

        public DebugMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
