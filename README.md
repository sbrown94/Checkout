# Checkout #

Checkout is an API and Client Library that is used to manage a basket of items. It allows for adding, removing and updating items into a basket.

- - - -

# Use #

Run Checkout (either by setting it up on a server, or in Visual Studio), and then similarly run Checkout.ClientLibrary. This will allow all tests to pass.

- - - -

# Challenge Technical Requirements #

Build a shopping basket that allows for:

* Creating a basket
* Deleting a basket
* Adding items to a basket
* Removing items from a basket
* Change the quantity of existing items in basket
* Clearing a basket

- - - -

# Assumptions #

* Data can be stored in a cache with the assumption that it will be integrated into a database at a future date.
* Only one item can be requested at any one time, similarly only one type of item can be removed or modified in the basket in a single request.
* Items will have a unique ID (in this case, a Guid will be used) to differentiate them. The client will be made aware of these IDs prior to accessing the shopping basket API, so that it can send the IDs when it adds/removes items.

- - - -

# Design #

The program is designed to closely follow SOLID principles.


### The API ###

* IDataAccessRepo, an interface which abstracts the implementation of the logic so that it can be implemented in different ways, primarily so that the application could be extended so that data is saved to a database rather than stored locally in the cache.
* CacheDataAccessRepo, an implementation of IDataAccessRepo which has the following functionality
    * Create a Basket
    * Get an existing Basket
    * Delete a Basket
    * Add an Item to a Basket
    * Remove an Item from a Basket
    * Update an Item in an existing Basket (change the quantity of the Item)
* The Basket Controller, for receieving external requests to the API and calling the IDataAccessRepo implementation
* The Basket Model, which stores a List of Items
* The item Model, which includes ID, Item Name and Item Quantity members

### The Client Library ###

* The Basket and Item Models
* A Requests folder, containing classes for each of the functionalities of the API
* The main CheckoutClientLibrary class, which has public methods for calling each of the Client Library functions

### Tests ###

Tests exist for the API and Client Library. These include success and failure tests.

- - - -

# Future Extensions #

* The IDataAccessRepo can be implemented in a way that allows for storing and saving data to a database.
* Currently, AddToBasket fails if there is already an item in the basket. This could be modified so that the server automatically redirects AddToBasket to UpdateItemsInBasket if there is already an item with the same ID. This logic could be followed through to other areas, for example redirecting Remove to Update if the quantity specified is less than the total amount in the basket.
