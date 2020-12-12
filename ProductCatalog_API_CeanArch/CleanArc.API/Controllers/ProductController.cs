using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArc.API.Models;
using CleanArc.API.Models.Shared;
using CleanArch.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("search")]
        public ActionResult<ICollection<ProductModel>> Get([FromQuery] string name , [FromQuery] double? price , [FromQuery] DateTime? lastUpdate , [FromQuery] int start , [FromQuery] int length)
        {
            try
            {
                //var productList = _productService.Search(dtParameters.Start, dtParameters.Length, name, price, lastUpdate);
                var productList = _productService.Search(start, length, name, price, lastUpdate);
                return Ok(productList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }

    }
}
