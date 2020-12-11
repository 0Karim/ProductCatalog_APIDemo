using CleanArch.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Services.Interfaces
{
    public interface IProductService
    {

        ICollection<Product> Search(string name, double? price , DateTime? lastUpdate);

        bool Add(Product product);

        bool Update(Product product);

        bool Delete(int Id);

        Product GetProductById(int id);
    }
}
