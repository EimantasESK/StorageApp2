using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp2
{
    public class Inventory
    {
        public Guid Id = Guid.NewGuid();
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Inventory()
        {
        }

        public Inventory(string inName, decimal inPrice)
        {
            Name = inName;
            Price = inPrice;
        }

        override public string ToString()
        {
            return "|" + Id + "|" + Name + "|$" + Price;
            //return ($"|{Id}|{Name}|{Price}\u20AC");
        }
    }
}
