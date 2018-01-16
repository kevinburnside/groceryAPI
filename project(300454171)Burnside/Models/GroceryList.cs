using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project300454171Burnside.Models
{
    public class GroceryList
    {
    public string GroceryListId { get; set; }
    public string GroceryItemId { get; set; }
    public string UserId { get; set; }
    [Required]
    public string GroceryName { get; set; }
        [DefaultValue(1)]
    public string Quantity { get; set; }

    public bool Shareable { get; set; }
    }
}
