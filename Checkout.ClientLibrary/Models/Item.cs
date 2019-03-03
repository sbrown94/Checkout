using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkout.ClientLibrary.Models
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }

        public Item(Guid id, string name, int quantity)
        {
            ItemId = id;
            ItemName = name;
            ItemQuantity = quantity;
        }
    }
}