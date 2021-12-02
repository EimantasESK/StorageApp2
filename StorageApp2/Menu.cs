﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace StorageApp2
{
    class Menu
    {
        public void Run()
        {
            Store s = new Store();

            Console.WriteLine("Store actions:");

            int action = choooseAction();

            while (action != 0)
            {
                Console.WriteLine("You choose: {0}", action);

                switch (action)
                {
                    case 1:
                        Console.WriteLine("You choose create item");

                        Guid itID = Guid.NewGuid();
                        string itName = "";
                        decimal itPrice = 0;

                        Console.Write("Item name: ");
                        itName = Console.ReadLine();

                        Console.Write("Item price: ");
                        itPrice = decimal.Parse(Console.ReadLine());

                        Inventory newItem = new Inventory(itID, itName, itPrice);
                        s.InventoryList.Add(newItem);
                        break;

                    case 2:
                        printInventory(s);
                        break;

                    case 3:
                        Console.WriteLine("You choose delete item");
                        Console.WriteLine("Item list:");
                        printInventory(s);
                        Console.WriteLine("Select item number to delete: ");
                        int deleteNum = int.Parse(Console.ReadLine());
                        s.InventoryList.Remove(s.InventoryList[deleteNum - 1]);
                        break;

                    case 4:
                        Console.WriteLine("You choose update item");
                        Console.WriteLine("Item list:");
                        printInventory(s);
                        Console.WriteLine("Select item number to tu update: ");
                        int updateNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("Old inventory information:");
                        Console.WriteLine(s.InventoryList[updateNum-1]);
                        s.InventoryList.Remove(s.InventoryList[updateNum - 1]);
                        Console.WriteLine("Add updated item Name: ");
                        string updateName = Console.ReadLine();
                        Console.WriteLine("Add updated item Price");
                        decimal updatePrice = decimal.Parse(Console.ReadLine());
                        Guid updateID = Guid.NewGuid();
                        s.InventoryList.Insert(updateNum-1, new Inventory(updateID,updateName,updatePrice));
                        break;
                }

                action = choooseAction();
            }
        }
        
        private static void printInventory(Store z)
        {
            /*
            foreach (Inventory item in z.InventoryList)
            {
                Console.WriteLine("{0} | {1} | {2}", item.Id, item.Name, item.Price);
            }
            */
            
            for (int i = 0; i < z.InventoryList.Count; i++)
            {
                Console.WriteLine("#" + (i +1) + " " + z.InventoryList[i]);
            }
        }

        public static int choooseAction()
        {
            int choice = 0;
            Console.WriteLine("1 - create item\n2 - show all inventory\n3 - delete\n4 - update item\n0 - exit");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
    }
}
