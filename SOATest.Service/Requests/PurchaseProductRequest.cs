namespace SOATest.Service
{
    public class PurchaseProductRequest
    {
        public string CorrelationId { get; set; }

        public string ReservationId { get; set; }

        public string ProductId { get; set; }
    }
}
