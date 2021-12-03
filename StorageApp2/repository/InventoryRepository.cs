using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp2.repository
{
    public class InventoryRepository
    {
        StoreRepository s = new StoreRepository();

        public void create(string name, decimal price)
        {
            Guid itID = Guid.NewGuid();
            Inventory newItem = new Inventory(itID, name, price);
            s.InventoryList.Add(newItem);

        }
        public List<Inventory> getAll()
        {
            return s.InventoryList;
        }

        public void delete(int index)
        {
            s.InventoryList.Remove(s.InventoryList[index - 1]);
        }

        public void update(int index, string name, decimal price)
        {
            Inventory oldItem = s.InventoryList[index - 1];
            oldItem.Name = name;
            oldItem.Price = price;
            s.InventoryList[index -1] = oldItem;
        }


    }

   
}
