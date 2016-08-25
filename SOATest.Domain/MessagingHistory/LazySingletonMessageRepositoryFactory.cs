using SOATest.Contracts;

namespace SOATest.Domain
{
    public class LazySingletonMessageRepositoryFactory : IMessageRepositoryFactory
    {
        public IMessageRepository Create()
        {
            return MessageRepository.Instance;
        }
    }
}
