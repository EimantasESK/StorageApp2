using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace StorageApp2
{
    public class StoreRepository
    {
        public List<Inventory> InventoryList { get; set; }
        public List<Inventory> ShoppingList { get; set; }
        
        public StoreRepository()
        {
            InventoryList = new List<Inventory>();
            ShoppingList = new List<Inventory>();
            Deserialize();
        }
        public void SaveInventoryList()
        {
            FileStream stream = new FileStream("InventoryList.xml", FileMode.Create);
            XmlSerializer x = new XmlSerializer(InventoryList.GetType());
            x.Serialize(stream, InventoryList);
            stream.Close();
        }
        public void SaveShoppingList()
        {
            string fullPath = @"C:\Users\Eimis\source\repos\StorageApp2\StorageApp2\bin\Debug\netcoreapp3.1\ShoppingList.xml";
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string path = Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;


            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}{1}", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
            }

            FileStream stream = new FileStream(newFullPath, FileMode.Create);
            XmlSerializer ser = new XmlSerializer(ShoppingList.GetType());
            ser.Serialize(stream, ShoppingList);
            stream.Close();
        }
        public void Deserialize()
        {
            FileStream stream = new FileStream("InventoryList.xml", FileMode.Open);
            XmlSerializer des = new XmlSerializer(typeof(List<Inventory>));
            InventoryList = (List<Inventory>)des.Deserialize(stream);
            stream.Close();
        }
    }
}
