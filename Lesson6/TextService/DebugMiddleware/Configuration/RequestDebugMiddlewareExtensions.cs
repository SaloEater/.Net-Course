﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugMiddleware.Configuration
{
    public static class RequestDebugMiddlewareExtensions
    {
        public static IApplicationBuilder UseDebug(
               this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DebugMiddleware>();
        }
    }
}
