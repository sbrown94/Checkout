using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Checkout.ClientLibrary.Requests
{
    public enum RequestType { Post, Get, Put }
    public abstract class BaseRequest
    {
        public abstract string jsonDataString { get; }
        public abstract string requestUrl { get; }
        public string Request()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Request failed");
                }
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        public HttpResponseMessage SendRequest(HttpClient client, RequestType reqType)
        {
            switch(reqType)
            {
                case RequestType.Post:
                    return client.PostAsync(requestUrl, new StringContent(jsonDataString, Encoding.UTF8, "application/json")).Result;
                case RequestType.Get:
                    return client.GetAsync(requestUrl).Result;
                case RequestType.Put:
                    return client.PutAsync(requestUrl, new StringContent(jsonDataString, Encoding.UTF8, "application/json")).Result;
            }

            return new HttpResponseMessage();
        }
    }
}