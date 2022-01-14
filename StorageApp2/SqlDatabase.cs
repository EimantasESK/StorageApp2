using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace StorageApp2
{
    class SqlDatabase
    {
        const string conString = "Server=.;Trusted_Connection=True;Initial catalog=StoreApp2";
        public static void CreateDb()
        {
            string connectionString = "Server=.; Trusted_Connection=True;";
            string sqlStatement = "create database StoreApp2";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                Console.WriteLine("Conncetion open");
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Database was created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                Console.WriteLine("Connection closed");
            }
        }
        public static void CreateTable()
        {
            string sqlStatement = "create table InventoryList (ID uniqueidentifier primary key default NEWID()," +
                "NAME varchar(50), PRICE money)";
            SqlConnection sqlConnection = new SqlConnection(conString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                Console.WriteLine("Conncetion open");
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                Console.WriteLine("Connection closed");
            }
        }
        
    }
}
