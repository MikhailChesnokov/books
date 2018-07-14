namespace SportsStore.Tests
{
    using System;
    using System.Linq;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.ViewModels;
    using Moq;
    using Xunit;



    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable);
            var controller = new ProductController(mock.Object) {PageCount = 3};

            var result = controller.List(null, 2)?.ViewData.Model as ProductListViewModel;

            Product[] products = result?.Products.ToArray();
            Assert.True(products?.Length is 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable);

            var controller = new ProductController(mock.Object) {PageCount = 3};

            var viewModel = controller.List(null, 2).ViewData.Model as ProductListViewModel;

            var pageInfo = viewModel?.PagingInfo;

            Assert.Equal(2, pageInfo?.CurrentPage);
            Assert.Equal(3, pageInfo?.ItemsPerPage);
            Assert.Equal(5, pageInfo?.TotalItems);
            Assert.Equal(2, pageInfo?.TotalPages);
        }

        [Fact]
        public void CanFilterProducts()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
            }.AsQueryable);

            var controller = new ProductController(mock.Object)
            {
                PageCount = 3
            };

            var result = (controller.List("Cat2").Model as ProductListViewModel)?.Products.ToArray();

            Assert.Equal(2, result?.Length);
            Assert.True(result?[0].Name is "P2" && result[0].Category is "Cat2");
            Assert.True(result[1].Name is "P4" && result[1].Category is "Cat2");
        }

        [Fact]
        public void GenerateCategorySpecificProductCount()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }.AsQueryable());
            var target = new ProductController(mock.Object) {PageCount = 3};
            Func<ViewResult, ProductListViewModel> GetModel = result => result?.ViewData?.Model as ProductListViewModel;


            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;


            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}