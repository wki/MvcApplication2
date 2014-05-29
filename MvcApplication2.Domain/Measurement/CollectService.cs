using DDDSkeleton.Domain;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication2.Domain.Measurement
{
    public class CollectService : ICollectService, IService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CollectService()
        {
            logger.Debug("initializing CollectService");
        }

        public void DoSomething(string message)
        {
            logger.Debug("Doing something with Message: " + message);
        }
    }
}
