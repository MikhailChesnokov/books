﻿namespace SportsStore.Models
{
    using System.Linq;



    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}