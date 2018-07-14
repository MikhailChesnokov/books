namespace SportsStore.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;



    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;



        public OrderController(IOrderRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }



        [HttpGet]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!_cart.Lines.Any())
            {
                ModelState.AddModelError(string.Empty, "Sorry, your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}