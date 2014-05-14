using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IUnityContainer container;

        public static IEventBus Current { get; private set; }

        public EventBus(IUnityContainer container)
        {
            // maybe we should die if Current is already set.
            Current = this;
            this.container = container;
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var eventHandlers = container.ResolveAll<ISubscribe<T>>();
            // var eventHandlers = container.ResolveAll<ISubscribe<GoodThing>>();
            Console.WriteLine(
                String.Format(@"For {0} Caught {1} handlers",
                    @event.GetType().ToString(),
                    eventHandlers.Count()
                )
            );

            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }
}
