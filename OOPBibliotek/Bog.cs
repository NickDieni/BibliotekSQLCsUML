using System.Data.SqlClient;


namespace OOPBibliotek
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Borrow { get; set; }
    }
    internal class Bog
    {
        public void PickBook()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = Passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);

            Console.WriteLine("v-- Pick --v");
            Console.WriteLine("");
            Console.WriteLine("1 to add book");
            Console.WriteLine("2 to delete book");
            Console.WriteLine("3 to go back");
            int? Checks = int.Parse(Console.ReadLine());
            switch (Checks)
            {
                case 1:
                    Console.Clear();
                    AddBook();
                    break;
                case 2:
                    Console.Clear();
                    DeleteBook();
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
        public void AddBook()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);
            con.Open();
            Book book = new Book();

            Console.Write("Enter the name of the book: ");
            book.Title = Console.ReadLine();
            bool LengthBool = book.Title.Length >= 1;
            if (LengthBool == false || book.Title == null) Error();
            Console.WriteLine();

            Console.Write("Enter the author of the book: ");
            book.Author = Console.ReadLine();
            LengthBool = book.Author.Length >= 1;
            if (LengthBool == false || book.Author == null) Error();
            Console.WriteLine();

            bool authorExists = CheckAuthorExists(cstring, book.Author);
            if (authorExists)
            {
                string sqlQuery = $"INSERT INTO Books (Bookname, Authorname) VALUES ('{book.Title}', '{book.Author}')";

                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected successfully: {rowsAffected}");
                }
                Thread.Sleep(2000);
                Console.Clear();
                con.Close();
                PickBook();
            }
            else
            {
                Console.WriteLine("Author does not exist. Cannot insert book.");
                Thread.Sleep(2000);
                Console.Clear();
                con.Close();
                PickBook();
            }
        }
        public static bool CheckAuthorExists(string cstring, string bookauthor)
        {
            using (SqlConnection connection = new SqlConnection(cstring))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT (*) FROM Authors WHERE Authornames = @AuthorName";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@AuthorName", bookauthor);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }

            }
        }
        public void DeleteBook()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";

            List<Book> books = new List<Book>();

            using (SqlConnection con = new SqlConnection(cstring))
            {
                con.Open();

                string sqlQuery = "SELECT * FROM Books";

                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = Convert.ToInt32(reader["BooksID"]),
                                Title = reader["Bookname"].ToString(),
                                Author = reader["Authorname"].ToString(),
                            };
                            books.Add(book);
                        }
                    }
                }
                con.Close();
            }

            foreach (Book book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
            }

            Console.WriteLine();
            Console.Write("Write the book you want to delete: ");
            string Deletename = Console.ReadLine();
            SqlConnection con2 = new SqlConnection(cstring);

            con2.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM Books WHERE Bookname='{Deletename}'", con2);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Success");
            Thread.Sleep(2000);
            Console.Clear();
            con2.Close();
            PickBook();
        }
        public void Error()
        {
            string cstring = "Server = 192.168.23.112,1433; Uid = Nick ;Pwd = passw0rd; Database = Bibliotek;";
            SqlConnection con = new SqlConnection(cstring);
            Console.Clear();
            con.Close();
            Console.WriteLine("Error try again");
            Thread.Sleep(2000);
            PickBook();
        }
    }
}