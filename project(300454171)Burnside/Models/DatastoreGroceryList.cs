using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Datastore.V1;

namespace project300454171Burnside.Models
{
    public class DatastoreGroceryList
    {
        ///// <summary>
        ///// Make a datastore key given a GroceryList's id.
        ///// </summary>
        ///// <param name="id">A GroceryList's id.</param>
        ///// <returns>A datastore key.</returns>
        //public Key ToGroceryListKey(this long id) =>
        //    new Key().WithElement("GroceryList", id);

        ///// <summary>
        ///// Make a GroceryList id given a datastore key.
        ///// </summary>
        ///// <param name="key">A datastore key</param>
        ///// <returns>A GroceryList id.</returns>
        //public long ToId(this Key key) => key.Path.First().Id;

        ///// <summary>
        ///// Create a datastore entity with the same values as GroceryList.
        ///// </summary>
        ///// <param name="GroceryList">The GroceryList to store in datastore.</param>
        ///// <returns>A datastore entity.</returns>
        ///// [START toentity]
        //public Entity ToEntity(this GroceryList gl)
        //{
        //    return new Entity()
        //    {
        //        Key = GroceryList.u_id.ToGroceryListKey(),
        //        ["groceryItem"] = gl.GroceryName,
        //        ["UserId"] = gl.UserId,
        //        ["quantity"] = gl.Quantity,
        //        ["shareable"] = gl.Shareable
        //        //  []

        //    };
        //}

        //// [END toentity]

        ///// <summary>
        ///// Unpack a GroceryList from a datastore entity.
        ///// </summary>
        ///// <param name="entity">An entity retrieved from datastore.</param>
        ///// <returns>A GroceryList.</returns>
        //public GroceryList ToGroceryList(this Entity entity) => new GroceryList()
        //{
        //    GroceryListId = entity.Key.Path.First().Id,
        //    Name = (string)entity["name"],
        //    Address = (string)entity["address"],
        //    Cuisine = (string)entity["cuisine"],
        //    // u_id = (long)entity["u_id"]
        //};
    }
}
