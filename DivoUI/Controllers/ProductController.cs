using DivoUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace DivoUI.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new("https://localhost:7239/api");
        private readonly HttpClient _httpClient;
        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var productlist = new List<ProductViewModel>();

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Product");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                productlist = JsonSerializer.Deserialize<List<ProductViewModel>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            return View(productlist);
        }
    }
}
