namespace SOATest.Contracts
{
    public interface IMessageRepository
    {
        bool IsUniqueRequest(string correlationId);

        void SaveResponse<T>(string correlationId, T response);

        T RetrieveResponseFor<T>(string correlationId);
    }
}