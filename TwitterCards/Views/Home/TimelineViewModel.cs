using System;
using System.Collections.Generic;
using System.Linq;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Views.Home
{
	public class TimelineViewModel
	{
		public TimelineViewModel(IList<ITweet> timelineTweets)
		{
			if (timelineTweets == null)
				throw new ArgumentNullException("timelineTweets");
			if (timelineTweets.Any(t => t == null))
				throw new ArgumentException("Null reference encountered in timelineTweets collection");

			TimelineTweets = timelineTweets;
		}

		public IList<ITweet> TimelineTweets { get; private set; }
	}
}
