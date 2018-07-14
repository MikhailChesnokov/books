namespace SportsStore.Tests
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Moq;
    using Xunit;



    public class OrderControllerTests
    {
        [Fact]
        public void CanotCheckoutEmptyCart()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var controller = new OrderController(mock.Object, cart);

            var result = controller.Checkout(order) as ViewResult;

            mock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never());
            Assert.True(string.IsNullOrWhiteSpace(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotCheckoutInvalidShippingData()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            var result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrWhiteSpace(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var target = new OrderController(mock.Object, cart);

            var result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrWhiteSpace(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }
    }
}