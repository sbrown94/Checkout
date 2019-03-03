using Checkout.ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Checkout.ClientLibrary.Requests
{
    public class GetBasket : BaseRequest
    {
        private Guid _id;
        private Item _item;

        public GetBasket(Guid id)
        {
            _id = id;
        }

        public override string requestUrl => string.Concat(base.baseUrl, "api/v1/GetBasket", _id);
        public override RequestType requestType => RequestType.Get;
        public override string jsonDataString => "";
    }
}