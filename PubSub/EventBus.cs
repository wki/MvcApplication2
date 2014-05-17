using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IWindsorContainer container;

        public static IEventBus Current { get; private set; }

        public EventBus(IWindsorContainer container)
        {
            // maybe we should die if Current is already set.
            Current = this;
            this.container = container;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            Console.WriteLine("");
            _publish<T>(@event, false);
        }

        // must be public to allow reflection to find it
        public void _publish<T>(T @event, bool recursive) where T : class, IEvent
        {
            Console.WriteLine(
                String.Format("Publishing: {0} Type: {1}", @event.GetType(), typeof(T))
            );
            var eventHandlers = container.ResolveAll<ISubscribe<T>>();
            foreach (var eventHandler in eventHandlers)
            {
                Console.WriteLine("Handler: " + eventHandler);
                eventHandler.Handle(@event);
            }

            if (!recursive)
            {
                foreach (var t in @event.GetType().GetInterfaces())
                {
                    MethodInfo publishMethod = this.GetType().GetMethod("_publish").MakeGenericMethod(t);

                    // Console.WriteLine("Method: " + publishMethod);
                    publishMethod.Invoke(this, new object[] { @event, true });
                }
            }
        }
    }
}
