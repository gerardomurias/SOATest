using System;
using System.Collections.Generic;
using System.Linq;
using SOATest.Contracts;

namespace SOATest.Domain
{
    public class Product : IProduct
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Allocation { get; set; }

        public List<IProductReservation> ReservedProducts
        {
            get { return InternalReservedProducts.Cast<IProductReservation>().ToList(); }
            set { InternalReservedProducts = value.Cast<ProductReservation>().ToList(); }
        }

        public List<IProductPurchase> PurchasedProducts
        {
            get { return InternalPurchasedProducts.Cast<IProductPurchase>().ToList(); }
            set { InternalPurchasedProducts = value.Cast<ProductPurchase>().ToList(); }
        }

        private List<ProductReservation> InternalReservedProducts { get; set; }

        private List<ProductPurchase> InternalPurchasedProducts { get; set; }

        public Product()
        {
            InternalReservedProducts = new List<ProductReservation>();
            InternalPurchasedProducts = new List<ProductPurchase>();
        }

        public int Available()
        {
            var soldAndReserved = 0;
            PurchasedProducts.ForEach(p => soldAndReserved += p.ProductQuantity);

            return Allocation - soldAndReserved;
        }

        public bool ReservationIsValid(string reservationId)
        {
            if (HasReservation(reservationId))
            {
                return GetReservationWith(reservationId).IsActive();
            }

            return false;
        }

        public IProductPurchase ConfirmPurchaseWith(string reservationId)
        {
            if (!ReservationIsValid(reservationId))
            {
                throw new Exception($"Cannot confirm the purchase with this Id: {reservationId}");
            }

            var reservation = GetReservationWith(reservationId);
            var purchase = new ProductPurchase(reservationId, this, reservation.Quantity);
            reservation.HasBeenConfirmed = true;
            PurchasedProducts.Add(purchase);

            return purchase;
        }

        public IProductReservation GetReservationWith(string reservationId)
        {
            if (!HasReservation(reservationId))
            {
                throw new Exception(string.Concat("No reservation found with id {0}", reservationId.ToString()));
            }
            return (from r in ReservedProducts where r.Id == reservationId select r).FirstOrDefault();
        }

        public bool HasReservation(string reservationId)
        {
            return ReservedProducts.Exists(p => p.Id == reservationId);
        }

        public bool CanReserveProduct(int quantity)
        {
            return Available() >= quantity;
        }

        public IProductReservation Reserve(string id, int quantity)
        {
            if (!CanReserveProduct(quantity))
            {
                throw new Exception("Cannot reserve so many tickets");
            }

            ProductReservation reservation = new ProductReservation(id, this, 1, quantity);
            ReservedProducts.Add(reservation);

            return reservation;
        }
    }
}