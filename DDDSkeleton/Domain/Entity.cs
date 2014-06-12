using DDDSkeleton.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSkeleton.Domain
{
    public class Entity<IdType> : DomainObject, IPublish
    {
        public IdType Id { get; private set; }

        public Entity()
        { }

        public Entity(IdType id)
        {
            Id = id;
        }

        public override bool Equals(Object entity)
        {
            return entity != null
                && entity is Entity<IdType>
                && this == (Entity<IdType>)entity;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<IdType> entity1, Entity<IdType> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            return entity1.Id.ToString() == entity2.Id.ToString();
        }

        public static bool operator !=(Entity<IdType> entity1, Entity<IdType> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
