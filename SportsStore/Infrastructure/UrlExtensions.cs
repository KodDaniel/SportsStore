using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        //// ExtensionMethod
        //public static string PathAndQuery(this HttpRequest request)
        //{
        //    // Hämtar " the request path"
        //    string pathForThisRequest = request.Path.ToString();

        //    // the raw query string
        //    string queryStringForThisRequest = request.QueryString.ToString();

        //    if (request.QueryString.HasValue)
        //    {
        //        return pathForThisRequest + queryStringForThisRequest;
        //    }
        //    else
        //    {
        //        return pathForThisRequest;
        //    }
        //}

        public static string PathAndQuery(this HttpRequest request) =>
         (request.QueryString.HasValue) ? $"{request.Path}{request.QueryString}" : request.Path.ToString();

    }
}
