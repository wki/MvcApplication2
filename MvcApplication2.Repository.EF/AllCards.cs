using MvcApplication2.Domain.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication2.Repository.EF
{
    public class AllCards : IAllCards
    {
        private readonly RepositoryContext context;

        public AllCards(RepositoryContext context)
        {
            this.context = context;
        }

        public Card ById(int id)
        {
            return new Card(42, "", "");
        }

        public void Save(Card card)
        {
           
        }
    }
}
