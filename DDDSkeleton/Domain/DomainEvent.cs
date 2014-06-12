using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSkeleton.EventBus;

namespace DDDSkeleton.Domain
{
    public class DomainEvent : IEvent
    {
        public DateTime OccuredOn { get; private set; }

        public DomainEvent()
        {
            OccuredOn = DateTime.Now;
        }
    }
}
