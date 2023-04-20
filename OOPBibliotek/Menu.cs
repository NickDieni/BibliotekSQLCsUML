using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPBibliotek
{
    internal class Menu
    {
        public void Pickmenu()
        {
            Bibliotek nyBib = new Bibliotek();
            Bog nyBog = new Bog();
            Forfatter nyFor = new Forfatter();

            Console.WriteLine("v-- Pick --v");
            Console.WriteLine("");
            Console.WriteLine("1 to add books");
            Console.WriteLine("2 to add authors");
            Console.WriteLine("3 for library");

            string? Checks = Console.ReadLine();

            switch (Checks)
            {
                case "1":
                    Console.Clear();
                    nyBog
                    break;
                case "2":
                    Console.Clear();
                    nyFor
                    break;
                case "3":
                    nyBib
                    break;
                default:
                    Console.WriteLine("Fejl prøv igen");
                    Console.ReadKey();
                    Pickmenu();
                    break;
            }
        }
    }
}
