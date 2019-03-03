using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout;
using Checkout.Interfaces;
using Checkout.Repositories;
using Checkout.Controllers;
using System.Web.Http;

namespace Checkout.Tests
{
    [TestClass]
    public class BasketTests
    {
        public static IDataAccessRepo dataAccessRepo;

        [TestInitialize]
        public void Initialize()
        {
            // Get the data access repo
            var dataAccessRepo = new CacheDataAccessRepo();
        }

        [TestMethod]
        public void TestCreateBasket()
        {
            var guid = dataAccessRepo.CreateBasket();
            Assert.AreNotEqual(guid, Guid.Empty);
        }

        //[TestMethod]
        //public void TestGetBasket()
        //{
        //    IHttpActionResult basket = basketController.GetBasket()
        //}
    }
}
