using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TwitterCards.Core.Implementations;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Extensions
{
	public static class ControllerExtensions
	{
		public static IAccessToken GetTwitterAccessToken(this Controller controller)
		{
			var accessToken = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterAccessToken");
			var accessTokenSecret = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterAccessTokenSecret");

			if (accessToken == null || accessTokenSecret == null)
				return null;

			return new AccessToken(accessToken.Value, accessTokenSecret.Value);
		}

		public static void PersistTwitterAuthorization(this Controller controller, string userId, IAccessToken token)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("userId must not be null, empty, or whitespace");
			if (token == null)
				throw new ArgumentNullException("token");

			// Login
			FormsAuthentication.SetAuthCookie(userId, false);

			// Save access token
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterAccessToken", token.Token));
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterAccessTokenSecret", token.Secret));
		}

		public static void ClearTwitterAuthorization(this Controller controller)
		{
			FormsAuthentication.SignOut();
		}
	}
}