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
        private IManager manager;

        public HomeController(IManager manager)
        {
            this.manager = manager;
        }

        public ActionResult Index()
        {
            var messageQConfig = MessageQConfiguration.Instance;

            ViewBag.Message =
                this.manager.SomeValue() 
                + ": MessageQ Host = " + messageQConfig.Host;

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