using Checkout.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Checkout.Controllers
{
    public class BasketController : ApiController
    {

        private readonly IDataAccessRepo dataAccessRepo;

        public string GetHello()
        {
            return "hello";
        }

        public BasketController()
        {
            dataAccessRepo = new CacheDataAccessRepo();
        }

        // Create a new shopping basket
        public IHttpActionResult CreateBasket()
        {
            var guid = dataAccessRepo.CreateBasket();
            return Ok(guid);
        }

        // Put add item
        [Route("api/v1/")]
        public IHttpActionResult AddItemToBasket()
        {

            return Ok()
        }
    }
}
