using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQ
{
    class Publish
    {
        public static void Main(string[] args)
        {
            var messageQ = new MessageQ();
            messageQ.Publish("render", "hello there");
            Console.WriteLine("published");
            return;
        }

    }
}
