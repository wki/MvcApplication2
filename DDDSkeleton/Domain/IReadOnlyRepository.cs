using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSkeleton.Domain
{
    public interface IReadOnlyRepository<AggregateType, IdType>
        where AggregateType: IAggregateRoot
    {
        AggregateType FindById(IdType id);
        IEnumerable<AggregateType> FindAll();
    }
}
