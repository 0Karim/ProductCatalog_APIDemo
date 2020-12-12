using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArc.API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
