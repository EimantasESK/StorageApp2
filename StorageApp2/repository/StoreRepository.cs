using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp2
{
    public class StoreRepository
    {
        public List<Inventory> InventoryList { get; set; }

        public StoreRepository()
        {
            InventoryList = new List<Inventory>();
        }
    }
}
