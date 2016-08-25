namespace SOATest.Contracts
{
    public interface IPurchaseProductRequest
    {
        string CorrelationId { get; set; }

        string ReservationId { get; set; }

        string ProductId { get; set; }
    }
}