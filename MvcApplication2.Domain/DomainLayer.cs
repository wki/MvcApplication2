using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

/** Domain Layer
 * 
 * // we need an instance of a custom registrar to be independent from our DI container
 * var domain = new DomainLayer(registrar);
 * 
 * // Access subdomains
 * domain.measurement
 * 
 * 
 * 
 */

namespace MvcApplication2.Domain
{
    public class DomainLayer
    {
        private readonly IWindsorContainer container;

        //// Subdomain(s)
        //public Measurement measurement {
        //    get { return DomainLayer._kernel.Get<Measurement>(); }
        //}
        
        // constructor. called from setup
        public DomainLayer(IWindsorContainer container)
        {
            this.container = container;

            Setup();
        }

        protected void Setup()
        {
            // container.RegisterType<Ix, X>();

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