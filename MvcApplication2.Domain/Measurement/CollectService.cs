using DDDSkeleton.Domain;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication2.Domain.Measurement
{
    public class CollectService : ICollectService, IService
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();

        public CollectService()
        {
            Log.Debug("initializing CollectService");
        }

        public void DoSomething(string message)
        {
            Log.Debug("Doing something with Message: " + message);
        }
    }
}
