using CleanArch.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace CleanArch.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Required Field"]
        public string Name { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Required Field"]
        public double Price { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
