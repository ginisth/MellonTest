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

            //var actors = new Movie(token).GetMoviesAsync().GetAwaiter().GetResult();
            //foreach (var actor in actors)
            //{
            //    Console.WriteLine(actor.id + " " + actor.name + " " + actor.year + " " + actor.director +
            //         " " + actor.actors[0] );
            //}
            //Console.WriteLine(actors);
            //var actor = new Movie(token).GetMovieAsync(50).GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Movie(token).
            //    CreateMovieAsync(new Movie { name = "lak",year=2019,director=305,actors=new List<int>() {435,440 } }).
            //    GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            //var actor = new Movie(token).
            //    UpdateMovieAsync(new Movie {id=71, name = "lak", year = 2018, director = 305, actors = new List<int>() { 420, 440 } })
            //    .GetAwaiter().GetResult();
            //Console.WriteLine(actor);

            var actor = new Movie(token).DeleteMovieAsync(71).GetAwaiter().GetResult();
            Console.WriteLine(actor);
            Console.ReadKey();

        }
    }
}
