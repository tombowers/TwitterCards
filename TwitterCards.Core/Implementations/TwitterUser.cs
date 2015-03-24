using System;
using System.Runtime.Serialization;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	[DataContract]
	[KnownType(typeof(TwitterUser))]
	public class TwitterUser : ITwitterUser
	{
		public TwitterUser(long id, string name, string screenName = null, string profileImageUrl = null)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("name must not be null, empty, or whitespace");

			Id = id;
			Name = name;
			ScreenName = screenName;
			ProfileImageUrl = profileImageUrl;
		}

		[DataMember]
		public long Id { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public string ScreenName { get; private set; }

		[DataMember]
		public string ProfileImageUrl { get; private set; }

	}
}
