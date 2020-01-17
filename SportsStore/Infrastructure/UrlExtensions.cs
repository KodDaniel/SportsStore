using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        // ExtensionMethod
        public static string PathAndQuery(this HttpRequest request) =>
            // Ternary operator och String interpolation
            (request.QueryString.HasValue) ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
    }
}
