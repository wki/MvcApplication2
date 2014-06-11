using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSkeleton.Domain;

namespace MvcApplication2.Domain.Production
{
    public interface IAllCards : IRepository
    {
        Card ById(int id);
        void Save(Card card);
    }
}
