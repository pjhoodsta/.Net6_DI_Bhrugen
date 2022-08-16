using WazeCreditGreen.Service.LifeTimeExample;

namespace WazeCreditGreen.Middleware {
    public class CustomMiddleware {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, TransientService transientService, SingletonService singletonService, ScopedService scopedService) {
            context.Items.Add("CustomMiddlewareTransient", "Transient Middleware -" + transientService.GetGuid());
            context.Items.Add("CustomMiddlewareSingleton", "Singleton Middleware -" + singletonService.GetGuid());
            context.Items.Add("CustomMiddlewareScoped", "Scoped Middleware -" + scopedService.GetGuid());

            await _next(context);
        }
    }
}
 