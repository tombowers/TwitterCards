using System.Collections.Generic;
using TweetSharp;

namespace TwitterCards.Core.Interfaces
{
	public interface ITwitterRetriever
	{
		string GetRequestToken(string callbackUrl);
		string GetAuthorizationUri(string requestToken);
		IAccessToken GetAccessToken(string requestToken, string oauthVerifier);

		long GetUserIdFromToken(IAccessToken accessToken);

		IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken);
	}
}
