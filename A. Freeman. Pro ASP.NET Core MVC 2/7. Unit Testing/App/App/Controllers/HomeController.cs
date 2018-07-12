using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    using System.Linq;
    using Models;



    public class HomeController : Controller
    {
        public IRepository Repository = SimpleRepository.SharedRepository;

        public IActionResult Index()
        {
            return View(Repository.Products);
        }

        [HttpGet]
        public IActionResult AddProduct() => View(new Product());

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            Repository.AddProduct(product);

            return RedirectToAction("Index");
        }
    }
}