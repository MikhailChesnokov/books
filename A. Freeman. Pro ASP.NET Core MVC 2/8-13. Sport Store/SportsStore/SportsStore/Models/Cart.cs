namespace SportsStore.Models
{
    using System.Collections.Generic;
    using System.Linq;



    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            if (_lineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID) is CartLine line)
            {
                line.Quantity += quantity;
            }
            else
            {
                _lineCollection.Add(new CartLine{Product = product, Quantity = quantity});
            }
        }

        public virtual void RemoveLine(Product product) => _lineCollection.RemoveAll(x => x.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() => _lineCollection.Sum(x => x.Product.Price * x.Quantity);

        public virtual void Clear() => _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;
    }
}