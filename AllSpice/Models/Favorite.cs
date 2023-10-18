using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Models
{
    public class Favorite
    {
        public int id { get; set; }
        public string accountId { get; set; }
        public int recipeId { get; set; }
    }
}