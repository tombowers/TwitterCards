using System.Web.Mvc;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;

namespace TwitterCards.Views
{
	public abstract class CustomViewPage<T> : WebViewPage<T>
	{
		protected ITwitterUser LoggedInUser
		{
			get
			{
				return ((Controller)ViewContext.Controller).GetLoggedInTwitterUser();
			}
		}

		protected string ControllerName
		{
			get { return ViewContext.Controller.ControllerContext.RouteData.Values["controller"].ToString(); }
		}

		protected string ActionName
		{
			get { return ViewContext.Controller.ControllerContext.RouteData.Values["action"].ToString(); }
		}
	}

	public abstract class CustomViewPage : CustomViewPage<object> { }
}
