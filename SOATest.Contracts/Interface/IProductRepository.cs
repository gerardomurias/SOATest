using System;

namespace SOATest.Contracts
{
    public interface IProductRepository
    {
        IProduct FindBy(string productId);

        void Save(IProduct product);
    }
}
