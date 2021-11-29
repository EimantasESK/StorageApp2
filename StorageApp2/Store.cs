using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp2
{
    public class Store
    {
        public List<Inventory> InventoryList { get; set; }

        public Store()
        {
            InventoryList = new List<Inventory>(); 
        }
    }
}
