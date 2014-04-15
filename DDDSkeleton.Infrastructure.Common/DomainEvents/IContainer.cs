using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSkeleton.Infrastructure.Common.Domain;

namespace DDDSkeleton.Infrastructure.Common.DomainEvents
{
    public interface IContainer
    {
        List<T> ResolveAll<T>();
    }
}
