using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

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
    public class DomainLayer
    {
        // Ninject
        public static IKernel _kernel;

        // Subdomain(s)
        public Measurement measurement {
            get { return DomainLayer._kernel.Get<Measurement>(); }
        }
        
        // constructor. called from setup
        public DomainLayer()
        {
        }

        // singleton: provide instance method
        private static DomainLayer _instance;
        public static DomainLayer Instance
        {
            get {
                if (DomainLayer._instance == null)
                    throw new UninitializedException();
                return DomainLayer._instance; 
            }
            set { DomainLayer._instance = value; }
        }


        // TODO: define arguments
        public static void Setup()
        {
            IKernel kernel = new StandardKernel();
            DomainLayer._kernel = kernel;

            // setup Subdomain(s)
            kernel.Bind<Measurement>()
                  .To<Measurement>()
                  .InSingletonScope();
            
            DomainLayer.Instance = new DomainLayer();
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