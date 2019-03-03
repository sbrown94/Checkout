using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ClientLibrary;
using Checkout.ClientLibrary.Models;
using System.Net;

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
            public void TestGetBasket()
            {
                var basketId = ClientLibrary.CreateBasket();
                var result = ClientLibrary.GetBasket(basketId);
                Assert.IsNotNull(result);
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
            public void TestRemoveFromBasketFail()
            {
                try
                {
                    var item = new Item(Guid.NewGuid(), "Apple", 1);
                    var basketId = ClientLibrary.CreateBasket();

                    var result = ClientLibrary.RemoveFromBasket(basketId, item);
                    Assert.Fail("Exception not thrown");
                }
                catch (Exception e)
                {
                    Assert.IsTrue(e.Message.Contains("You are trying to remove item 'Apple' from a basket which does not contain this item."));
                }
            }

            [TestMethod]
            public void TestUpdateFail()
            {
                try
                {
                    var item = new Item(Guid.NewGuid(), "Apple", 1);
                    var basketId = ClientLibrary.CreateBasket();

                    var result = ClientLibrary.UpdateItemInBasket(basketId, item);
                    Assert.Fail("Exception not thrown");
                }
                catch (Exception e)
                {
                    Assert.IsTrue(e.Message.Contains("You are trying to modify the value of 'Apple' which does not exist in this basket."));

                }
            }

            [TestMethod]
            public void TestGetBasketFail()
            {
                try
                {
                    var guid = Guid.NewGuid();
                    var result = ClientLibrary.GetBasket(guid);
                    Assert.Fail("Exception not thrown");
                }
                catch (Exception e)
                {
                    Assert.IsTrue(e.Message.Contains("Basket ID is invalid. Basket does not exist."));
                }

            }

            [TestMethod]
            public void TestInvalidBasket()
            {
                try
                {
                    var guid = Guid.Empty;
                    var result = ClientLibrary.GetBasket(guid);
                    Assert.Fail("Exception not thrown");
                }
                catch (Exception e)
                {
                    Assert.IsTrue(e.Message.Contains("Basket ID is not valid."));
                }

            }

            [TestMethod]
            public void TestInvalidItems()
            {
                try
                {
                    var item = new Item(Guid.Empty, "Apple", 1);
                    var basketId = ClientLibrary.CreateBasket();

                    var result = ClientLibrary.AddToBasket(basketId, item);
                    Assert.Fail("Exception not thrown");
                }
                catch (Exception e)
                {
                    Assert.IsTrue(e.Message.Contains("Item ID is not valid"));
                }
            }
        }
    }
}
