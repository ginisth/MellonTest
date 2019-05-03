using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClient;

namespace MellonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = new GetToken().CatchToken("TestUser", "ZfuzpbZ8Mo4").GetAwaiter().GetResult();
            Console.WriteLine($"Token:{token}");
            var actors = new Actors(token).GetActors().GetAwaiter().GetResult();
            foreach (var actor in actors)
            {
                Console.WriteLine(actor.Id + " " + actor.Name + " " + actor.Birthday);
            }
            Console.WriteLine(actors);
            Console.ReadKey();

        }
    }
}
