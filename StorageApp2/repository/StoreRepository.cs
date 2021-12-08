using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace StorageApp2
{
    public class StoreRepository
    {
        public List<Inventory> InventoryList { get; set; }
        public List<Inventory> ShopingList { get; set; }

        public StoreRepository()
        {
            InventoryList = new List<Inventory>();
            ShopingList = new List<Inventory>();
        }
    }
}
