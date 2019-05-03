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

            //var actors = new Director(token).GetDirectorsAsync().GetAwaiter().GetResult();
            //foreach (var actor in actors)
            //{
            //    Console.WriteLine(actor.id + " " + actor.name + " " + actor.birthday);
            //}
            //Console.WriteLine(actors);
            //var actor = new Director(token).GetDirectorAsync(289).GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Director(token).
            //    CreateDirectorAsync(new Director { name = "lak", birthday = DateTime.Now.Date.ToString("yyyy-MM-dd") }).
            //    GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Director(token).
            //    UpdateDirectorAsync(new Director { id = 323, name = "Lus", birthday = DateTime.Now.Date.ToString("yyyy-MM-dd") })
            //    .GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Director(token).DeleteDirectorAsync(323).GetAwaiter().GetResult();
            //Console.WriteLine(actor);
            Console.ReadKey();

        }
    }
}
