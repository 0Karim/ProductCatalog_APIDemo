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
using AutoMapper;
using System.IO;
using OfficeOpenXml;

namespace CleanArch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public HomeController(IWebHostEnvironment hostEnvironment , IMapper mapper)
        {
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
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

                            product = _mapper.Map<ProductViewModel>(dto);
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

                    #region Add 

                    if (model.Id == 0)
                    {

                        if (Request.Form.Files?.Count > 0)
                        {
                            model.Photo = await SaveFile(Request.Form.Files["photo"]);
                        }


                        productDto = _mapper.Map<ProductViewModel, ProductDto>(model);

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
                                    TempData[Constants.ErrorMessage] = Constants.ErrorMsg;
                                    return View(model);
                                }
                            }
                        }
                    }

                    #endregion

                    #region Edit

                    else
                    {
                        if (Request.Form.Files?.Count > 0)
                        {
                            model.Photo = await SaveFile(Request.Form.Files["photo"] , true , model.Photo);
                        }

                        productDto = _mapper.Map<ProductViewModel, ProductDto>(model);

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
                                    TempData[Constants.ErrorMessage] = Constants.ErrorMsg;
                                    return View(model);
                                }
                            }
                        }
                    }

                    #endregion

                    TempData[Constants.SuccessMessage] = Constants.SuccessMsg;
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                TempData[Constants.ErrorMessage] = Constants.ErrorMsg;
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(Constants.BaseUrl + "/delete?Id={0}", Id);
                    using (var response = await httpClient.DeleteAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            TempData[Constants.SuccessMessage] = Constants.SuccessMsg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData[Constants.ErrorMessage] = Constants.ErrorMsg;
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> ExportToExcel()
        {
            var products = new List<Product>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(Constants.BaseUrl + "/GetProductsExcel");
                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            products = (JsonConvert.DeserializeObject<List<Product>>(apiResponse));
                        }
                    }
                }

                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells.LoadFromCollection(products, true);
                    package.Save();
                }
                stream.Position = 0;
                string excelName = $"ProductList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/octet-stream", excelName);  
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch(Exception ex)
            {
                TempData[Constants.ErrorMessage] = Constants.ErrorMsg;
                return RedirectToAction("Index", "Home");
            }
        }

        #region Private Methods

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

        #endregion

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
