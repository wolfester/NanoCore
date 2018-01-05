using System.Linq;

namespace NanoCore {
    public static class Registrar {
        public static void ConfigureRouter(NanoConfiguration config){
            foreach(var x in config.Controllers) {
                var controllerpath = x.Name;

                //Serving only public, static methods
                foreach (var y in x.GetMethods().Where(m => m.IsStatic && m.IsPublic)) {
                    var methodpath = y.Name;

                    var routepath = $"/{controllerpath}/{methodpath}";

                    var nanoMethod = new NanoMethod(y);

                    Router.AddRoute(routepath, nanoMethod);
                }
            }
        }
    }
}