using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WebPixabayAPI.Models;

namespace WebPixabayAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //public async Task<IActionResult> Search(string SearchString)
        //{
        //    var listobj = new List<dynamic>();
        //    //pageNumber phải từ 1
        //    int pageNumber = 1;
        //    var Key = _config["PixabayApiKey"];
        //    string tempProduct;
        //    while (pageNumber <= 5)
        //    {
        //        tempProduct = "";
        //        string UrlGetApi = $"https://pixabay.com/api/?key={Key}&q={SearchString}&image_type=photo&page={pageNumber}&per_page=100&pretty=true";
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage response = await client.GetAsync(UrlGetApi);
        //        //response.Content.Headers.ContentType.CharSet = @"utf-8";
        //        try
        //        {
        //            tempProduct = await response.Content.ReadAsStringAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            tempProduct = ex.Message;
        //        }
        //        //kết nối 2 string Json
        //        //phải bỏ các dấu ngoặc của 2 Json mới kết nối được
                
        //        listobj.Add(JsonConvert.DeserializeObject<dynamic>(tempProduct));
        //        ++pageNumber;
        //    }

        //    //product = await response.Content.ReadAsStringAsync();
        //    //var jsonApi = JsonConvert.DeserializeObject<dynamic>(product);
            
        //    return View(listobj);
        //}

        public async Task<IActionResult> Search(string SearchString, string? SearchType)
        {
            SearchType = (SearchType == "photo") ? null : "videos/";
            ViewData["searchtype"] = SearchType;
            List<dynamic> listobj = new List<dynamic>();
            //pageNumber phải từ 1
            int pageNumber = 1;
            var Key = _config["PixabayApiKey"];
            string tempProduct;
            while (pageNumber <= 5)
            {
                tempProduct = "";
                string UrlGetApi = $"https://pixabay.com/api/{SearchType}?key={Key}&q={SearchString}&page={pageNumber}&per_page=100&pretty=true";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(UrlGetApi);
                //response.Content.Headers.ContentType.CharSet = @"utf-8";
                try
                {
                    tempProduct = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    tempProduct = ex.Message;
                }
                //kết nối 2 string Json
                //phải bỏ các dấu ngoặc của 2 Json mới kết nối được

                listobj.Add(JsonConvert.DeserializeObject<dynamic>(tempProduct));
                ++pageNumber;
            }

            //product = await response.Content.ReadAsStringAsync();
            //var jsonApi = JsonConvert.DeserializeObject<dynamic>(product);

            return View(listobj);
        }

        //public async Task<IActionResult> Search(string SearchString,string? SearchType)
        //{
        //    string check = SearchType;
        //    SearchType = (SearchType == "photo") ? null : "videos/";
        //    List<object> listobj = new List<object>();
        //    //pageNumber phải từ 1
        //    int pageNumber = 1;
        //    var Key = _config["PixabayApiKey"];
        //    string tempProduct = "";
        //    while (pageNumber <= 1)
        //    {
        //        tempProduct = "";
        //        string UrlGetApi = $"https://pixabay.com/api/{SearchType}?key={Key}&q={SearchString}&page={pageNumber}&per_page=100&pretty=true";
        //        HttpClient client = new HttpClient();
        //        HttpResponseMessage response = await client.GetAsync(UrlGetApi);
        //        //response.Content.Headers.ContentType.CharSet = @"utf-8";
        //        try
        //        {
        //            tempProduct = await response.Content.ReadAsStringAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            tempProduct = ex.Message;
        //        }
        //        //kết nối 2 string Json
        //        //phải bỏ các dấu ngoặc của 2 Json mới kết nối được

        //        listobj.Add(JsonConvert.DeserializeObject<dynamic>(tempProduct));
        //        ++pageNumber;
        //    }

        //    //product = await response.Content.ReadAsStringAsync();
        //    var jsonApi = JsonConvert.DeserializeObject<dynamic>(tempProduct);

        //    return View(jsonApi);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
