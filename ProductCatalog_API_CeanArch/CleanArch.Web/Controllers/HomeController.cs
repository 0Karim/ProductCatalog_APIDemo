using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CleanArch.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;
using CleanArch.Models.Entities;
using X.PagedList;
using CleanArch.Common.Dtos;
using System.Text;

namespace CleanArch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1, string name="", double? price = null , DateTime? lastUpdate = null, int? pageSize = 10)
        {
            ProductListViewModel product = new ProductListViewModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format("https://localhost:44331/api/product/search?name={0}&price={1}&lastUpdate={2}&start={3}&length={4}"
                        , name, price, lastUpdate, page, pageSize);
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            product.Products = (JsonConvert.DeserializeObject<List<Product>>(apiResponse)).ToPagedList(page, pageSize.Value);
                        }
                    }
                }
                return View(product);
            }catch(Exception ex)
            {
                return View(product);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var product = new ProductViewModel();
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var url = string.Format("https://localhost:44331/api/product/add");
                            
                            var productDto = new ProductDto();
                            productDto.Name = model.Name;
                            productDto.Photo = model.Photo;
                            productDto.Price = model.Price;
                            productDto.LastUpdate = DateTime.Now;

                            StringContent content = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");

                            using (var response = await httpClient.PostAsync(url , content))
                            {

                                if (response.IsSuccessStatusCode)
                                {
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                }
                                else
                                {
                                    return View(model);
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
