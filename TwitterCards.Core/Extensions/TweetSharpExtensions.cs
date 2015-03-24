using System.Linq;
using TweetSharp;
using TwitterCards.Core.Interfaces;
using Abstracted = TwitterCards.Core.Implementations;

namespace TwitterCards.Core.Extensions
{
	public static class TweetSharpExtensions
	{
		public static ITweet ToTweet(this TwitterStatus twitterStatus)
		{
			var mediaEntity = twitterStatus.Entities.Media.FirstOrDefault();

			return new Abstracted.Tweet(
				twitterStatus.Id,
				twitterStatus.User.ToTwitterUser(),
				twitterStatus.Text,
				mediaEntity != null ? mediaEntity.MediaUrl : null
			);
		}

		public static IAccessToken ToAccessToken(this OAuthAccessToken accessToken)
		{
			return new Abstracted.AccessToken(accessToken.Token, accessToken.TokenSecret);
		}

		public static ITwitterUser ToTwitterUser(this TwitterUser user)
		{
			return new Abstracted.TwitterUser(user.Id, user.Name, user.ScreenName, user.ProfileImageUrl);
		}
	}
}
