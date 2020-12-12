using CleanArch.Models.Entities;
using CleanArch.Repositories.Repository;
using CleanArch.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArch.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;
        private readonly ILogger logger;

        public ProductService(IRepository _repository, ILogger _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        public bool Add(Product product)
        {
            try
            {
                repository.Add<Product>(product);
                var success = repository.SaveChangesAsync();
                return success;

            }catch(Exception ex)
            {
                logger.LogError(DateTime.Now.ToString("dd/MM/YYYY") + "    " + ex.Message + "    " + ex.StackTrace + "    "  + ex.InnerException);
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var product = this.GetProductById(Id);
                if (product == null)
                    return false;

                repository.Remove<Product>(product);
                var success = repository.SaveChangesAsync();

                return success;
            }
            catch(Exception ex)
            {
                logger.LogError(DateTime.Now.ToString("dd/MM/YYYY") + "    " + ex.Message + "    " + ex.StackTrace + "    " + ex.InnerException);
                return false;
            }
        }

        public Product GetProductById(int id)
        {
            return repository.GetAllWhereQ<Product>(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Product> Search(int skip, int take ,string name, double? price = null, DateTime? lastUpdate = null)
        {
            try
            {
                var nameToLower = name?.ToLower();

                var productsList = repository.GetAllWhereQ<Product>
                    (p => (string.IsNullOrEmpty(nameToLower) || p.Name.ToLower().Contains(nameToLower)) &&
                          (price == null || p.Price == price) &&
                          (lastUpdate == null || (lastUpdate.Value >= p.LastUpdate))
                    ).ToList();

                return productsList;
            }
            catch(Exception ex)
            {
                logger.LogError(DateTime.Now.ToString("dd/MM/YYYY") + "    " +ex.Message + "    " + ex.StackTrace + "    " + ex.InnerException);
                return new List<Product>();
            }
        }

        public bool Update(Product product)
        {
            try
            {

                repository.Update<Product>(product);
                var success = repository.SaveChangesAsync();
                return success;
            }
            catch(Exception ex)
            {
                logger.LogError(DateTime.Now.ToString("dd/MM/YYYY") + "    " + ex.Message + "    " + ex.StackTrace + "    " + ex.InnerException);
                return false;
            }

        }
    }
}
