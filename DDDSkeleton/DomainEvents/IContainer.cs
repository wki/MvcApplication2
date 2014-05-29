using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSkeleton.Domain;

namespace DDDSkeleton.DomainEvents
{
    public interface IContainer
    {
        List<T> ResolveAll<T>();
    }
}
