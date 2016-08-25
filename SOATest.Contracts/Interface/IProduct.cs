using System;
using System.Collections.Generic;

namespace SOATest.Contracts
{
    public interface IProduct
    {
        string Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        int Allocation { get; set; }

        List<IProductReservation> ReservedProducts { get; set; }

        List<IProductPurchase> PurchasedProducts { get; set; }

        int Available();

        bool ReservationIsValid(string reservationId);

        IProductPurchase ConfirmPurchaseWith(string reservationId);

        IProductReservation GetReservationWith(string reservationId);

        bool HasReservation(string reservationId);

        bool CanReserveProduct(int quantity);

        IProductReservation Reserve(string id, int quantity);
    }
}
