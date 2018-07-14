namespace SportsStore.Tests
{
    using System.Linq;
    using Models;
    using Xunit;



    public class CartTest
    {
        [Fact]
        public void CanAddNewLines()
        {
            var p1 = new Product{ProductID = 1, Name = "P1"};
            var p2 = new Product{ProductID = 2, Name = "P2"};

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var results = target.Lines.OrderBy(x => x.Product.ProductID).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };
            var p3 = new Product { ProductID = 3, Name = "P3" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.Equal(0, target.Lines.Count(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            var target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            
            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            
            var target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            
            target.Clear();
            
            Assert.Empty(target.Lines);
        }
    }
}