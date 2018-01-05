using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NanoCore {
    public static class ApplicationBuilderExtensions {
        public static IApplicationBuilder UseNano(this IApplicationBuilder app, NanoConfiguration config = null) {
            Init(app, config);

            return app;
        }

        private static void Init(IApplicationBuilder app, NanoConfiguration config){
            Registrar.ConfigureRouter(config);

            app.Run(async (context) => {
                var routerResponse = await Router.Route(context);

                await context.Response.WriteAsync((string)JsonConvert.SerializeObject(routerResponse));
            });
        }
    }
}