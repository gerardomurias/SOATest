using SOATest.Contracts;

namespace SOATest.Domain
{
    public class LazySingletonProductRepositoryFactory : IProductRepositoryFactory
    {
        public IProductRepository Create()
        {
            return ProductRepository.Instance;
        }
    }
}