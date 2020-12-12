using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArc.API.Models;
using CleanArch.Common.Dtos;
using CleanArch.Models.Entities;
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
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("search")]
        public ActionResult<ICollection<ProductModel>> Get([FromQuery] string name , [FromQuery] double? price , [FromQuery] DateTime? lastUpdate , [FromQuery] int start , [FromQuery] int length)
        {
            try
            {
                var productList = _productService.Search(start, length, name, price, lastUpdate);
                return Ok(productList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }


        [HttpGet("GetById")]
        public ActionResult<ICollection<ProductModel>> Get([FromQuery] int Id)
        {
            try
            {
                if(Id == 0)
                {
                    return NotFound();
                }

                var product = _productService.GetProductById(Id);
                var productDto = new ProductDto();

                productDto = _mapper.Map<Product , ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }


        [HttpPost("add")]
        public IActionResult Post(ProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var entity = new Product();
                entity = _mapper.Map<ProductDto , Product>(product);

                var success = _productService.Add(entity);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }


        [HttpPut("update")]
        public IActionResult Put(ProductDto product)
        {
            try
            {
                if (product == null || product.Id == 0)
                    return BadRequest();

                var entity = _productService.GetProductById(product.Id);

                entity.Name = product.Name;
                entity.Price = product.Price;
                entity.Photo = product.Photo;
                entity.LastUpdate = product.LastUpdate;

                var success = _productService.Update(entity);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int Id)
        {
            try
            {
                if (Id == 0)
                    return BadRequest();

                var entity = _productService.GetProductById(Id);

                if (entity == null)
                    return NotFound();

                var success = _productService.Delete(Id);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Database Failure");
            }
        }

    }
}
