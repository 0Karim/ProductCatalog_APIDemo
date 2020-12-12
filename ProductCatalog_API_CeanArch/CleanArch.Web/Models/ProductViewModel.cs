using CleanArch.Models.Entities;
using System;
using X.PagedList;

namespace CleanArch.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public DateTime LastUpdate { get; set; }

        //Product List
        public IPagedList<Product> Products { get; set; }
    }
}
