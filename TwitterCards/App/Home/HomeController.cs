using System;
using System.Linq;
using System.Web.Mvc;
using TwitterCards.App.Home.ViewModels;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;

namespace TwitterCards.App.Home
{
    public class HomeController : Controller
    {
		private readonly ITwitterRetriever _twitterRetriever;

		public HomeController(ITwitterRetriever twitterRetriever)
		{
			if (twitterRetriever == null)
				throw new ArgumentNullException("twitterRetriever");

			_twitterRetriever = twitterRetriever;
		}

		[AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Following()
		{
			return View();
		}

		public ActionResult Timeline()
		{
			return View(new TimelineViewModel(_twitterRetriever.ListTweetsOnHomeTimeline(this.GetTwitterAccessToken()).ToList()));
		}
    }
}