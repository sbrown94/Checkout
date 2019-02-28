using Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Checkout.Interfaces
{
    public interface IDataAccessRepo
    {
        /// <summary>
        /// Generates a new basket
        /// </summary>
        Guid CreateBasket();

        /// <summary>
        /// Get a basket by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Basket GetBasket(Guid id);

        /// <summary>
        /// Delete a basket by Guid
        /// </summary>
        /// <param name="id"></param>
        void DeleteBasket(Guid id);

        /// <summary>
        /// Add an item to a basket by guid and item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        Basket AddItemToBasket(Guid id, Item item);

        /// <summary>
        /// Remove an item from a basket by guid and item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        Basket RemoveItemFromBasket(Guid id, Item item);

        /// <summary>
        /// Update an item from a basket by guid, item and number of items
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <param name="count"></param>
        Basket UpdateItemInBasket(Guid id, Item item, int count);
    }
}