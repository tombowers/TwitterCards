using System;
using System.Collections.Generic;
using System.Linq;
using TweetSharp;
using TwitterCards.Core.Exceptions;
using TwitterCards.Core.Extensions;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations.TweetSharp
{
	public class TweetSharpTwitterRetriever : ITwitterRetriever
	{
		private static IEnumerable<ITweet> _tempCachedTweets;

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

		/// <summary>
		/// Get a request token.
		/// </summary>
		/// <param name="callbackUrl"></param>
		/// <exception cref="TwitterServiceException"></exception>
		public string GetRequestToken(string callbackUrl)
		{
			if (string.IsNullOrWhiteSpace(callbackUrl))
				throw new ArgumentException("callbackUrl must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			try
			{
				return service.GetRequestToken(callbackUrl).Token;
			}
			catch (Exception e)
			{
				throw new TwitterServiceException("Unable to get request token.", e);
			}
		}

		/// <summary>
		/// Get a url which can be used to prompt a user for authorization.
		/// </summary>
		/// <param name="requestToken"></param>
		/// <exception cref="TwitterServiceException"></exception>
		public string GetAuthorizationUri(string requestToken)
		{
			if (string.IsNullOrWhiteSpace(requestToken))
				throw new ArgumentException("requestToken must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			try
			{
				return service.GetAuthorizationUri(new OAuthRequestToken {Token = requestToken}).ToString();
			}
			catch (Exception e)
			{
				throw new TwitterServiceException("Unable to get authorization uri.", e);
			}
		}

		/// <summary>
		/// Get an access token from the request token and OAuth verifier.
		/// </summary>
		/// <param name="requestToken"></param>
		/// <param name="oauthVerifier"></param>
		/// <exception cref="TwitterServiceException"></exception>
		public IAccessToken GetAccessToken(string requestToken, string oauthVerifier)
		{
			if (string.IsNullOrWhiteSpace(requestToken))
				throw new ArgumentException("requestToken must not be null, empty, or whitespace");
			if (string.IsNullOrWhiteSpace(oauthVerifier))
				throw new ArgumentException("oauthVerifier must not be null, empty, or whitespace");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			try {
				return service.GetAccessToken(new OAuthRequestToken { Token = requestToken }, oauthVerifier).ToAccessToken();
			}
			catch (Exception e)
			{
				throw new TwitterServiceException("Unable to get access token.", e);
			}
		}

		/// <summary>
		/// Get the ITwitterUser related to an access token.
		/// </summary>
		/// <param name="accessToken"></param>
		/// <exception cref="TwitterServiceException"></exception>
		public ITwitterUser GetUserFromToken(IAccessToken accessToken)
		{
			if (accessToken == null)
				throw new ArgumentException("accessToken");

			var service = new TwitterService(_consumerKey, _consumerSecret);

			try
			{
				service.AuthenticateWith(accessToken.Token, accessToken.Secret);
				return service.VerifyCredentials(new VerifyCredentialsOptions()).ToTwitterUser();
			}
			catch (Exception e)
			{
				throw new TwitterServiceException("Unable to get access token.", e);
			}
		}

		/// <summary>
		/// List the timeline tweets of the user related to the specified access token.
		/// </summary>
		/// <param name="accessToken"></param>
		/// <exception cref="TwitterServiceException"></exception>
		public IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken)
		{
			if (accessToken == null)
				throw new ArgumentNullException("accessToken");

			if (_tempCachedTweets != null)
				return _tempCachedTweets;

			// In v1.1, all API calls require authentication
			var service = new TwitterService(_consumerKey, _consumerSecret);

			try
			{
				service.AuthenticateWith(accessToken.Token, accessToken.Secret);

				var tweetSharpTweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();

				if (tweetSharpTweets.Any(t => t == null || t.User == null))
					throw new Exception("Forcing exception for awful internal TweetSharp error");

				var tweets = tweetSharpTweets.Select(t => t.ToTweet()).ToList();

				_tempCachedTweets = tweets;
				return tweets;
			}
			catch (Exception e)
			{
				throw new TwitterServiceException("Unable to get access token.", e);
			}
		}
	}
}
