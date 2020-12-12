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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SysIO = System.IO;
using CleanArch.Common.Helper;

namespace CleanArch.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //ILogger<HomeController> logger

        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            //_logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1, string name="", double? price = null , DateTime? lastUpdate = null, int? pageSize = Constants.PageSize)
        {
            ProductListViewModel product = new ProductListViewModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(Constants.BaseUrl + "/search?name={0}&price={1}&lastUpdate={2}&start={3}&length={4}"
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
        public async Task<IActionResult> Edit(int Id)
        {
            var product = new ProductViewModel();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(Constants.BaseUrl + "/GetById?Id={0}", Id);
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var dto = (JsonConvert.DeserializeObject<ProductDto>(apiResponse));
                            product.Name = dto.Name;
                            product.Photo = dto.Photo;
                            product.Price = dto.Price;
                            product.LastUpdate = dto.LastUpdate;
                            product.Id = dto.Id;
                        }
                    }
                }
                return View(product);
            }
            catch(Exception ex)
            {
                return View(product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productDto = new ProductDto();
                    if (model.Id == 0)
                    {

                        if (Request.Form.Files?.Count > 0)
                        {
                            model.Photo = await SaveFile(Request.Form.Files["photo"]);
                        }

                        productDto.Name = model.Name;
                        productDto.Photo = model.Photo;
                        productDto.Price = model.Price;
                        productDto.LastUpdate = DateTime.Now;

                        using (var httpClient = new HttpClient())
                        {
                            var url = string.Format(Constants.BaseUrl + "/add");                           
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
                        if (Request.Form.Files?.Count > 0)
                        {
                            model.Photo = await SaveFile(Request.Form.Files["photo"] , true , model.Photo);
                        }

                        productDto.Name = model.Name;
                        productDto.Photo = model.Photo;
                        productDto.Price = model.Price;
                        productDto.LastUpdate = DateTime.Now;
                        productDto.Id = model.Id;

                        using (var httpClient = new HttpClient())
                        {
                            var url = string.Format(Constants.BaseUrl + "/update");
                            StringContent content = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PutAsync(url, content))
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
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
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


        private async Task<string> SaveFile(IFormFile file, bool deleteOld = false, string oldFileName = null)
        {
            if (file != null && file.Length > 0)
            {
                //Delete the old file (used by Edit action)
                if (deleteOld && !string.IsNullOrEmpty(oldFileName))
                {
                    DeleteFile(oldFileName);
                }

                //Save the uploaded file

                //  Read the file content
                var contentBytes = new byte[file.Length];

                file.OpenReadStream().Read(contentBytes, 0, contentBytes.Length);


                //  Determine the file path
                var newFileName = $"{Guid.NewGuid()}{SysIO.Path.GetExtension(file.FileName)}";

                var path = $"{_hostEnvironment.WebRootPath}{Constants.ProductUploadDirectory.Replace('/', '\\')}{newFileName}";

                //  Write the file to the disk
                await SysIO.File.WriteAllBytesAsync(path, contentBytes);

                return newFileName;
            }
            return oldFileName;
        }

        private void DeleteFile(string fileName)
        {
            try
            {
                SysIO.File.Delete($"{_hostEnvironment.WebRootPath}{Constants.ProductUploadDirectory.Replace('/', '\\')}{fileName}");
            }
            catch { }
        }
    }
}
