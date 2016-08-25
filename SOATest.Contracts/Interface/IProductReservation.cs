using System;

namespace SOATest.Contracts
{
    public interface IProductReservation
    {
        string Id { get; set; }

        IProduct Product { get; set; }

        DateTime Expiry { get; set; }

        int Quantity { get; set; }

        bool HasBeenConfirmed { get; set; }

        bool Expired();

        bool IsActive();
    }
}