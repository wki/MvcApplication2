using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Managers;

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
            ViewBag.Message = this.manager.SomeValue() + ": Modify this template to jump-start your ASP.NET MVC application.";

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