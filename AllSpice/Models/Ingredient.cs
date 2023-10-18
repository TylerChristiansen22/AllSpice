using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Models
{
    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public int recipeId { get; set; }
    }
}