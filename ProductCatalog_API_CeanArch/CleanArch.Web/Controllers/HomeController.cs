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
            ProductViewModel product = new ProductViewModel();
            using (var httpClient = new HttpClient())
            {
                var x = string.Format("https://localhost:44331/api/product/search?name={0}&price={1}&lastUpdate={2}&start={3}&length={4}"
                    , name, price, lastUpdate, page, pageSize);
                using (var response = await httpClient.GetAsync(x))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product.Products = (JsonConvert.DeserializeObject<List<Product>>(apiResponse)).ToPagedList(page, pageSize.Value);
                    }
                }
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
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
