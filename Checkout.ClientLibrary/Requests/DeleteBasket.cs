using Checkout.ClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Checkout.ClientLibrary.Requests
{
    public class DeleteBasket : BaseRequest
    {
        private Guid _id;
        private Item _item;

        public DeleteBasket(Guid id)
        {
            _id = id;
        }

        public override string requestUrl => string.Concat(base.baseUrl, "api/v1", _id);
        public override RequestType requestType => RequestType.Put;
        public override string jsonDataString => "";
    }
}