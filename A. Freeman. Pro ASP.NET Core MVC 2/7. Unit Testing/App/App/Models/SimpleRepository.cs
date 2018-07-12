namespace App.Models
{
    using System.Collections.Generic;
    using System.Linq;



    public class SimpleRepository : IRepository
    {
        private static readonly SimpleRepository _sharedRepository = new SimpleRepository();
        private readonly Dictionary<string, Product> _products = new Dictionary<string, Product>();

        public static SimpleRepository SharedRepository = _sharedRepository;

        public SimpleRepository()
        {
            new[]
            {
                new Product {Name = "Kayak", Price = 245M},
                new Product {Name = "Lifejacked", Price = 48.95M},
                new Product {Name = "Soocer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            }.ToList().ForEach(AddProduct);
        }

        public IEnumerable<Product> Products => _products.Values;

        public void AddProduct(Product p) => _products.Add(p.Name, p);
    }
}