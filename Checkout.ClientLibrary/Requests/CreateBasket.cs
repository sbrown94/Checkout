using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Checkout.ClientLibrary.Requests
{
    public class CreateBasket : BaseRequest
    {
        public CreateBasket()
        {

        }

        public override string requestUrl { get => string.Concat(base.baseUrl, "api/v1"); }
        public override RequestType requestType { get => RequestType.Get; }
    }
}