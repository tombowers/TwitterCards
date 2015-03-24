using System;
using System.Web.Mvc;
using TwitterCards.Core.Exceptions;
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
		public ActionResult Authorize(string returnUrl)
		{
			try
			{
				// This is the registered callback URL
				var callbackUrl = Url.Action("AuthorizeCallback", "Auth", new {redirectUrl = returnUrl}, Request.Url.Scheme);

				// Redirect to the OAuth Authorization URL
				return new RedirectResult(_twitterRetriever.GetAuthorizationUri(callbackUrl), false /*permanent*/);
			}
			catch (TwitterServiceException)
			{
				return View("Error");
			}
		}

		// This URL is registered as the application's callback at http://dev.twitter.com
		[AllowAnonymous]
		public ActionResult AuthorizeCallback(string redirectUrl, string oauth_token, string oauth_verifier)
		{
			if (string.IsNullOrWhiteSpace(oauth_token) || string.IsNullOrWhiteSpace(oauth_verifier))
				return RedirectToAction("Index", "Home");

			IAccessToken accessToken;
			ITwitterUser user;

			try
			{
				// Exchange the Request Token for an Access Token
				accessToken = _twitterRetriever.GetAccessToken(oauth_token, oauth_verifier);
				user = _twitterRetriever.GetUserFromToken(accessToken);
			}
			catch (TwitterServiceException)
			{
				return View("Error");
			}

			this.PersistTwitterAuthorization(user, accessToken);

			if (!string.IsNullOrWhiteSpace(redirectUrl))
				return Redirect(redirectUrl);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Logout()
		{
			this.ClearTwitterAuthorization();

			return RedirectToAction("Index", "Home");
		}
    }
}