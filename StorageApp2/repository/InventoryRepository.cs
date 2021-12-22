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
        StoreRepository storeRep = new StoreRepository();

        public void Create(string name, decimal price)
        {
            Inventory newItem = new Inventory(name, price);
            storeRep.InventoryList.Add(newItem);

            //storeRep.InventoryList.Add(new Inventory() { Name = name, Price = price });
        }

        public List<Inventory> GetAll()
        {
            return storeRep.InventoryList;
        }
        public List<Inventory> GetAllShop()
        {
            return storeRep.ShopingList;
        }
        public void Delete(Guid id)
        {
            storeRep.InventoryList.Remove(storeRep.InventoryList.SingleOrDefault(i => i.Id == id));
        }
        public void Update(Guid id, string name, decimal price)
        {
            Inventory oldItem = storeRep.InventoryList.SingleOrDefault(i => i.Id == id);
            oldItem.Name = name;
            oldItem.Price = price;
        }
        public void Save()
        {
            FileStream stream = new FileStream("Inventory.xml", FileMode.Create);
            //new XmlSerializer(typeof(StoreRepository)).Serialize(stream, s);
            XmlSerializer x = new XmlSerializer(storeRep.GetType());
            x.Serialize(stream, storeRep);
            stream.Close();
        }
        public void Deserialize()
        {
            FileStream stream = new FileStream("Inventory.xml", FileMode.Open);
            XmlSerializer des = new XmlSerializer(typeof(StoreRepository));
            using (XmlReader reader = XmlReader.Create(stream))
            {
                storeRep = (StoreRepository)des.Deserialize(reader);
            }
            stream.Close();
        }
        public void ShopingCart(int index)
        {
            storeRep.ShopingList.Add(storeRep.InventoryList[index - 1]);
        }
        public decimal TotalCost()
        {
            decimal totalCost = 0;

            foreach (var c in storeRep.ShopingList)
            {
                totalCost += c.Price;
            }

            return totalCost;
        }
        public void DeleteShopItem(int index)
        {
            storeRep.ShopingList.Remove(storeRep.ShopingList[index - 1]);
        }
        public void BuyItem()
        {
            storeRep.InventoryList = storeRep.InventoryList.Except(storeRep.ShopingList).ToList();
            storeRep.ShopingList.Clear();
        }
        public void OrderByName()
        {
            List<Inventory> orderName = storeRep.InventoryList.OrderBy(n => n.Name).ToList();
            orderName.ForEach(n => Console.WriteLine(n));
        }
        public void OrderByPrice()
        {
            List<Inventory> orderByPrice = storeRep.InventoryList.OrderBy(p => p.Price).ToList();
            orderByPrice.ForEach(p => Console.WriteLine(p));
        }
        public void FindPartByName(string word)
        {
            var findName = storeRep.InventoryList.FindAll(x => x.Name.StartsWith(word)).ToList();
            findName.ForEach(f => Console.WriteLine(f));
        }
    }
}
