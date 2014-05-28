using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Managers;
using Web.Configuration;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IManager manager;

        public HomeController(IManager manager)
        {
            logger.Debug("initiating Home Controller");
            this.manager = manager;
        }

        public ActionResult Index()
        {
            logger.Debug("Home Controller: Index()");
            var messageQConfig = MessageQConfiguration.Instance;

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