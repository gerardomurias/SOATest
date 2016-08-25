using System;

namespace SOATest.Service
{
    public class LimitedAvailabilityException : Exception
    {
        public LimitedAvailabilityException(string message)
        : base(message)
        { }

        public LimitedAvailabilityException()
            : base("There are not enough products left to fulfil your request.")
        { }
    }
}
