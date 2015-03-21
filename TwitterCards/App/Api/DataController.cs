using System;
using System.Collections.Generic;
using System.Web.Http;
using TwitterCards.Core.Implementations;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;

namespace TwitterCards.App.Api
{
    public class DataController : ApiController
    {
		private readonly ITwitterRetriever _twitterRetriever;

		public DataController(ITwitterRetriever twitterRetriever)
		{
			if (twitterRetriever == null)
				throw new ArgumentNullException("twitterRetriever");

			_twitterRetriever = twitterRetriever;
		}

		[HttpGet]
        public IEnumerable<ITweet> Index()
        {
			var tweets = _twitterRetriever.ListTweetsOnHomeTimeline(this.GetTwitterAccessToken());
			return tweets;
        }
    }
}