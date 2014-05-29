Domain - Aufbau
---------------


namespace DDD // Basisklassen
	
	abstract class DomainObject

	abstract class DomainEvent
		public DateTime occuredOn { get; private set; }

	abstract class EntityBase<IdType>
		public IdType id { get; private set; }

	abstract class ValueObjectBase

	interface AggregateRoot

	interface IDomainService // zum Finden von Services allgemein


namespace MvcApplication2.Domain  // Domain Implementierung
    interface IAllXxx // sample repository

	interface IYyyService : IDomainService

	class YyyService : IYyyService


namespace MvcApplication2.Infrastructure.Memory // in-memory repositories
    class AllXxx : IAllXxx


namespace MvcApplication2.Infrastructure.Ef // EF repositories
    class AllXxx : IAllXxx
