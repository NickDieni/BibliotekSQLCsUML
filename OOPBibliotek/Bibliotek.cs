using System.Data.SqlClient;

namespace OOPBibliotek
{
    internal class Bibliotek
    {
        public void PickLib()
        {
            Console.WriteLine("v-- Pick --v");
            Console.WriteLine("");
            Console.WriteLine("1 Borrow a book");
            Console.WriteLine("2 Return a book");
            Console.WriteLine("3 to go back");
            int? Checks = int.Parse(Console.ReadLine());
            switch (Checks)
            {
                case 1:
                    Console.Clear();
                    BorrowBook();
                    break;
                case 2:
                    Console.Clear();
                    ReturnBook();
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
        public void BorrowBook()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";

            List<Book> books = new List<Book>();

            using (SqlConnection con = new SqlConnection(cstring))
            {
                con.Open();

                string sqlQuery2 = "SELECT * FROM Books WHERE Borrowed = 'No'";

                using (SqlCommand command = new SqlCommand(sqlQuery2, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Title = reader["Bookname"].ToString(),
                                AuthorId = Convert.ToInt32(reader["AuthorID"]),
                                Borrow = reader["Borrowed"].ToString(),
                                Borrowname = reader["Borrowname"].ToString()
                            };
                            books.Add(book);
                        }
                    }
                }
                con.Close();
            }

            foreach (Book book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author ID: {book.AuthorId}, Borrowed: {book.Borrow}, Borrower Name: {book.Borrowname}");
            }

            Console.WriteLine();
            Console.Write("Pick what book you want to borrow or write B to go back: ");
            Console.WriteLine();
            Book book1 = new Book();
            book1.Title = Console.ReadLine();
            if (book1.Title == "B" || book1.Title == "b")
            {
                Console.Clear();
                PickLib();
            }


            Console.WriteLine();
            Console.Write("Write your name: ");
            Console.WriteLine();
            string BorrowName = Console.ReadLine();
            bool LengthBool = BorrowName.Length >= 1;
            if (LengthBool == false || BorrowName == null) Error();


            SqlConnection con2 = new SqlConnection(cstring);
            con2.Open();
            string sqlQuery = $"UPDATE Books SET Borrowed = 'Yes', Borrowname = '{BorrowName}' WHERE Bookname = '{book1.Title}'";

            using (SqlCommand command = new SqlCommand(sqlQuery, con2))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected successfully: {rowsAffected}");
            }
            con2.Close();

            Console.WriteLine("Success");
            Thread.Sleep(2000);
            Console.Clear();
            con2.Close();
            PickLib();
        }
        public void ReturnBook()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";

            List<Book> books = new List<Book>();

            using (SqlConnection con = new SqlConnection(cstring))
            {
                con.Open();

                string sqlQuery2 = "SELECT * FROM Books WHERE Borrowed = 'Yes'";

                using (SqlCommand command = new SqlCommand(sqlQuery2, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Title = reader["Bookname"].ToString(),
                                AuthorId = Convert.ToInt32(reader["AuthorID"]),
                                Borrow = reader["Borrowed"].ToString(),
                            };
                            books.Add(book);
                        }
                    }
                }
                con.Close();
            }

            foreach (Book book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author ID: {book.AuthorId}, Borrowed: {book.Borrow}");
            }

            Console.WriteLine();
            Console.Write("Pick what book you want to return or write B to go back: ");
            Book book1 = new Book();
            book1.Title = Console.ReadLine().ToLower();

            if (book1.Title == "B" || book1.Title == "b")
            {
                Console.Clear();
                PickLib();
            }

            SqlConnection con2 = new SqlConnection(cstring);
            con2.Open();
            string sqlQuery = $"UPDATE Books SET Borrowed = 'No', Borrowname = 'None' WHERE Bookname = '{book1.Title}'";
            using (SqlCommand command = new SqlCommand(sqlQuery, con2))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected successfully: {rowsAffected}");
            }
            con2.Close();

            Console.WriteLine("Success");
            Thread.Sleep(2000);
            Console.Clear();
            con2.Close();
            PickLib();
        }
        public void Error()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);
            Console.Clear();
            con.Close();
            Console.WriteLine("Error try again");
            Thread.Sleep(2000);
            PickLib();
        }
    }

}
