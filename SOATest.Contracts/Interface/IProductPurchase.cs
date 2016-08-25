using System;

namespace SOATest.Contracts
{
    public interface IProductPurchase
    {
        int ProductQuantity { get; set; }

        string Id { get; set; }

        IProduct Product { get; set; }
    }
}