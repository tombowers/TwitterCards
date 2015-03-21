using System.Web.Mvc;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;

namespace TwitterCards.Views
{
	public abstract class CustomViewPage<T> : WebViewPage<T>
	{
		public ITwitterUser LoggedInUser
		{
			get
			{
				return ((Controller)ViewContext.Controller).GetLoggedInTwitterUser();
			}
		}
	}

	public abstract class CustomViewPage : CustomViewPage<object> { }
}
