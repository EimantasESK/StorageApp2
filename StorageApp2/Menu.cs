using StorageApp2.repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StorageApp2
{
    class Menu
    {
        public void Run()
        {
            
            InventoryRepository inventoryRepository = new InventoryRepository();
            inventoryRepository.Deserialize();

            Console.WriteLine("Store actions:");

            int action = choooseAction();

            while (action != 0)
            {
                Console.WriteLine("You choose: {0}", action);

                switch (action)
                {
                    case 1:
                        Console.WriteLine("You choose create item");

                        string itName = "";
                        decimal itPrice = 0;

                        Console.Write("Item name: ");
                        itName = Console.ReadLine().ToLower();

                        Console.Write("Item price: ");
                        itPrice = decimal.Parse(Console.ReadLine());
                        inventoryRepository.Create(itName, itPrice);
                        inventoryRepository.Save();
                        break;

                    case 2:
                        printInventory(inventoryRepository.GetAll());
                        break;

                    case 3:
                        Console.WriteLine("You choose delete item");
                        Console.WriteLine("Item list:");
                        printInventory(inventoryRepository.GetAll());
                        Console.WriteLine("Enter item id to delete: ");
                        Guid deleteNum = Guid.Parse(Console.ReadLine());
                        inventoryRepository.Delete(deleteNum);
                        inventoryRepository.Save();
                        break;

                    case 4:
                        Console.WriteLine("You choose update item");
                        Console.WriteLine("Item list:");
                        printInventory(inventoryRepository.GetAll());
                        Console.WriteLine("Enter item id to update: ");
                        Guid updateNum = Guid.Parse(Console.ReadLine());
                        Console.WriteLine("Add updated item Name: ");
                        string updateName = Console.ReadLine();
                        Console.WriteLine("Add updated item Price");
                        decimal updatePrice = decimal.Parse(Console.ReadLine());
                        inventoryRepository.Update(updateNum, updateName, updatePrice);
                        inventoryRepository.Save();
                        break;

                    case 5:
                        Console.WriteLine("You choose to add inventory to shopping cart");
                        Console.WriteLine("Item list:");
                        printInventory(inventoryRepository.GetAll());
                        Console.WriteLine("Select item number you want to buy:");
                        int cartChosen = int.Parse(Console.ReadLine());
                        inventoryRepository.ShopingCart(cartChosen);
                        Console.WriteLine("Your shopping list:");
                        printInventory(inventoryRepository.GetAllShop());
                        Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
                        Console.WriteLine("Item added to chart!");
                        break;

                    case 6:
                        Console.WriteLine("You choose to show inventory to shopping cart:");
                        printInventory(inventoryRepository.GetAllShop());
                        Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
                        break;

                    case 7:
                        Console.WriteLine("You choose delete item form shopping cart");
                        Console.WriteLine("Item list:");
                        printInventory(inventoryRepository.GetAllShop());
                        Console.WriteLine("Select item number to delete: ");
                        int deleteItem = int.Parse(Console.ReadLine());
                        inventoryRepository.DeleteShopItem(deleteItem);
                        break;

                    case 8:
                        Console.WriteLine("You choose buy item(s)");
                        Console.WriteLine("Item list:");
                        printInventory(inventoryRepository.GetAllShop());
                        Console.Write("Total price: ${0}\n", inventoryRepository.TotalCost());
                        Console.WriteLine("Thank you for buying from us! ");
                        inventoryRepository.BuyItem();
                        inventoryRepository.Save();
                        break;

                    case 9:
                        Console.WriteLine("You choose order items");
                        Console.WriteLine("We have two options:\nPress \"N\" to order by name\nPress \"P\" to order by price ");
                        char orderChoice = char.Parse(Console.ReadLine().Trim().ToLower());

                        if (orderChoice == 'n')
                        {
                            inventoryRepository.OrderByName();
                        }
                        else if(orderChoice == 'p')
                        {
                            inventoryRepository.OrderByPrice();
                        }
                        else
                        {
                            Console.WriteLine("Ups something wrong please try again");
                        }
                        break;

                    case 10:
                        Console.WriteLine("You choose find item");
                        Console.WriteLine("Please enter item name or first letter:");
                        string findWord = Console.ReadLine().ToLower();
                        inventoryRepository.FindPartByName(findWord);
                        break;

                    default:
                        break;
                }

                action = choooseAction();
            }
        }

        private static void printInventory(List<Inventory> z)
        {
            //foreach (Inventory item in z.InventoryList)
            //{
            //    Console.WriteLine("{0} | {1} | {2}", item.Id, item.Name, item.Price);
            //}

            for (int i = 0; i < z.Count; i++)
            {
                Console.WriteLine("#" + (i + 1) + " " + z[i]);
            }
        }

        public static int choooseAction()
        {
            int choice = 0;
            Console.WriteLine("1 - create item\n2 - show all inventory\n3 - delete" +
                            "\n4 - update item\n5 - add item to shopping cart" +
                            "\n6 - show all item in shopping cart\n7 - delete item form cart" +
                            "\n8 - buy\n9 - order item\n10 - find item by name\n0 - exit");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
    }
}
