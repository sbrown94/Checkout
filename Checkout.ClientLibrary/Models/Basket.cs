﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkout.ClientLibrary.Models
{
    public class Basket
    {
        public List<Item> items { get; set; }

        public Basket(List<Item> its)
        {
            items = its;
        }

        public Basket()
        {

        }
    }
}