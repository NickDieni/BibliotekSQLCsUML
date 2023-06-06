namespace OOPBibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataCheck nyCheck = new DataCheck();
            nyCheck.Check();
            Menu nyMenu = new Menu();
            nyMenu.Pickmenu();
        }
    }
}
