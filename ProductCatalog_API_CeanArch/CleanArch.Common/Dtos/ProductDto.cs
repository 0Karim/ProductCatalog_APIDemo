using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Common.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
