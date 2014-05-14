using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public interface ISubscribe<T>
    {
        void Handle(T @event);
    }
}
