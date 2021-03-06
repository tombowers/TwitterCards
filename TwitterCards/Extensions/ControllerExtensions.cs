﻿using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using TwitterCards.Core.Implementations;
using TwitterCards.Core.Interfaces;

namespace TwitterCards.Extensions
{
	public static class ControllerExtensions
	{
		public static IAccessToken GetTwitterAccessToken(this ApiController controller)
		{
			var accessToken = controller.Request.Headers.GetCookies("twitterAccessToken").FirstOrDefault();
			var accessTokenSecret = controller.Request.Headers.GetCookies("twitterAccessTokenSecret").FirstOrDefault();

			if (accessToken == null || accessTokenSecret == null)
				return null;

			return new AccessToken(accessToken["twitterAccessToken"].Value, accessTokenSecret["twitterAccessTokenSecret"].Value);
		}

		public static IAccessToken GetTwitterAccessToken(this Controller controller)
		{
			var accessToken = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterAccessToken");
			var accessTokenSecret = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterAccessTokenSecret");

			if (accessToken == null || accessTokenSecret == null)
				return null;

			return new AccessToken(accessToken.Value, accessTokenSecret.Value);
		}

		public static void PersistTwitterAuthorization(this Controller controller, ITwitterUser user, IAccessToken token)
		{
			if (user == null)
				throw new ArgumentNullException("user");
			if (token == null)
				throw new ArgumentNullException("token");

			// Login
			FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);

			// N.B. This all should normally be added to user identity
			// Save access token
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterAccessToken", token.Token));
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterAccessTokenSecret", token.Secret));

			// Save user data
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterId", user.Id.ToString()));
			controller.ControllerContext.HttpContext.Response.SetCookie(new HttpCookie("twitterName", user.Name));
		}

		public static void ClearTwitterAuthorization(this Controller controller)
		{
			FormsAuthentication.SignOut();

			string[] myCookies = controller.ControllerContext.HttpContext.Request.Cookies.AllKeys;
			foreach (string cookie in myCookies)
			{
				controller.ControllerContext.HttpContext.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
			}
		}

		public static ITwitterUser GetLoggedInTwitterUser(this Controller controller)
		{
			var id = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterId");
			var name = controller.ControllerContext.HttpContext.Request.Cookies.Get("twitterName");

			if (id == null || name == null)
				return null;

			return new TwitterUser(Convert.ToInt64(id.Value), name.Value);
		}
	}
}