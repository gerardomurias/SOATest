using System;
using SOATest.Contracts;

namespace SOATest.Domain
{
    public class ProductPurchase : IProductPurchase
    {
        public int ProductQuantity { get; set; }
        public string Id { get; set; }
        public IProduct Product { get; set; }

        public ProductPurchase(string id, IProduct product, int quantity)
        {
            Id = id;
            if (product == null) throw new Exception("Product cannot be null");
            if (quantity < 1) throw new Exception("The quantity should be at least 1");

            Product = product;
            ProductQuantity = quantity;
        }
    }
}