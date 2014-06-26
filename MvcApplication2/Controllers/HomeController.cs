using MvcApplication2.Infrastructure.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Domain;

namespace MvcApplication2.Controllers
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
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Foo()
        {
            var result = new Dictionary<string, int> { { "Howey", 49 }, { "Foo", 33 } };
            var names = new[] { "Howey", "Foo" };
            var foo = new { Name = "Tom", Age = 42, Foo = "Bar" };

            ViewBag.Something = "Foo";

            return Json( foo, JsonRequestBehavior.AllowGet );
        }

        public ActionResult Bar()
        {
            string result = "adsf";

            //using (var db = new ModelContext())
            //{
            //    var person = db.People.Find(1);
            //    result = person.Name;
            //}
            //var db = new ModelContext();
            //var person = db.People.Find(1);
            //result = person.Name;

            return Json(new { Name = result }, JsonRequestBehavior.AllowGet);
        }
    }
}
