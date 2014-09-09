using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Managers;
using Web.Configuration;
using MvcApplication2.Domain.Measurement;
using System.Security.Claims;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();
        private IManager manager;
        private ICollectService collectService;

        // nice try but no improvement
        // public static Lazy<Logger> xlog(() => LogManager.GetCurrentClassLogger());

        public HomeController(IManager manager, ICollectService collectService)
        {
            Log.Debug("initiating Home Controller");

            this.manager = manager;
            this.collectService = collectService;
        }

        public ActionResult Index()
        {
            Log.Debug("Home Controller: Index()");
            var messageQConfig = MessageQConfiguration.Instance;

            collectService.DoSomething("adfasdf");

            var p = ClaimsPrincipal.Current;
            Log.Debug(p);

            ViewBag.Message =
                this.manager.SomeValue() 
                + ": MessageQ Host = " + messageQConfig.HostName;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}