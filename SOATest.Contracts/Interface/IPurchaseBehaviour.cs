using System;

namespace SOATest.Contracts
{
    public interface IPurchaseBehaviour
    {
        IProductPurchase ConfirmPurchaseWith(Guid reservationId);
    }
}