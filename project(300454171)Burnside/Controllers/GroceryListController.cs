using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project300454171Burnside.Models;
using Google.Cloud.Datastore.V1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project300454171Burnside.Controllers
{
    [Route("/groceries")]
public class GroceryListController : Controller
{
        private DatastoreDb _db;
        private KeyFactory _keyFactory;
        private string projectId;
        private GroceryList gl = new GroceryList();


    public GroceryListController()
    {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\arlin\\Desktop\\what-am-i-calling-this-project-e1c237c4aa27.json");
            projectId = "what-am-i-calling-this-project";
            _db = DatastoreDb.Create(projectId);
            _keyFactory = _db.CreateKeyFactory("GroceryList");
            
        }
    [HttpGet]
    public IActionResult GetAll()
    {
         Query query = new Query("GroceryList");
            //return _db.RunQuery(query).ToString();
            List<GroceryList> GroceryLists = new List<GroceryList>();
            
           foreach (var entity in _db.RunQuery(query).Entities)
            {
                //string item = "";
                //item = "GroceryId" + (string)entity["id"] + ",\nUserId: " + (string)entity["GroceryName"] + ",\nQuantity: " + (string)entity["Quantity"] + ",\nSharable: " + (string)entity["Sharable"];
                GroceryLists.Add(new GroceryList() { GroceryListId = (string)entity["id"], UserId = (string)entity["UserId"], GroceryName = (string)entity["GroceryName"], Quantity = (string)entity["Quantity"], Shareable = (bool)entity["Sharable"] });
            }
            JsonResult jsonItem = new JsonResult(GroceryLists);
           return jsonItem;
        }

    [HttpGet("{id}", Name = "GetGroceries")]
    public IActionResult GetById(int id)
    {
            Query query = new Query("GroceryList")
            {
                Filter = Filter.Equal("id", id)
            };
            JsonResult jsonItem = new JsonResult(_db.RunQuery(query));
            return jsonItem;
        }
        // creates a new grocery list
        [HttpPost("/newList")]
        public void CreateNewList(String userId, [FromBody] GroceryList item)
        {
            Key key = _keyFactory.CreateKey(new Random().Next());
            Entity groceryList = new Entity()
            {
                Key = _keyFactory.CreateIncompleteKey(),
                ["GroceryListId"] = gl.GroceryListId,
                ["groceryItem"] = gl.GroceryName,
                ["UserId"] = gl.UserId,
                ["quantity"] = gl.Quantity,
                ["shareable"] = gl.Shareable
            };
           this. _db.Insert(groceryList);

            //TODO figure out how to return the full grocery list
            //GetById(Key);
        }
        // adds an item to a selected grocery list
        [HttpPost("{id}/item")]
        public IActionResult AddItemToList(String userId, [FromBody] GroceryList item)
        {
            Key key = _keyFactory.CreateKey(new Random().Next());
            Entity groceryList = new Entity()
            {
                Key = _keyFactory.CreateIncompleteKey(),
                ["GroceryListId"] = gl.GroceryListId,
                ["groceryItem"] = gl.GroceryName,
                ["UserId"] = gl.UserId,
                ["quantity"] = gl.Quantity,
                ["shareable"] = gl.Shareable
            };
            this._db.Insert(groceryList);

            // TODO figure out how to return the grocery list, or a success message
            JsonResult jsonItem = new JsonResult(this._db.Insert(groceryList));
            return jsonItem;
        }
        // Deletes the selected grocery item
        [HttpDelete("/{groceryListId}/{groceryName}")]
        public void deleteGroceryItem(string groceryId, string groceryName)
        {
            _db.Delete(_keyFactory.CreateKey(groceryId + groceryName));
        }
            

    //    [HttpPut("{id}")]
    //public IActionResult Update(int id, [FromBody] GroceryList item)
    //{
    //    if (item == null || item.Id != id)
    //    {
    //        return BadRequest();
    //    }
    //    var groceryItem = _context.GroceryItems.FirstOrDefault(g => g.Id == id);
    //    if(groceryItem == null)
    //    {
    //        return NotFound();
    //    }

    //    groceryItem.GroceryName = item.GroceryName;
    //    groceryItem.Quantity = item.Quantity;

    //    _context.GroceryItems.Update(groceryItem);
    //    _context.SaveChanges();
    //    return new NoContentResult();
    //}
}
}

