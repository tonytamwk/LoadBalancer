using System;
using System.Runtime.Serialization;

namespace Exercise3
{
    [Serializable]
    internal class AllThresholdsReachedException : Exception
    {
        public AllThresholdsReachedException()
        {
        }

        public AllThresholdsReachedException(string message) : base(message)
        {
        }

        public AllThresholdsReachedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AllThresholdsReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}