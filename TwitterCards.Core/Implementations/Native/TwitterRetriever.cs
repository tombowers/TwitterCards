using System;
using System.Collections.Generic;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations.Native
{
	public class TwitterRetriever : ITwitterRetriever
	{
		private readonly IOAuthProvider _oAuthProvider;

		public TwitterRetriever(IOAuthProvider oAuthProvider)
		{
			if (oAuthProvider == null)
				throw new ArgumentNullException("oAuthProvider");

			_oAuthProvider = oAuthProvider;
		}

		public string GetAuthorizationUri(string callbackUrl)
		{
			// 1: get request token from oauth provider
			// 2: get auth url from twitter using request token

			throw new NotImplementedException();
		}

		public IAccessToken GetAccessToken(string requestToken, string oauthVerifier)
		{
			throw new NotImplementedException();
		}

		public ITwitterUser GetUserFromToken(IAccessToken accessToken)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken)
		{
			throw new NotImplementedException();
		}
	}
}
