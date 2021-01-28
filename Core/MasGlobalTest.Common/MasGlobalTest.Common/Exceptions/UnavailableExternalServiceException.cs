using System;
using System.Runtime.Serialization;

namespace MasGlobalTest.Common.Exceptions
{
    [Serializable]
    public class UnavailableExternalServiceException : Exception
    {
        public int ExternalSystemId { get; set; }

        public UnavailableExternalServiceException() { }

        public UnavailableExternalServiceException(string message) : base(message) { }

        public UnavailableExternalServiceException(string message, Exception inner) : base(message, inner) { }

        protected UnavailableExternalServiceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
