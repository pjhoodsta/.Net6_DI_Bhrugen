using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using WazeCreditGreen.Service.LifeTimeExample;
using Microsoft.AspNetCore.Http;

namespace WazeCreditGreen.Controllers {
    public class LifeTimeController : Controller {
        private readonly TransientService _transientService;
        private readonly SingletonService _singletonService;
        private readonly ScopedService _scopedService;
        public LifeTimeController(TransientService transientService, SingletonService singletonService, ScopedService scopedService) {
            _transientService = transientService;  
            _singletonService = singletonService;   
            _scopedService = scopedService;     
        }



        public IActionResult Index() {
            var messages = new List<string>
            {
                HttpContext.Items["CustomMiddlewareTransient"].ToString(), $"Transient Controller - {_transientService.GetGuid()}",
                HttpContext.Items["CustomMiddlewareSingleton"].ToString(), $"Singleton Controller - {_singletonService.GetGuid()}",
                HttpContext.Items["CustomMiddlewareScoped"].ToString(), $"Scoped Controller - {_scopedService.GetGuid()}",

            };
            return View(messages);  
        }
    }
}