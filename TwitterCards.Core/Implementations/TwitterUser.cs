using System;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	public class TwitterUser : ITwitterUser
	{
		public TwitterUser(long id, string handle)
		{
			if (string.IsNullOrWhiteSpace(handle))
				throw new ArgumentException("handle must not be null, empty, or whitespace");

			Id = id;
			Handle = handle;
		}

		public long Id { get; private set; }
		public string Handle { get; private set; }
	}
}
