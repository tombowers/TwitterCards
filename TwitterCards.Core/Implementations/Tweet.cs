using System;
using System.Runtime.Serialization;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	[DataContract]
	[KnownType(typeof(Tweet))]
	public class Tweet : ITweet
	{
		public Tweet(long id, string author, string text)
		{
			if (string.IsNullOrWhiteSpace(author))
				throw new ArgumentException("author must not be null, empty, or whitespace");
			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException("text must not be null, empty, or whitespace");

			Id = id;
			Author = author;
			Text = text;
		}

		[DataMember]
		public long Id { get; private set; }

		[DataMember]
		public string Author { get; private set; }

		[DataMember]
		public string Text { get; private set; }
	}
}
