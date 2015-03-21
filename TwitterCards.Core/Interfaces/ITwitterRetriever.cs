using System.Collections.Generic;

namespace TwitterCards.Core.Interfaces
{
	public interface ITwitterRetriever
	{
		string GetRequestToken(string callbackUrl);
		string GetAuthorizationUri(string requestToken);
		IAccessToken GetAccessToken(string requestToken, string oauthVerifier);

		ITwitterUser GetUserFromToken(IAccessToken accessToken);

		IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken);
	}
}
