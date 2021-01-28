using System;

namespace MasGlobalTest.Common.Exceptions
{
    [Serializable]
    public class EntityDoesNotExistException : Exception
    {
        public Type EntityType { get; set; }

        public EntityDoesNotExistException()
        {
            EntityType = default!;
        }

        public EntityDoesNotExistException(string message) : base(message)
        {
            EntityType = default!;
        }

        public EntityDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
            EntityType = default!;
        }

        protected EntityDoesNotExistException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            EntityType = default!;
        }
    }
}
