using CleanArch.Common.Helper;
using CleanArch.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CleanArch.Web.Models
{
    public class ProductListViewModel
    {
        //Product List
        public IPagedList<Product> Products { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string Photo { get; set; }

        public string RootPath { get; set; } = Constants.ProductUploadDirectory.Replace('/', '\\');

        public DateTime? LastUpdate { get; set; }
    }
}
