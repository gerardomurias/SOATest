using System;

namespace SOATest.Contracts
{
    public interface IProductReservationResponse
    {
        string ReservationId { get; set; }

        DateTime Expiration { get; set; }

        string ProductId { get; set; }

        string ProductName { get; set; }

        int ProductQuantity { get; set; }
    }
}