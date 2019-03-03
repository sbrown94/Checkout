using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ClientLibrary;
using Checkout.ClientLibrary.Models;

namespace Checkout.Tests
{
    class ClientLibraryTests
    {
        [TestInitialize]
        public void Initialize()
        {


        }

        [TestClass]
        public class BasketTests
        {

            private readonly CheckoutClientLibrary ClientLibrary = new CheckoutClientLibrary();

            /* Success Tests */

            [TestMethod]
            public void TestCreateBasket()
            {
                var basketId = ClientLibrary.CreateBasket();
                Assert.AreNotEqual(basketId, Guid.Empty);
            }

            [TestMethod]
            public void TestAddToBasket()
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = ClientLibrary.CreateBasket();
                var result = ClientLibrary.AddToBasket(basketId, item);
                Assert.AreEqual(result.items[0].ItemName, item.ItemName);
                Assert.AreEqual(result.items[0].ItemQuantity, item.ItemQuantity);
                Assert.AreEqual(result.items[0].ItemId, item.ItemId);
            }

            [TestMethod]
            public void TestUpdateItemInBasket()
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = ClientLibrary.CreateBasket();
                var setup = ClientLibrary.AddToBasket(basketId, item);

                var oldQuantity = item.ItemQuantity;
                item.ItemQuantity = 3;

                var result = ClientLibrary.UpdateItemInBasket(basketId, item);
                Assert.AreEqual(result.items[0].ItemQuantity, item.ItemQuantity + oldQuantity);
            }

            [TestMethod]
            public void TestRemoveFromBasket()
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = ClientLibrary.CreateBasket();
                var setup = ClientLibrary.AddToBasket(basketId, item);

                var result = ClientLibrary.RemoveFromBasket(basketId, item);
                Assert.AreEqual(result.items.Count, 0);
            }

            /* Fail Tests */

            [TestMethod]
            [ExpectedException(typeof(ApplicationException),
                "You are trying to remove item 'Apple' from a basket which does not contain this item.")]
            public void TestRemoveFromBasketFail()
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = ClientLibrary.CreateBasket();

                var result = ClientLibrary.RemoveFromBasket(basketId, item);
            }

            [TestMethod]
            [ExpectedException(typeof(ApplicationException),
                "You are trying to modify the value of 'Apple' which does not exist in this basket.")]
            public void TestUpdateFail()
            {
                var item = new Item(Guid.NewGuid(), "Apple", 1);
                var basketId = ClientLibrary.CreateBasket();

                var result = ClientLibrary.UpdateItemInBasket(basketId, item);
            }

            [TestMethod]
            [ExpectedException(typeof(ApplicationException),
                "Basket ID is invalid. Basket does not exist.")]
            public void TestGetBasketFail()
            {
                var guid = Guid.NewGuid();
                var result = ClientLibrary.GetBasket(guid);
            }

        }
    }
}
