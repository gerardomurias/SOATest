using System.Collections.Generic;
using SOATest.Contracts;

namespace SOATest.Domain
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Dictionary<string, object> _responseHistory;

        public MessageRepository()
        {
            _responseHistory = new Dictionary<string, object>();
        }

        public bool IsUniqueRequest(string correlationId)
        {
            return !_responseHistory.ContainsKey(correlationId);
        }

        public void SaveResponse<T>(string correlationId, T response)
        {
            _responseHistory[correlationId] = response;
        }

        public T RetrieveResponseFor<T>(string correlationId)
        {
            if (_responseHistory.ContainsKey(correlationId))
            {
                return (T) _responseHistory[correlationId];
            }

            return default(T);
        }


        #region "Singleton Pattern"

        public static IMessageRepository Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            internal static readonly IMessageRepository instance = new MessageRepository();

            static Nested() { }
        }

        #endregion
    }
}