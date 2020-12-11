using System;
using System.ComponentModel.DataAnnotations;


namespace CleanArch.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
