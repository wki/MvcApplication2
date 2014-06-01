using Castle.Windsor;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class Hub : IHub
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IWindsorContainer container;

        public static IHub Current { get; private set; }

        public Hub(IWindsorContainer container)
        {
            // maybe we should die if Current is already set.
            Current = this;
            this.container = container;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            Log.Debug(m => m("Publish: {0} Type: {1}", @event.GetType(), typeof(T)));
            
            DispatchEventClass<T>(@event);
            DispatchParentInterfaces(@event);
        }

        // must be public to allow reflection to find it
        public void DispatchEventClass<T>(T @event) where T : class, IEvent
        {
            var eventHandlers = container.ResolveAll<ISubscribe<T>>();
            foreach (var eventHandler in eventHandlers)
            {
                Log.Debug(m => m("Handler: {0}", eventHandler));
                eventHandler.Handle(@event);
            }
        }

        public void DispatchParentInterfaces(IEvent @event)
        {
            // Hint: this Query could be cached.
            foreach (var t in @event.GetType().GetInterfaces().Where(t => typeof(IEvent).IsAssignableFrom(t)))
            {
                MethodInfo publishMethod = this.GetType().GetMethod("DispatchEventClass").MakeGenericMethod(t);

                publishMethod.Invoke(this, new object[] { @event } );
            }
        }
    }
}
