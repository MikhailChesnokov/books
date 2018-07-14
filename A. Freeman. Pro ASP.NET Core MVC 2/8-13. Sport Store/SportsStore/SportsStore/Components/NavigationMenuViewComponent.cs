namespace SportsStore.Components
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;



    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;



        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }



        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(
                _repository
                    .Products
                    .GroupBy(x => x.Category)
                    .Select(x => x.Key));
        }
    }
}