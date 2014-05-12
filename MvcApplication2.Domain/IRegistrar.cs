using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication2.Domain
{
    /* A Registrar is someone capable of doing a registration.
     * A concrete implementation would call then Bootstrap's registration
     */ 
    public interface IRegistrar
    {
        void RegisterType<TFrom, TTo>() where TTo : TFrom;
    }
}
