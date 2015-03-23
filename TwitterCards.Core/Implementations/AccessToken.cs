using System;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Core.Implementations
{
	[Serializable]
	public class AccessToken : IAccessToken
	{
		public AccessToken(string token, string secret)
		{
			if (string.IsNullOrWhiteSpace(token))
				throw new ArgumentException("token must not be null, empty, or whitespace");
			if (string.IsNullOrWhiteSpace(secret))
				throw new ArgumentException("secret must not be null, empty, or whitespace");

			Token = token;
			Secret = secret;
		}

		public string Token { get; private set; }
		public string Secret { get; private set; }
	}
}
