using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public struct baseUrls
    {
        public const string firstapi = "https://localhost:7116/";
        
    }
    public struct Request
    {
        public const string firstapi = "firstapi";
    }

    public static class Router
    {

        public static string route(this string Route, string project)
        {
            string? route = null;
            switch (project)
            {
                case Request.firstapi:
                    route = $"{baseUrls.firstapi}{Route}";
                    break;
                default:
                    throw new ArgumentException("Invalid Route");
            }
            return route;
        }
    }
}
