using SOATest.Contracts;
using SOATest.Service.Responses;

namespace SOATest.Service
{
    public class PurchaseProductResponse : ServiceResponseBase, IPurchaseProductResponse
    {
        public string PurchaseId { get; set; }

        public string ProductName { get; set; }

        public string ProductId { get; set; }

        public int ProductQuantity { get; set; }
    }
}