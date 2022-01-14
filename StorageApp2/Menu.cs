using StorageApp2.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StorageApp2
{
    class Menu
    {
        InventoryRepository inventoryRepository = new InventoryRepository();
        const string conString = "Server=.;Trusted_Connection=True;Initial catalog=StoreApp2";

        public void Run()
        {
            Console.WriteLine("Store actions:");

            int action = ChoooseAction();

            while (action != 0)
            {
                Console.WriteLine("You choose: {0}", action);

                switch (action)
                {
                    case 1:
                        CreateItem();
                        //CreateItemSql();
                        break;

                    case 2:
                        PrintInventory();
                        //PrintInventorySql();
                        break;

                    case 3:
                        DeleteItem();
                        //DeleteItemSql();
                        break;

                    case 4:
                        UpdateItem();
                        //UpdateItemSql();
                        break;

                    case 5:
                        AddShopCart();
                        break;

                    case 6:
                        ShowShopCart();
                        break;

                    case 7:
                        DeleteFromShopCart();
                        break;

                    case 8:
                        BuyItems();
                        break;

                    case 9:
                        OrderItems();
                        break;

                    case 10:
                        FindItem();
                        break;

                    default:
                        break;
                }

                action = ChoooseAction();
            }
        }
        public static int ChoooseAction()
        {
            int choice = 0;
            Console.WriteLine("1 - create item\n2 - show all inventory\n3 - delete" +
                            "\n4 - update item\n5 - add item to shopping cart" +
                            "\n6 - show all item in shopping cart\n7 - delete item form cart" +
                            "\n8 - buy\n9 - order item\n10 - find item by name\n0 - exit");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        public void CreateItem()
        {
            Console.WriteLine("You choose create item");
            Console.Write("Item name: ");
            string itName = Console.ReadLine().ToLower();
            Console.Write("Item price: ");
            decimal itPrice = decimal.Parse(Console.ReadLine());
            inventoryRepository.Create(itName, itPrice);
        }
        public void PrintInventory()
        {
            List<Inventory> items = inventoryRepository.GetAll();

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine("#" + (i + 1) + " " + items[i]);
            }
        }
        public void PrintShopCart()
        {
            List<Inventory> items = inventoryRepository.GetAllShop();

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine("#" + (i + 1) + " " + items[i]);
            }
        }
        public void DeleteItem()
        {
            Console.WriteLine("You choose delete item");
            Console.WriteLine("Item list:");
            PrintInventory();
            Console.WriteLine("Enter item id to delete: ");
            Guid deleteNum = Guid.Parse(Console.ReadLine());
            inventoryRepository.Delete(deleteNum);
        }
        public void UpdateItem()
        {
            Console.WriteLine("You choose update item");
            Console.WriteLine("Item list:");
            PrintInventory();
            Console.WriteLine("Enter item id to update: ");
            Guid updateNum = Guid.Parse(Console.ReadLine());
            Console.WriteLine("Add updated item Name: ");
            string updateName = Console.ReadLine();
            Console.WriteLine("Add updated item Price");
            decimal updatePrice = decimal.Parse(Console.ReadLine());
            inventoryRepository.Update(updateNum, updateName, updatePrice);
        }
       public void AddShopCart()
        {
            Console.WriteLine("You choose to add inventory to shopping cart");
            Console.WriteLine("Item list:");
            PrintInventory();
            Console.WriteLine("Select item number you want to buy:");
            int cartChosen = int.Parse(Console.ReadLine());
            inventoryRepository.ShopingCart(cartChosen);
            Console.WriteLine("Your shopping list:");
            PrintShopCart();
            Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
            Console.WriteLine("Item added to chart!");
        }
        public void ShowShopCart()
        {
            Console.WriteLine("You choose to show inventory to shopping cart:");
            PrintShopCart();
            Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
        }
       public void DeleteFromShopCart()
        {
            Console.WriteLine("You choose delete item form shopping cart");
            Console.WriteLine("Item list:");
            PrintShopCart();
            Console.WriteLine("Select item number to delete: ");
            int deleteItem = int.Parse(Console.ReadLine());
            inventoryRepository.DeleteShopItem(deleteItem);
        }
        public void BuyItems()
        {
            Console.WriteLine("You choose buy item(s)");
            Console.WriteLine("Item list:");
            PrintShopCart();
            Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
            Console.WriteLine("Thank you for buying from us! ");
            inventoryRepository.BuyItem();
        }
        public void OrderItems()
        {
            Console.WriteLine("You choose order items");
            Console.WriteLine("We have two options:\nPress \"N\" to order by name\nPress \"P\" to order by price ");
            char orderChoice = char.Parse(Console.ReadLine().Trim().ToLower());

            if (orderChoice == 'n')
            {
                inventoryRepository.OrderByName();
            }
            else if (orderChoice == 'p')
            {
                inventoryRepository.OrderByPrice();
            }
            else
            {
                Console.WriteLine("Ups something wrong please try again");
            }
        }
        public void FindItem()
        {
            Console.WriteLine("You choose find item");
            Console.WriteLine("Please enter item name or first letter:");
            string findWord = Console.ReadLine().ToLower();
            inventoryRepository.FindPartByName(findWord);
        }
        public static void CreateItemSql()
        {
            Console.WriteLine("You choose create item");
            Console.Write("Item name: ");
            string itName = Console.ReadLine().ToLower();
            Console.Write("Item price: ");
            decimal itPrice = decimal.Parse(Console.ReadLine());
            
            string sqlStatement = $"insert into InventoryList(NAME,PRICE) values ('{itName}',{itPrice})";
            SqlConnection sqlConnection = new SqlConnection(conString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Added to Invenotry list");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }
        private void PrintInventorySql()
        {
            string sqlStatement = "select * from InventoryList";
            SqlConnection sqlConnection = new SqlConnection(conString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Console.WriteLine($"{sqlDataReader.GetGuid(0)} | {sqlDataReader.GetString(1)} | {sqlDataReader.GetDecimal(2)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void DeleteItemSql()
        {
            Console.WriteLine("You choose delete item");
            Console.WriteLine("Item list:");
            PrintInventorySql();
            Console.WriteLine("Enter item id to delete: ");
            Guid deleteNum = Guid.Parse(Console.ReadLine());

            string sqlStatement = $"delete from InventoryList where ID = '{deleteNum}'";
            SqlConnection sqlConnection = new SqlConnection(conString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void UpdateItemSql()
        {
            Console.WriteLine("You choose update item");
            Console.WriteLine("Item list:");
            PrintInventorySql();
            Console.WriteLine("Enter item id to update: ");
            Guid updateNum = Guid.Parse(Console.ReadLine());
            Console.WriteLine("Add updated item Name: ");
            string updateName = Console.ReadLine();
            Console.WriteLine("Add updated item Price");
            decimal updatePrice = decimal.Parse(Console.ReadLine());

            SqlConnection sqlConnection = new SqlConnection(conString);
            string sqlStatement = $"update InventoryList set NAME = '{updateName}' where ID = '{updateNum}'";
            string sqlStatement2 = $"update InventoryList set PRICE = '{updatePrice}' where ID = '{updateNum}'";
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStatement,sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand(sqlStatement2,sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
