using System;
using System.Collections.Generic;
using System.Linq;
using TwitterCards.Core.Extensions;
using TweetSharp;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations.TweetSharp
{
	public class TweetSharpTwitterRetriever : ITwitterRetriever
	{
		private readonly string _consumerKey;
		private readonly string _consumerSecret;

		public TweetSharpTwitterRetriever(string consumerKey, string consumerSecret)
		{
			if (string.IsNullOrWhiteSpace(consumerKey))
				throw new ArgumentException("consumerKey must not be null, empty, or whitespace");
			if (string.IsNullOrWhiteSpace(consumerSecret))
				throw new ArgumentException("consumerSecret must not be null, empty, or whitespace");

			_consumerKey = consumerKey;
			_consumerSecret = consumerSecret;
		}

		public string GetRequestToken(string callbackUrl)
		{
			if (string.IsNullOrWhiteSpace(callbackUrl))
				throw new ArgumentException("callbackUrl must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);
			var requestToken = service.GetRequestToken(callbackUrl);

			return requestToken.Token;
		}

		public string GetAuthorizationUri(string requestToken)
		{
			if (string.IsNullOrWhiteSpace(requestToken))
				throw new ArgumentException("requestToken must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			return service.GetAuthorizationUri(new OAuthRequestToken { Token = requestToken }).ToString();
		}

		public IAccessToken GetAccessToken(string requestToken, string oauthVerifier)
		{
			if (string.IsNullOrWhiteSpace(requestToken))
				throw new ArgumentException("requestToken must not be null, empty, or whitespace");
			if (string.IsNullOrWhiteSpace(oauthVerifier))
				throw new ArgumentException("oauthVerifier must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			return service.GetAccessToken(new OAuthRequestToken { Token = requestToken }, oauthVerifier).ToAccessToken();
		}

		public long GetUserIdFromToken(IAccessToken accessToken)
		{
			if (accessToken == null)
				throw new ArgumentException("accessToken");

			var service = new TwitterService(_consumerKey, _consumerSecret);
			service.AuthenticateWith(accessToken.Token, accessToken.Secret);
			var user = service.VerifyCredentials(new VerifyCredentialsOptions());

			return user.Id;
		}

		public IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken)
		{
			if (accessToken == null)
				throw new ArgumentNullException("accessToken");

			// In v1.1, all API calls require authentication
			var service = new TwitterService(_consumerKey, _consumerSecret);
			service.AuthenticateWith(accessToken.Token, accessToken.Secret);

			return service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).Select(t => t.ToTweet());
		}
	}
}
