using Checkout.Interfaces;
using Checkout.Models;
using Checkout.Repositories;
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

        public BasketController()
        {
            dataAccessRepo = new CacheDataAccessRepo();
        }

        // Create a new shopping basket
        [Route("api/v1/Basket/CreateBasket")]
        public IHttpActionResult CreateBasket()
        {
            var guid = dataAccessRepo.CreateBasket();
            return Ok(guid);
        }

        // Put add item
        [Route("api/v1/Basket/GetBasket/{id:Guid}")]
        public IHttpActionResult GetBasket(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid");
            }
            var result = dataAccessRepo.GetBasket(id);
            return Ok(result);
        }

        [Route("api/v1/Basket/AddItemToBasket/{id:Guid}")]
        public IHttpActionResult AddItemToBasket(Guid id, [FromBody]Item item)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid");
            }
            if (item == null)
            {
                return BadRequest("Item is not valid");
            }

            var result = dataAccessRepo.AddItemToBasket(id, item);

            return Ok(result);
        }

        [Route("api/v1/Basket/UpdateItemInBasket/{id:Guid}")]
        public IHttpActionResult UpdateItemInBasket(Guid id, [FromBody]Item item)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid");
            }
            if (item == null)
            {
                return BadRequest("Item is not valid");
            }

            var result = dataAccessRepo.UpdateItemInBasket(id, item);

            return Ok(result);
        }
    }
}
