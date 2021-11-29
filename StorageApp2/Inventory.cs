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

        public Inventory(Guid inId, string inName, decimal inPrice)
        {
            Id = inId;
            Name = inName;
            Price = inPrice;
        }

        
    }

}
