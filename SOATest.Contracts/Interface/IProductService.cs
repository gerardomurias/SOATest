namespace SOATest.Contracts
{
    public interface IProductService
    {
        IProductReservationResponse ReserveProduct(IReserveProductRequest productReservationRequest);

        IPurchaseProductResponse PurchaseProduct(IPurchaseProductRequest productPurchaseRequest);
    }
}
