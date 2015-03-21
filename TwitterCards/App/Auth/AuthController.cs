using System;
using System.Web.Mvc;
using System.Web.Security;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;

namespace TwitterCards.App.Auth
{
    public class AuthController : Controller
    {
		private readonly ITwitterRetriever _twitterRetriever;

		public AuthController(ITwitterRetriever twitterRetriever)
		{
			if (twitterRetriever == null)
				throw new ArgumentNullException("twitterRetriever");

			_twitterRetriever = twitterRetriever;
		}

		[AllowAnonymous]
		public ActionResult Authorize()
		{
			// This is the registered callback URL
			var requestToken = _twitterRetriever.GetRequestToken("http://localhost:31206/Auth/AuthorizeCallback");

			// Redirect to the OAuth Authorization URL
			var redirectUri = _twitterRetriever.GetAuthorizationUri(requestToken);
			return new RedirectResult(redirectUri, false /*permanent*/);
		}

		// This URL is registered as the application's callback at http://dev.twitter.com
		[AllowAnonymous]
		public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
		{
			if (string.IsNullOrWhiteSpace(oauth_token) || string.IsNullOrWhiteSpace(oauth_verifier))
				return RedirectToAction("Index", "Home");

			// Exchange the Request Token for an Access Token
			var accessToken = _twitterRetriever.GetAccessToken(oauth_token, oauth_verifier);

			var user = _twitterRetriever.GetUserFromToken(accessToken);

			this.PersistTwitterAuthorization(user, accessToken);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Logout()
		{
			this.ClearTwitterAuthorization();

			return RedirectToAction("Index", "Home");
		}
    }
}