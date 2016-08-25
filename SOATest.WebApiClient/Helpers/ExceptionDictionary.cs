using System;
using System.Collections.Generic;
using System.Net;
using SOATest.Service;

namespace SOATest.WebApiClient
{
    public static class ExceptionDictionary
    {
        public static HttpStatusCode ConvertToHttpStatusCode(this Exception exception)
        {
            Dictionary<Type, HttpStatusCode> dictionary = GetExceptionDictionary();

            if (dictionary.ContainsKey(exception.GetType()))
            {
                return dictionary[exception.GetType()];
            }

            return dictionary[typeof (Exception)];
        }

        public static Dictionary<Type, HttpStatusCode> GetExceptionDictionary()
        {
            var dictionary = new Dictionary<Type, HttpStatusCode>();

            dictionary[typeof (ResourceNotFoundException)] = HttpStatusCode.NotFound;
            dictionary[typeof (LimitedAvailabilityException)] = HttpStatusCode.InternalServerError;
            dictionary[typeof (Exception)] = HttpStatusCode.InternalServerError;

            return dictionary;
        }
    }
}