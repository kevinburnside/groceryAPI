using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project300454171Burnside.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project300454171Burnside.Controllers
{
    [Produces("application.json")]
    [Route("/groceries")]
public class GroceryListController : Controller
{
    private readonly Models.GroceryContext _context;
    public GroceryListController(GroceryContext context)
    {
        _context = context;

        if (_context.GroceryItems.Count() == 0)
        {
            _context.GroceryItems.Add(new GroceryItem { GroceryName = "Apples" });
            _context.SaveChanges();
        }
    }
    [HttpGet]
    public IEnumerable<GroceryItem> GetAll()
    {
        return _context.GroceryItems.ToList();
    }

    [HttpGet("{id}", Name = "GetGroceries")]
    public IActionResult GetById(int id)
    {
        var groceryItem = _context.GroceryItems.FirstOrDefault(i => i.Id == id);
        if (groceryItem == null)
        {
            return NotFound();
        }
        return new ObjectResult(groceryItem);
    }

    [HttpPost]
    public IActionResult Create([FromBody] GroceryItem item)
    {
        if (item == null)
        {
            return BadRequest();
        }
        _context.GroceryItems.Add(item);
        _context.SaveChanges();

        return CreatedAtRoute("GetGrocery", new { id = item.Id }, item);
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

