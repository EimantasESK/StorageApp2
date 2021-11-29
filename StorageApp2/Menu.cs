using System;
using System.Collections.Generic;
using System.Text;

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
                }

                action = choooseAction();
            }
        }
        private static void printInventory(Store s)
        {
            foreach (Inventory item in s.InventoryList)
            {
                Console.WriteLine("{0} | {1} | {2}", item.Id, item.Name, item.Price);
            }
        }

        public static int choooseAction()
        {
            int choice = 0;
            Console.WriteLine("1 - create item\n2 - show all inventory\n0 - exit");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
    }
}
