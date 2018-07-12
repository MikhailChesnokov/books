namespace App.Models
{
    using System.Collections.Generic;



    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        void AddProduct(Product p);
    }
}