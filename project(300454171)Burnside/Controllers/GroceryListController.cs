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
    [Produces("application.json")]
    [Route("/groceries")]
public class GroceryListController : Controller
{
        private DatastoreDb _db;
        private KeyFactory _keyFactory;
        private string projectId;
        private GroceryList gl = new GroceryList();


    public GroceryListController(GroceryContext context)
    {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "E:\\Downloads\\My Project-812dcc062ed5.json");
            projectId = "cellular-datum-186719";
            _db = DatastoreDb.Create(projectId);
            _keyFactory = _db.CreateKeyFactory("GroceryList");
            
        }
    [HttpGet]
    public IEnumerable<GroceryItem> GetAll()
    {
        
    }

    [HttpGet("{id}", Name = "GetGroceries")]
    public IActionResult GetById(int id)
    {

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
        }
        // adds an item to a selected grocery list
        [HttpPost("{id}/item")]
        public void AddItemToList(String userId, [FromBody] GroceryList item)
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
        }
        // Deletes the selected grocery item
        [HttpDelete("/{groceryListId}/{groceryName}")]
        public string deleteGroceryItem(string groceryId, string groceryName)
        {
            _db.Delete(keyFactory.CreateKey(groceryId && groceryName));
        }
            

        [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] GroceryItem item)
    {
        if (item == null || item.Id != id)
        {
            return BadRequest();
        }
        var groceryItem = _context.GroceryItems.FirstOrDefault(g => g.Id == id);
        if(groceryItem == null)
        {
            return NotFound();
        }

        groceryItem.GroceryName = item.GroceryName;
        groceryItem.Quantity = item.Quantity;

        _context.GroceryItems.Update(groceryItem);
        _context.SaveChanges();
        return new NoContentResult();
    }
}
}

