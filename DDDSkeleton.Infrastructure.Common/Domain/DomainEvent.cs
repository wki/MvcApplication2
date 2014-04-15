using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSkeleton.Infrastructure.Common.Domain
{
    public class DomainEvent
    {
        private DateTime _occuredOn = DateTime.Now;

        public DomainEvent()
        {
        }

        public DateTime OccuredOn()
        {
            return _occuredOn;
        }
    }
}
