using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Reflection;

namespace MvcApplication2.Tests.Routes
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string url = null, string method = "GET")
        {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest
                .Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(url);
            mockRequest
                .Setup(m => m.HttpMethod)
                .Returns(method);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse
                .Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext
                .Setup(m => m.Request)
                .Returns(mockRequest.Object);
            mockContext
                .Setup(m => m.Response)
                .Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object properties)
        {
            Func<object, object, bool> compare = (v1,v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            bool result = compare(routeResult.Values["controller"], controller)
                && compare(routeResult.Values["action"], action);

            if (properties != null) {
                PropertyInfo[] propertyInfo = properties.GetType().GetProperties();
                foreach (var pi in propertyInfo) {
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                            && compare(routeResult.Values[pi.Name], pi.GetValue(properties, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private RouteData GetRouteData(string url, string method = "GET")
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            return routes.GetRouteData(CreateHttpContext(url, method));
        }

        private void TestRouteMatch(string url, string controller, string action, object properties = null, string method = "GET")
        {
            var result = GetRouteData(url, method);

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, properties));
        }

        private void TestRouteFail(string url)
        {
            var result = GetRouteData(url);

            // Assert.AreEqual("Home", result.Values["controller"]);
            // Assert.AreEqual("Xxx", result.Values["action"]);
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestHomeRoutes()
        {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Home", "Home", "Index");
            TestRouteMatch("~/Home/Index", "Home", "Index");
            TestRouteMatch("~/Home/Foo", "Home", "Foo");
        }

        [TestMethod]
        public void TestFailingRoutes()
        {
            // does not work, from a routing-point-of-view this route is OK.
            // actually locating the action method will fail, not the routing...
            // TestRouteFail("~/Home/Xxx");

            TestRouteFail("~/Home/Index/Bla/Blubb");
            Assert.IsTrue(true);
        }
    }
}
