using CleanArch.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Services.Interfaces
{
    public interface IProductService
    {
        //int GetTotalCount(string name, double? price, DateTime? lastUpdate);
        
        ICollection<Product> Search(int skip, int take, string name, double? price , DateTime? lastUpdate);

        bool Add(Product product);

        bool Update(Product product);

        bool Delete(int Id);

        Product GetProductById(int id);
    }
}
