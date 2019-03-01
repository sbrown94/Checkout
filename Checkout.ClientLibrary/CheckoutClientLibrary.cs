using Checkout.ClientLibrary.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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
    }
}