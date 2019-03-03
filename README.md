# Checkout

Checkout is an API and Client Library that is used to manage a basket of items. It allows for adding, removing and updating items into a basket.

---

# Use

Run Checkout (either by setting it up on a server, or in Visual Studio), and then similarly run Checkout.ClientLibrary. This will allow all tests to pass.

---

# Challenge Technical Requirements

Build a shopping basket that allows for:

- Creating a basket
- Deleting a basket
- Adding items to a basket
- Removing items from a basket
- Change the quantity of existing items in basket
- Clearing a basket

---

# Assumptions

- Data can be stored in a cache with the assumption that it will be integrated into a database
- Only one item can be requested at any one time, similarly only one type of item can be removed or modified in the basket in a single request
- Items will have a unique ID (in this case, a Guid will be used) to differentiate them. The client will be made aware of these IDs prior to accessing the shopping basket API, so that it can send the IDs when it adds/removes items.

