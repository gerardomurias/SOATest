using SOATest.Contracts;

namespace SOATest.Service
{
    public class ReserveProductRequest : IReserveProductRequest
    {
        public string ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}