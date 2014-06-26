using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcApplication2.Domain.Production;

namespace MvcApplication2.Repository.InMemory
{
    public class AllCards : IAllCards
    {
        private Dictionary<int, Card> storage;

        public AllCards()
        {
            storage = new Dictionary<int, Card>();
            storage.Add(1, new Card(id: 1, name: "Howey", phone: "11833"));
            storage.Add(2, new Card(id: 2, name: "Wolfgang", phone: "4711"));
        }

        public Card ById(int id)
        {
            return storage[id];
        }

        public void Save(Card card)
        {
            storage[card.Id] = card;
        }
    }
}
