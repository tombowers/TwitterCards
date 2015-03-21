using TweetSharp;
using TwitterCards.Core.Interfaces;
using Abstracted = TwitterCards.Core.Implementations;

namespace TwitterCards.Core.Extensions
{
	public static class TweetSharpExtensions
	{
		public static ITweet ToTweet(this TwitterStatus twitterStatus)
		{
			return new Abstracted.Tweet(twitterStatus.Id, twitterStatus.Author.ScreenName, twitterStatus.Text);
		}

		public static IAccessToken ToAccessToken(this OAuthAccessToken accessToken)
		{
			return new Abstracted.AccessToken(accessToken.Token, accessToken.TokenSecret);
		}

		public static ITwitterUser ToTwitterUser(this TweetSharp.TwitterUser user)
		{
			return new Abstracted.TwitterUser(user.Id, user.ScreenName);
		}
	}
}
