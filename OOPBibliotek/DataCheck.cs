using System;
using System.Data.SqlClient;
using System.Threading;

namespace OOPBibliotek
{
    internal class DataCheck
    {
        public void Check()
        {
            Menu nyMenu = new Menu();
            string cstring = "Server=192.168.23.112,1433; Uid=Nick; Pwd=passw0rd; Database=Bibliotek;";

            try
            {
                using (SqlConnection conn = new SqlConnection(cstring))
                {
                    conn.Open();
                    Console.WriteLine("SQL Connection established");
                    Thread.Sleep(1000);
                    Console.Clear();
                    return;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Thread.Sleep(1000);
                Console.Clear();
                nyMenu.Pickmenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
                throw;
            }
        }
    }
}
