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

        /// <summary>
        /// Search action method that accept name as string , price as nullable double , lastUpdate as nullabel date , start as int and pageSize as int
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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



        /// <summary>
        /// GetById action method that accept Id as Integer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// add product action method that accept product as ProductDto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update product action method that accept product as ProductDto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
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

        /// <summary>
        /// delete product action method that accept Id as ProductDto
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
