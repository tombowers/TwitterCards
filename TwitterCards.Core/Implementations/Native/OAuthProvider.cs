using System;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations.Native
{
	public class OAuthProvider : IOAuthProvider
	{
		public string GetRequestToken()
		{
			throw new NotImplementedException();
		}

		private void GenerateAuthHeader()
		{
			// Build auth header with:
			// 1) oauth_consumer_key: app consumer key from twitter
			// 2) oauth_nonce: relatively random alphanumeric string
			// 3) oauth_signature: https://dev.twitter.com/oauth/overview/creating-signatures
			// 4) oauth_signature_method: HMAC-SHA1
			// 5) oauth_timestamp: seconds since unix epoch
			// 6) oauth_token: access token, needed for requests which require authorisation
			// 7) oauth_version	1.0
		}
	}
}
