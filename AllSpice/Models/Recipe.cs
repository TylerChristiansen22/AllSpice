using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string img { get; set; }
        public string Category { get; set; }
        public Account creator { get; set; }
        public string creatorId { get; set; }
    }
}