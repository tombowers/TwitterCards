﻿using System;
using System.Runtime.Serialization;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	[DataContract]
	[KnownType(typeof(Tweet))]
	public class Tweet : ITweet
	{
		public Tweet(long id, ITwitterUser author, string text)
		{
			if (author == null)
				throw new ArgumentNullException("author");
			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException("text must not be null, empty, or whitespace");

			Id = id;
			Author = author;
			Text = text;
		}

		[DataMember]
		public long Id { get; private set; }

		[DataMember]
		public ITwitterUser Author { get; private set; }

		[DataMember]
		public string Text { get; private set; }
	}
}
