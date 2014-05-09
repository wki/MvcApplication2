using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication2
{

    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;

        public UnityControllerFactory(IUnityContainer container)
        {
            _container = container;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                return (IController)_container.Resolve(GetControllerType(requestContext, controllerName));
            }
            catch
            {
                return base.CreateController(requestContext, controllerName);
            }
        }

        public override void ReleaseController(IController controller)
        {
            _container.Teardown(controller);

            base.ReleaseController(controller);
        }
    }
}
