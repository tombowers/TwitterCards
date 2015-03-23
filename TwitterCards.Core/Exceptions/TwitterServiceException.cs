using System;
using System.Runtime.Serialization;

namespace TwitterCards.Core.Exceptions
{
	[Serializable]
	public class TwitterServiceException : Exception
	{
		public TwitterServiceException()
		{
		}

		public TwitterServiceException(string message) : base(message)
		{
		}

		public TwitterServiceException(string message, Exception inner) : base(message, inner)
		{
		}

		protected TwitterServiceException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}
