using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDDSkeleton.Domain;

namespace MvcApplication2.Domain.Production
{
    public class Card : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }

        public Card()
            : base()
        {
        }

        public Card(int id, string name = "", string phone = "") : base(id)
        {
            Name = name;
            Phone = phone;
        }



    }
}
