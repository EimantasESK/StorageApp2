using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StorageApp2.repository
{
    public class InventoryRepository
    {
        StoreRepository s = new StoreRepository();

        public void Create(string name, decimal price)
        {
            //Guid itID = Guid.NewGuid();
            //Inventory newItem = new Inventory(itID, name, price);
            //s.InventoryList.Add(newItem);
            Guid itID = Guid.NewGuid();
            s.InventoryList.Add(new Inventory() {Id = itID, Name = name, Price = price});
           
        }
        public List<Inventory> GetAll()
        {
            return s.InventoryList;
        }

        public List<Inventory> GetAllShop()
        {
            return s.ShopingList;
        }

        public void Delete(int index)
        {
            s.InventoryList.Remove(s.InventoryList[index - 1]);
        }

        public void Update(int index, string name, decimal price)
        {
            Inventory oldItem = s.InventoryList[index - 1];
            oldItem.Name = name;
            oldItem.Price = price;
            s.InventoryList[index - 1] = oldItem;
        }

        public void Save()
        {
            FileStream stream = new FileStream("Inventory.xml", FileMode.Create);
            //new XmlSerializer(typeof(StoreRepository)).Serialize(stream, s);
            XmlSerializer x = new XmlSerializer(s.GetType());
            x.Serialize(stream, s);
            stream.Close();
        }

        public void Deserialize()
        {
            FileStream stream = new FileStream("Inventory.xml", FileMode.Open);
            XmlSerializer des = new XmlSerializer(typeof(StoreRepository));
            using (XmlReader reader = XmlReader.Create(stream))
            {
                s = (StoreRepository)des.Deserialize(reader);
            }
            stream.Close();
        }
        public void ShopingCart(int index)
        {
            s.ShopingList.Add(s.InventoryList[index - 1]);
        }

        public decimal TotalCost()
        {
            decimal totalCost = 0;

            foreach (var c in s.ShopingList)
            {
                totalCost += c.Price;
            }

            //s.ShopingList.Clear();

            return totalCost;
        }
        public void DeleteShopItem(int index)
        {
            s.ShopingList.Remove(s.ShopingList[index - 1]);
        }

        public void BuyItem()
        {
            s.InventoryList = s.InventoryList.Except(s.ShopingList).ToList();
            s.ShopingList.Clear();
        }
    }

   
}
