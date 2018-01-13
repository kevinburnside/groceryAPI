using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project300454171Burnside.Models
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions<GroceryContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryList> GroceryItems { get; set; }
        
    }
}
