namespace SportsStore.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.ViewModels;



    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;


        public ProductController(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int PageCount { get; set; } = 4;



        public ViewResult List(string category, int page = 1)
        {
            return View(
                new ProductListViewModel
                {
                    Products =
                        _productRepository
                            .Products
                            .Where(x => category == null || x.Category == category)
                            .OrderBy(x => x.ProductID)
                            .Skip((page - 1) * PageCount)
                            .Take(PageCount),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageCount,
                        TotalItems = category is null
                            ? _productRepository.Products.Count()
                            : _productRepository.Products.Count(x => x.Category == category)
                    },
                    CurrentCategory = category
                });
        }
    }
}