using System;
using SOATest.Contracts;
using SOATest.Service.Responses;

namespace SOATest.Service
{
    public class ProductReservationResponse : ServiceResponseBase, IProductReservationResponse
    {
        public string ReservationId { get; set; }

        public DateTime Expiration { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductQuantity { get; set; }
    }
}