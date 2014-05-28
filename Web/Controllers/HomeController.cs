using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Managers;
using Web.Configuration;
using MvcApplication2.Domain.Measurement;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IManager manager;
        private ICollectService collectService;

        public HomeController(IManager manager, ICollectService collectService)
        {
            logger.Debug("initiating Home Controller");
            this.manager = manager;
            this.collectService = collectService;
        }

        public ActionResult Index()
        {
            logger.Debug("Home Controller: Index()");
            var messageQConfig = MessageQConfiguration.Instance;

            collectService.DoSomething("adfasdf");

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