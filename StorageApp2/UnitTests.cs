using StorageApp2.repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace StorageApp2
{
    public class UnitTests
    {
        [Fact]
        public void CreateInventory()
        {
            //assrange
            InventoryRepository list = new InventoryRepository();
            //act
            list.Create("test1",99);
            //assert
            //var createItem = Assert.Single(list.GetAll());
            //Assert.NotNull(createItem);
            //Assert.Equal(List<Inventory> )

        }
    }
}
