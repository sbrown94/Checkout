using Checkout.ClientLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Checkout.ClientLibrary.Models;

namespace Checkout.ClientLibrary
{
    public class CheckoutClientLibrary
    {
        public Guid CreateBasket()
        {
            // TODO CHECK ERROR
            var request = new CreateBasket();
            var response = JsonConvert.DeserializeObject<string>(request.MakeRequest());
            return Guid.Parse(response);
        }

        public Basket GetBasket(Guid id)
        {
            var request = new GetBasket(id);
            var response = JsonConvert.DeserializeObject<Basket>(request.MakeRequest());
            return response;
        }

        public Guid DeleteBasket(Guid id)
        {
            var request = new DeleteBasket(id);
            var response = JsonConvert.DeserializeObject<string>(request.MakeRequest());
            return Guid.Parse(response);
        }

        public Basket AddToBasket(Guid id, Item item)
        {
            var request = new AddToBasket(id, item);
            var response = JsonConvert.DeserializeObject<Basket>(request.MakeRequest());
            return response;
        }

        public Basket RemoveFromBasket(Guid id, Item item)
        {
            var request = new RemoveFromBasket(id, item);
            var response = JsonConvert.DeserializeObject<Basket>(request.MakeRequest());
            return response;
        }

        public Basket UpdateItemInBasket(Guid id, Item item)
        {
            var request = new UpdateItemInBasket(id, item);
            var response = JsonConvert.DeserializeObject<Basket>(request.MakeRequest());
            return response;
        }

    }
}