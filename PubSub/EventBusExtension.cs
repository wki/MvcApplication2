using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public static class EventBusExtension
    {
        public static void Publish<T>(this object obj, IEvent @event)
        {
            EventBus.Current.Publish(@event);
        }
    }
}
