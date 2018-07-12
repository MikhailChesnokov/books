namespace AppTest
{
    using System.Collections.Generic;
    using App.Controllers;
    using App.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;



    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; } = new[]
            {
                new Product {Name = "P1", Price = 275M},
                new Product {Name = "P2", Price = 48.95M},
                new Product {Name = "P3", Price = 19.50M},
                new Product {Name = "P3", Price = 34.95M}
            };

            public void AddProduct(Product p) { }
        }



        class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
        {
            public IEnumerable<Product> Products { get; set; } = new Product[]
            {
                new Product {Name = "P1", Price = 5M},
                new Product {Name = "P2", Price = 48.95M},
                new Product {Name = "P3", Price = 19.50M},
                new Product {Name = "P3", Price = 34.95M}
            };

            public void AddProduct(Product p) { }
        }



        [Fact]
        public void IndexActionModelIsCompleted()
        {
            var controller = new HomeController {Repository = new ModelCompleteFakeRepository()};

            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((x, y) => x.Name == y.Name && x.Price == y.Price));
        }

        [Fact]
        public void IndexActionModelIsCompletedPriceUnder50()
        {
            var controller = new HomeController {Repository = new ModelCompleteFakeRepositoryPricesUnder50()};

            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((x, y) => x.Name == y.Name && x.Price == y.Price));
        }




        [Theory]
        [InlineData(275, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, 24.95)]
        public void IndexActionModelIsComplete(decimal price1, decimal price2, decimal price3, decimal price4)
        {
            // Arrange
            var controller = new HomeController
            {
                Repository = new ModelCompleteFakeRepository
                {
                    Products = new[]
                    {
                        new Product {Name = "P1", Price = price1},
                        new Product {Name = "P2", Price = price2},
                        new Product {Name = "P3", Price = price3},
                        new Product {Name = "P4", Price = price4},
                    }
                }
            };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }




        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete2(Product[] products)
        {
            // Arrange
            var controller = new HomeController
            {
                Repository = new ModelCompleteFakeRepository
                {
                    Products = products
                }
            };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }





        [Theory]
        [MemberData(nameof(GetMemberDate))]
        public void IndexActionModelIsComplete3(Product[] products)
        {
            // Arrange
            var controller = new HomeController
            {
                Repository = new ModelCompleteFakeRepository
                {
                    Products = products
                }
            };

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        public static IEnumerable<object[]> GetMemberDate()
        {
            yield return new object[]
            {
                new object[]
                {
                    new Product {Name = "P1", Price = 5},
                    new Product {Name = "P2", Price = 48.95M},
                    new Product {Name = "P3", Price = 19.50M},
                    new Product {Name = "P4", Price = 24.95M}
                }
            };
        }



        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsCompleteUsingMoq(Product[] products)
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController {Repository = mock.Object};

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        [Fact]
        public void RepositoryPropertyCalledOnceUsingMoq()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(new[] {new Product {Name = "P1", Price = 100}});
            var controller = new HomeController {Repository = mock.Object};

            // Act
            var result = controller.Index();

            // Assert
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}