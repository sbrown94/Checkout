using Checkout.Interfaces;
using Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Web.Http;
using System.Net.Http;

namespace Checkout.Repositories
{
    public class CacheDataAccessRepo : IDataAccessRepo
    {

        private MemoryCache _cache = MemoryCache.Default;

        // Create a new basket and add it to the cache
        // Baskets expire after half an hour
        public Guid CreateBasket()
        {
            var basket = new Basket();
            Guid guid = Guid.NewGuid();
            // expire basket after half an hour
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = System.TimeSpan.FromMinutes(30);
            _cache.Add(guid.ToString(), basket, policy);
            return guid;
        }

        public Basket GetBasket(Guid id)
        {
            return GetBasketWithItems(id);
        }

        public Guid DeleteBasket(Guid id)
        {
            _cache.Remove(id.ToString());
            return id;
        }

        public Basket AddItemToBasket(Guid id, Item item)
        {
            var basket = GetBasketWithItems(id);
            var exists = basket.items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (exists == null)
            {
                basket.items.Add(item);
            }
            else
            {
                throw new ApplicationException("You are trying to add item '" + item.ItemName + "' to a basket which already contains this item.");
            }
            return basket;
        }

        public Basket RemoveItemFromBasket(Guid id, Item item)
        {
            var basket = GetBasketWithItems(id);
            var exists = basket.items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (exists != null)
            {
                basket.items.Remove(exists);
            }
            else
            {
                throw new ApplicationException("You are trying to remove item '" + item.ItemName + "' from a basket which does not contain this item.");
            }
            return basket;
        }

        public Basket UpdateItemInBasket(Guid id, Item item)
        {
            var basket = GetBasketWithItems(id);
            var inBasketItem = basket.items.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (inBasketItem == null)
            {
                throw new ApplicationException("You are trying to modify the value of '" + item.ItemName + "' which does not exist in this basket.");
            }
            basket.items.Remove(inBasketItem);
            item.ItemQuantity += inBasketItem.ItemQuantity;
            if(item.ItemQuantity > 0)
            {
                basket.items.Add(item);
            }
            return basket;
        }

        private Basket GetBasketWithItems(Guid id)
        {
            var basket = new Basket();
            basket = (Basket)_cache.Get(id.ToString());
            if(basket == null)
            {
                throw new ApplicationException("Basket ID is invalid. Basket does not exist.");
            }
            if(basket.items == null)
            {
                basket.items = new List<Item>();
            }
            return basket;
        }
    }
}