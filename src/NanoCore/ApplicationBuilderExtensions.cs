using Microsoft.AspNetCore.Builder;

namespace NanoCore {
    public static class ApplicationBuilderExtensions {
        public static void UseNanoCore(this IApplicationBuilder app, NanoConfiguration configuration = null) {
            if (configuration == null) configuration = new NanoConfiguration();

            foreach(var x in configuration.GetControllers()){
                Registrar.AddMethods(x);
            }

            app.Run(async (context) => {
                await Router.RouteRequest(context);
            });
        }
    }
}