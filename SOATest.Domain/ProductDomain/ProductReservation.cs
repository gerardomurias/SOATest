using System;
using SOATest.Contracts;

namespace SOATest.Domain
{
    public class ProductReservation : IProductReservation
    {
        public string Id { get; set; }
        
        public IProduct Product { get; set; }

        public DateTime Expiry { get; set; }

        public int Quantity { get; set; }

        public bool HasBeenConfirmed { get; set; }

        public bool Expired()
        {
            return DateTime.Now > Expiry;
        }

        public bool IsActive()
        {
            return !HasBeenConfirmed && !Expired();
        }

        public ProductReservation()
        {
            
        }

        public ProductReservation(string id, Product product, int expiryInMinutes, int quantity)
        {
            if (product == null) throw new ArgumentException("Product cannot be null");
            if (quantity < 1) throw new ArgumentException("The quantity should be at least 1");

            Product = product;
            Id = id;
            Expiry = DateTime.Now.AddMinutes(expiryInMinutes);
            Quantity = quantity;
        }
    }
}