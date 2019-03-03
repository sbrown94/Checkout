using Checkout.ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Checkout.ClientLibrary.Requests
{
    public class RemoveFromBasket : BaseRequest
    {
        private Guid _id;
        private Item _item;

        public RemoveFromBasket(Guid id, Item item)
        {
            _id = id;
            _item = item;
        }

        public override string requestUrl => string.Concat(base.baseUrl, "api/v1/Basket/RemoveItemFromBasket/", _id);
        public override RequestType requestType => RequestType.Put;
        public override string jsonDataString => JsonConvert.SerializeObject(_item);
    }
}