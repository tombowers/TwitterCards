using System.Collections.Generic;

namespace TwitterCards.Core.Interfaces
{
	public interface ITwitterRetriever
	{
		string GetAuthorizationUri(string callbackUrl);
		IAccessToken GetAccessToken(string requestToken, string oauthVerifier);

		ITwitterUser GetUserFromToken(IAccessToken accessToken);

		IEnumerable<ITweet> ListTweetsOnHomeTimeline(IAccessToken accessToken);
	}
}
