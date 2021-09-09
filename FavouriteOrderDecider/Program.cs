using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavouriteOrderDecider
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("entrants.txt");

            var entrants = new List<Entrant>();
            foreach (var line in lines)
            {
                entrants.Add(new Entrant(line, 0));
            }

            var alreadyVotedOn = new List<string>();

            foreach (var entrantA in entrants)
            {
                foreach (var entrantB in entrants.Where(e => e.Name != entrantA.Name))
                {
                    if (alreadyVotedOn.Contains(entrantB.Name))
                    {
                        continue;
                    }

                    Console.WriteLine($"1 - {entrantA.Name}, 2 - {entrantB.Name}");
                    var input = Console.ReadKey().Key;
                    if (input == ConsoleKey.D1)
                    {
                        entrants.FirstOrDefault(e => e.Name == entrantA.Name).Score++;
                    }
                    else if (input == ConsoleKey.D2)
                    {
                        entrants.FirstOrDefault(e => e.Name == entrantB.Name).Score++;
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine();
                }
                alreadyVotedOn.Add(entrantA.Name);
            }

            var result = entrants.OrderByDescending(e => e.Score).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
    }

    public class Entrant
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Entrant(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

}
