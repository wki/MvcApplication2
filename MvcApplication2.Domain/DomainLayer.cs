using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/** Domain Layer
 * 
 * // Singleton, must init with
 * DomainLayer.Setup(...)
 * 
 * // Access subdomains
 * DomainLayer.Instance().measurement
 * 
 * 
 * 
 */

namespace MvcApplication2.Domain
{
    interface Ix { }
    class X : Ix { }

    public class DomainLayer
    {
        // a registrar is a generic variant for registering types
        // think: container.Register<IFoo, Foo>();
        private IRegistrar _registrar;

        //// Subdomain(s)
        //public Measurement measurement {
        //    get { return DomainLayer._kernel.Get<Measurement>(); }
        //}
        
        // constructor. called from setup
        public DomainLayer(IRegistrar registrar)
        {
            _registrar = registrar;

            Setup();
        }

        protected void Setup()
        {
            _registrar.RegisterType<Ix, X>();

            // TODO: Typen registrieren
            // TODO: alle Subdomains durchgehen
            // TODO: alle Services instantiieren
        }
    }

    [Serializable]
    public class UninitializedException : Exception
    {
        public UninitializedException() { }
        public UninitializedException(string message) : base(message) { }
        public UninitializedException(string message, Exception inner) : base(message, inner) { }
        protected UninitializedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}