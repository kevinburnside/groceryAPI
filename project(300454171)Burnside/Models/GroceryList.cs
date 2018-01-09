using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project300454171Burnside.Models
{
    public class GroceryItem
    {
    public int Id { get; set; }
    public int userId { get; set; }
    [Required]
    public string GroceryName { get; set; }
        [DefaultValue(1)]
    public int Quantity { get; set; }
    }
}
