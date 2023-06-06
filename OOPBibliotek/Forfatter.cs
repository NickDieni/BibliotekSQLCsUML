using System.Data.SqlClient;


namespace OOPBibliotek
{
    internal class Forfatter
    {
        public void Authorpick()
        {
            DataCheck nyCheck = new DataCheck();
            nyCheck.Check();

            Console.WriteLine("v-- Pick --v");
            Console.WriteLine("");
            Console.WriteLine("1 Add a author");
            Console.WriteLine("2 Delete a author");
            Console.WriteLine("3 to go back");
            int? Checks = int.Parse(Console.ReadLine());
            switch (Checks)
            {
                case 1:
                    Console.Clear();
                    AddAuthor();
                    break;
                case 2:
                    Console.Clear();
                    DeleteAuthor();
                    break;
                case 3:
                    Console.Clear();
                    Menu nyMenu = new Menu();
                    nyMenu.Pickmenu();
                    break;
                default:
                    Error();
                    break;
            }
        }
        public void AddAuthor()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);
            con.Open();
            Book book = new Book();

            Console.Write("Enter the name of the author or write B to go back: ");
            book.Author = Console.ReadLine();
            bool LengthBool = book.Author.Length >= 1;
            if (LengthBool == false || book.Author == null) Error();
            if (book.Author == "B" || book.Author == "b")
            {
                Console.Clear();
                Authorpick();
            }
            Console.WriteLine();

            string sqlQuery = $"INSERT INTO Authors (Authornames) VALUES ('{book.Author}')";
            using (SqlCommand command = new SqlCommand(sqlQuery, con))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected successfully: {rowsAffected}");
            }
            Thread.Sleep(2000);
            Console.Clear();
            con.Close();
            Authorpick();
        }
        public void DeleteAuthor()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";

            List<Book> Authors = new List<Book>();

            using (SqlConnection con = new SqlConnection(cstring))
            {
                con.Open();

                string sqlQuery = "SELECT * FROM Authors";

                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = Convert.ToInt32(reader["AuthorsID"]),
                                Author = reader["Authornames"].ToString(),
                            };
                            Authors.Add(book);
                        }
                    }
                }
                con.Close();
            }

            foreach (Book book in Authors)
            {
                Console.WriteLine($"Author: {book.Author}");
            }

            Console.WriteLine();
            Console.Write("Write the name of a author you want to delete or write B to go back: ");
            string Deletename = Console.ReadLine();
            SqlConnection con2 = new SqlConnection(cstring);
            if (Deletename == "B" || Deletename == "b")
            {
                Console.Clear();
                Authorpick();
            }
            con2.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM Authors WHERE Authornames='{Deletename}'", con2);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Success");
            Thread.Sleep(2000);
            Console.Clear();
            con2.Close();
            Authorpick();
        }
        public void Error()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);
            Console.Clear();
            con.Close();
            Console.WriteLine("Error try again");
            Thread.Sleep(2000);
            Authorpick();
        }
    }
}
