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
            Console.WriteLine("1 to books");
            Console.WriteLine("2 to authors");
            Console.WriteLine("3 to library");

            int? Checks = int.Parse(Console.ReadLine());

            switch (Checks)
            {
                case 1:
                    Console.Clear();
                    nyBog.PickBook();
                    break;
                case 2:
                    Console.Clear();
                    nyFor.AuthorPick();
                    break;
                case 3:
                    Console.Clear();
                    nyBib.PickLib();
                    break;
                default:
                    Console.WriteLine("Error try again");
                    Console.ReadKey();
                    Pickmenu();
                    break;
            }
        }
    }
}
