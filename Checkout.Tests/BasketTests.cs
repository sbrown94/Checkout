using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout;
using Checkout.Interfaces;
using Checkout.Repositories;
using Checkout.Controllers;
using System.Web.Http;
using System.Runtime.Caching;
using System.Collections.Generic;
using Checkout.Models;
using System.Linq;

namespace Checkout.Tests
{
    [TestClass]
    public class BasketTests
    {
        public static IDataAccessRepo dataAccessRepo;
        public List<Guid> basketGuids;
        public Dictionary<Guid, Basket> basketDict;
        public List<Item> items;
        public static int noBaskets = 3;

        [TestInitialize]
        public void Initialize()
        {
            // Get the data access repo
            dataAccessRepo = new CacheDataAccessRepo();

            basketDict = new Dictionary<Guid, Basket>();
            basketGuids = new List<Guid>();

            // generate some fake items
            items = new List<Item>();
            string[] randomItemNames = new string[] { "Apple", "Orange", "Pear" };
            for(var i = 0; i < noBaskets; i++)
            {
                var item = new Item(Guid.NewGuid(), randomItemNames[i], i);
                items.Add(item);
            }

            // generate a fake basket
            var basket = new Basket(items);

            // generate a memory cache
            var mockCache = MemoryCache.Default;
            // expire basket after half an hour
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = System.TimeSpan.FromMinutes(30);
            // generate some guids and add them to the cache with a basket
            for(var i = 0; i < noBaskets; i++)
            {
                var testGuid = Guid.NewGuid();
                basketDict.Add(testGuid, basket);
                mockCache.Add(testGuid.ToString(), basket, policy);
                basketGuids.Add(testGuid);
            }
        }

        /* Success Tests */

        [TestMethod]
        public void TestCreateBasket()
        {
            var mockCache = MemoryCache.Default;
            var guid = dataAccessRepo.CreateBasket();
            Assert.IsNotNull(mockCache[guid.ToString()]);
        }

        [TestMethod]
        public void TestDeleteBasket()
        {
            var mockCache = MemoryCache.Default;
            var guid = dataAccessRepo.DeleteBasket(basketGuids[0]);
            Assert.IsNull(mockCache[basketGuids[0].ToString()]);
        }

        [TestMethod]
        public void TestGetBasket()
        {
            foreach(var guid in basketGuids)
            {
                var basket = dataAccessRepo.GetBasket(guid);
                Assert.IsNotNull(basket);
            }
        }

        [TestMethod]
        public void TestAddItemToBasket()
        {
            for(var i = 0; i < basketDict.Count; i++)
            {
                var addItem = new Item(Guid.NewGuid(), "Mango", 1);
                var basket = dataAccessRepo.AddItemToBasket(basketGuids[i], addItem);
                Assert.IsNotNull(basket.items.FirstOrDefault(x => x.ItemId == addItem.ItemId));
            }
        }

        [TestMethod]
        public void TestRemoveItemFromBasket()
        {
            for (var i = 0; i < items.Count; i++)
            {
                var removeItem = items[i];
                var basket = dataAccessRepo.RemoveItemFromBasket(basketGuids[0], removeItem);
                Assert.IsNull(basket.items.FirstOrDefault(x => x.ItemId == removeItem.ItemId));
            }
        }

        [TestMethod]
        public void TestUpdateItemInBasket()
        {
            var updateItem = items[0];
            updateItem.ItemQuantity = 5;
            var basket = dataAccessRepo.UpdateItemInBasket(basketGuids[0], updateItem);
            Assert.AreEqual(basket.items.FirstOrDefault(x => x.ItemId == updateItem.ItemId).ItemQuantity, updateItem.ItemQuantity);
        }

        /* Fail Tests */

        [TestMethod]
        public void TestUpdateFail()
        {
            try
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = dataAccessRepo.CreateBasket();
                var result = dataAccessRepo.UpdateItemInBasket(basketId, item);
                Assert.Fail("Exception not thrown");
            }
            catch(Exception e)
            {
                Assert.AreEqual("You are trying to modify the value of 'Apple' which does not exist in this basket.", e.Message);
            }
        }

        [TestMethod]
        public void TestRemoveFromBasketFail()
        {
            try
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = dataAccessRepo.CreateBasket();
                var result = dataAccessRepo.RemoveItemFromBasket(basketId, item);
                Assert.Fail("Exception not thrown");
            }
            catch(Exception e)
            {
                Assert.AreEqual("You are trying to remove item 'Apple' from a basket which does not contain this item.", e.Message);
            }
        }

        [TestMethod]
        public void TestGetBasketFail()
        {
            try
            {
                var guid = Guid.NewGuid();
                var result = dataAccessRepo.GetBasket(guid);
                Assert.Fail("Exception not thrown");
            }
            catch(Exception e)
            {
                Assert.AreEqual("Basket ID is invalid. Basket does not exist.", e.Message);
            }
        }

        [TestMethod]
        public void TestAddItemToBasketFail()
        {
            try
            {
                var addItem = items[0];
                var basket = dataAccessRepo.AddItemToBasket(basketGuids[0], addItem);
                basket = dataAccessRepo.AddItemToBasket(basketGuids[0], addItem);
                Assert.Fail("Exception not thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual("You are trying to add item 'Apple' to a basket which already contains this item.", e.Message);
            }
        }
    }
}
