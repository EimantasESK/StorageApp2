using System;
using System.Data;
using System.Data.SqlClient;
namespace StorageApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SqlDatabase.CreateDb();
            //SqlDatabase.CreateTable();
            Menu run = new Menu();
            run.Run();
        }
        
    }
}
