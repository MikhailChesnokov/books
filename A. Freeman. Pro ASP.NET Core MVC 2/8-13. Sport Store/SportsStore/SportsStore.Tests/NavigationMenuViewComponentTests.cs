namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Components;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Routing;
    using Models;
    using Moq;
    using Xunit;



    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void CanSelectCategories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            }.AsQueryable());

            var target = new NavigationMenuViewComponent(mock.Object);

            var result = ((target.Invoke() as ViewViewComponentResult).ViewData.Model as IEnumerable<string>).ToArray();

            Assert.True(new [] { "Apples" , "Plums", "Oranges" }.SequenceEqual(result));
        }

        [Fact]
        public void IndicatesSelectedCategory()
        {
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
            }.AsQueryable());
            var target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            var result = (target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"] as string;

            Assert.Equal(categoryToSelect, result);
        }
    }
}