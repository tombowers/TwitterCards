using System;
using System.Runtime.Serialization;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	[DataContract]
	[KnownType(typeof(TwitterUser))]
	public class TwitterUser : ITwitterUser
	{
		public TwitterUser(long id, string handle, string profileImageUrl = null)
		{
			if (string.IsNullOrWhiteSpace(handle))
				throw new ArgumentException("handle must not be null, empty, or whitespace");

			Id = id;
			Handle = handle;
			ProfileImageUrl = profileImageUrl;
		}

		[DataMember]
		public long Id { get; private set; }

		[DataMember]
		public string Handle { get; private set; }

		[DataMember]
		public string ProfileImageUrl { get; private set; }
	}
}
