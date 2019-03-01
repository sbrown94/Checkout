using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ClientLibrary;

namespace Checkout.Tests
{
    class ClientLibraryTests
    {
        [TestClass]
        public class BasketTests
        {

            private readonly CheckoutClientLibrary ClientLibrary = new CheckoutClientLibrary();

            [TestMethod]
            public void TestCreateBasket()
            {
                var basketGuid = ClientLibrary.CreateBasket();
                Assert.AreNotEqual(basketGuid, Guid.Empty);
            }
        }
    }
}
