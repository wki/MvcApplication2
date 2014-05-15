using Microsoft.Practices.Unity;
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
        private readonly IUnityContainer container;

        public static IEventBus Current { get; private set; }

        public EventBus(IUnityContainer container)
        {
            // maybe we should die if Current is already set.
            Current = this;
            this.container = container;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            _publish<T>(@event, false);
        }

        private void _publish<T>(T @event, bool recursive) where T : class, IEvent
        {
            Console.WriteLine(
                String.Format("Publishing: {0}", @event.GetType())
            );
            var eventHandlers = container.ResolveAll<ISubscribe<T>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }

            // IDEA: call Publish recursively for Base-Class and all Interfaces
            // Remember no of called event handlers and don't call them repeatedly
            // if no handlers are found.

            @event
                .GetType()
                .GetInterfaces()
                .ToList()
                .ForEach(x => Console.WriteLine("Would Call: " + x));

            if (!recursive)
            {
                foreach (var t in @event.GetType().GetInterfaces())
                {
                    //Console.WriteLine("Must call: " + t);
                    MethodInfo publishMethod = this.GetType().GetMethod("publish").MakeGenericMethod(t);
                    publishMethod.Invoke(this, new object[] { @event, true });
                }
            }

            /*
            
            public static T Cast<T>(object o)
            {
                return (T)o;
            }

            // Then invoke this using reflection:

            MethodInfo castMethod = this.GetType().GetMethod("Cast").MakeGenericMethod(t);
            object castedObject = castMethod.Invoke(null, new object[] { obj });
            
            */
        }
    }
}
