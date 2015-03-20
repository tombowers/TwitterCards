using TweetSharp;
using TwitterCards.Core.Implementations;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Extensions
{
	public static class TweetSharpExtensions
	{
		public static ITweet ToTweet(this TwitterStatus twitterStatus)
		{
			return new Tweet(twitterStatus.Id, twitterStatus.Author.ScreenName, twitterStatus.Text);
		}

		public static IAccessToken ToAccessToken(this OAuthAccessToken accessToken)
		{
			return new AccessToken(accessToken.Token, accessToken.TokenSecret);
		}
	}
}
