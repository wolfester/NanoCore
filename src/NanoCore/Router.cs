using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NanoCore {
    public static class Router {
        private static Dictionary<string, RequestHandler> _routes = new Dictionary<string, RequestHandler>();

        public static void AddRoute(string path, RequestHandler handler) {
            if(_routes.TryGetValue(path, out var x)){
                return;
            }

            _routes[path] = handler;
        }

        public static async Task RouteRequest(HttpContext context){

        }
    }
}