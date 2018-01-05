using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NanoCore {
    public static class Router {
        private static Dictionary<string, NanoMethod> routes;

        static Router(){
            routes = new Dictionary<string, NanoMethod>();
        }
        public static async Task<object> Route (HttpContext context){
            if(routes.TryGetValue(context.Request.Path, out var method)){
                return await method.Invoke(context);
            }

            throw new Exception($"No method found for {context.Request.Path}");
        }

        internal static void AddRoute (string path, NanoMethod method){
            routes.Add(path, method);
        }
    }
}