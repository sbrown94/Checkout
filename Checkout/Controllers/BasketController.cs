﻿using Checkout.Interfaces;
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

        [Route("api/v1/Basket/CreateBasket")]
        [HttpPost]
        public IHttpActionResult CreateBasket()
        {
            var guid = dataAccessRepo.CreateBasket();
            return Ok(guid);
        }

        [Route("api/v1/Basket/DeleteBasket/{id:Guid}")]
        [HttpPut]
        public IHttpActionResult DeleteBasket(Guid id)
        {
            var guid = dataAccessRepo.DeleteBasket(id);
            return Ok(guid);
        }

        [Route("api/v1/Basket/GetBasket/{id:Guid}")]
        [HttpGet]
        public IHttpActionResult GetBasket(Guid id)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid.");
            }
            var result = dataAccessRepo.GetBasket(id);
            return Ok(result);
        }

        [Route("api/v1/Basket/AddItemToBasket/{id:Guid}")]
        [HttpPut]
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
            if (item.ItemId == Guid.Empty || item.ItemId == null)
            {
                return BadRequest("Item ID is not valid");
            }

            var result = dataAccessRepo.AddItemToBasket(id, item);

            return Ok(result);
        }

        [Route("api/v1/Basket/RemoveItemFromBasket/{id:Guid}")]
        [HttpPut]
        public IHttpActionResult RemoveItemFromBasket(Guid id, [FromBody]Item item)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid");
            }
            if (item == null)
            {
                return BadRequest("Item is not valid");
            }
            if(item.ItemId == Guid.Empty || item.ItemId == null)
            {
                return BadRequest("Item ID is not valid");
            }

            Basket result = new Basket();
            try
            {
                result = dataAccessRepo.RemoveItemFromBasket(id, item);
            }
            catch (Exception e)
            {
                HttpResponseMessage responseData = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseData.Content = new StringContent(e.Message);
                return (ResponseMessage(responseData));
            }

            return Ok(result);
        }

        [Route("api/v1/Basket/UpdateItemInBasket/{id:Guid}")]
        [HttpPut]
        public IHttpActionResult UpdateItemInBasket(Guid id, [FromBody]Item item)
        {
            if (id == Guid.Empty || id == null)
            {
                return BadRequest("Basket ID is not valid.");
            }
            if (item == null)
            {
                return BadRequest("Item is not valid.");
            }
            if (item.ItemId == Guid.Empty || item.ItemId == null)
            {
                return BadRequest("Item ID is not valid.");
            }

            Basket result = new Basket();
            try
            {
                result = dataAccessRepo.UpdateItemInBasket(id, item);
            }
            catch (Exception e)
            {
                HttpResponseMessage responseData = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseData.Content = new StringContent(e.Message);
                return (ResponseMessage(responseData));
            }

            return Ok(result);
        }
    }
}
