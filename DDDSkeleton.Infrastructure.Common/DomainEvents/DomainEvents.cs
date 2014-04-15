using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSkeleton.Infrastructure.Common.Domain;

namespace DDDSkeleton.Infrastructure.Common.DomainEvents
{
    /* Idee hinter den Domain Events:
     *  - Aggregates senden Events
     *    DomainEvents.Raise(new SomethingsHappened(...))
     *    
     *  - Services empfangen Events
     *    class Service : IHandle<SomethingHappened> 
     *    {
     *        public void Handle(SomethingHappened e) { }
     *    }
     *    
     *  - wie Vermitteln?
     *     * private Dictionary<Type, event Action<DomainEvent>> observers;
     *     * alle Events suchen
     *     * alle Handler jedes Events finden, eintragen, fertig.
     *     
     *     * Attribute
     *       [DomainEventHandler]
     *       public Handle(SomethingHappened) { }
     *  
     */
    static public class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;

        // public static IContainer Container { get; set; }

        public static void Register<Event>(Action<Event> callback)
            where Event: DomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<Event>(Event eventRaised)
            where Event: DomainEvent
        {
            // regular case: container looks up a handler
            //if (Container != null)
            //{
            //    foreach (IHandle<Event> handler in Container.ResolveAll<IHandle<Event>>())
            //        handler.Handle(eventRaised);
            //}

            // testing only: check for actions
            if (actions != null)
            {
                foreach (var action in actions)
                    if (action is Action<Event>)
                        ((Action<Event>)action)(eventRaised);
            }
        }
    }
}
