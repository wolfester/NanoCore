using System;
using System.Reflection;

namespace NanoCore {
    public static class Registrar {
        public static void AddMethods(Type type){
            var methodsToAdd = type.GetMethods(BindingFlags.Public | BindingFlags.Static);

            var controllerPath = type.Name.TrimEnd("Controller".ToCharArray());

            foreach(var x in methodsToAdd){
                var handler = new RequestHandler(x);
                
                var handlerPath = string.Format("/{0}/{1}", controllerPath, x.Name);

                Router.AddRoute(handlerPath, handler);
            }
        }
    }
}