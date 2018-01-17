﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project300454171Burnside.Models;
using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Http;
using System.Transactions;

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

            List<GroceryList> GroceryLists = new List<GroceryList>();

            foreach (var entity in _db.RunQuery(query).Entities)
            {
                GroceryLists.Add(new GroceryList() { GroceryListId = entity.Key.Path[0].Id.ToString(), UserId = (string)entity["UserId"], GroceryName = (string)entity["GroceryName"], Quantity = (string)entity["Quantity"], Shareable = (bool)entity["Shareable"] });
            }
            JsonResult jsonItem = new JsonResult(GroceryLists);
            return jsonItem;
        }

        [HttpGet("{id}", Name = "GetGroceries")]
        public IActionResult GetById(long id)
        {

            Entity entity = _db.Lookup(_keyFactory.CreateKey(id));
            GroceryList item = new GroceryList() { GroceryListId = entity.Key.Path[0].Id.ToString(), UserId = (string)entity["UserId"], GroceryName = (string)entity["GroceryName"], Quantity = (string)entity["Quantity"], Shareable = (bool)entity["Shareable"] };

            JsonResult jsonItem = new JsonResult(item);
            return jsonItem;
        }
        // creates a new grocery list
        [HttpPost("/newList")]
        public ActionResult CreateNewList([FromBody] GroceryList gl)
        {
            Key key = _keyFactory.CreateKey(new Random().Next());
            Entity groceryList = new Entity()
            {
                Key = _keyFactory.CreateIncompleteKey(),
                //["GroceryListId"] = gl.GroceryListId,
                ["GroceryName"] = gl.GroceryName,
                ["UserId"] = gl.UserId,
                ["Quantity"] = gl.Quantity,
                ["Shareable"] = gl.Shareable
            };
            this._db.Insert(groceryList);

            //TODO figure out how to return the full grocery list
            JsonResult jsonResult = new JsonResult(_db.Lookup(groceryList.Key));
            return jsonResult;
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
            //this._db.Insert(groceryList);

            // TODO figure out how to return the grocery list, or a success message
            JsonResult jsonItem = new JsonResult(this._db.Insert(groceryList));
            return jsonItem;
        }
        // Deletes the selected grocery item
        [HttpDelete("{id}/delete")]
        public JsonResult deleteGroceryItem(long id)
        {
            _db.Delete(_keyFactory.CreateKey(id));
            return new JsonResult("deleted");

        }
        
        [HttpPut("{id}/update")]
        public void UpdateGroceryItem(long id, [FromBody] GroceryList gl)
        {
             using (var transaction = _db.BeginTransaction())
             {
                Entity groceryItem = transaction.Lookup(_keyFactory.CreateKey(id));
                 if (groceryItem != null)
                 {
                     groceryItem["GroceryName"] = gl.GroceryName;
                     groceryItem["Quantity"] = gl.Quantity;
                     groceryItem["Shareable"] = gl.Shareable;
                     transaction.Update(groceryItem);
                 };
                transaction.Commit();
              }   //end "using"

        }
    }
}


