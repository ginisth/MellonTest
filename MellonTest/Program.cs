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
            //var actors = new Actor(token).GetActorsAsync().GetAwaiter().GetResult();
            //foreach (var actor in actors)
            //{
            //    Console.WriteLine(actor.id + " " + actor.name + " " + actor.birthday);
            //}
            //Console.WriteLine(actors);
            //var actor = new Actor(token).GetActorAsync(483).GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Actor(token).
            //    CreateActorAsync(new Actor { name = "lak", birthday = DateTime.Now.Date.ToString("yyyy-MM-dd") }).
            //    GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Actor(token).
            //    UpdateActorAsync(new Actor { id = 491, name = "Lus", birthday = DateTime.Now.Date.ToString("yyyy-MM-dd") })
            //    .GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            var actor = new Actor(token).DeleteActorAsync(491).GetAwaiter().GetResult();
            Console.WriteLine(actor);
            Console.ReadKey();

        }
    }
}
