using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout;
using Checkout.Interfaces;
using Checkout.Repositories;

namespace Checkout.Tests
{
    [TestClass]
    public class BasketTests
    {
        private readonly IDataAccessRepo dataAccessRepo;

        [TestMethod]
        public void TestCreateBasket()
        {

            var dataAccessRepo = new CacheDataAccessRepo();

            var guid = dataAccessRepo.CreateBasket();
            Assert.AreNotEqual(guid, Guid.Empty);
        }
    }
}
