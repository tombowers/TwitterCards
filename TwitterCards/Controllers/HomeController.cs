using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCards.Core;
using TwitterCards.Core.Interfaces;
using TwitterCards.Extensions;
using TwitterCards.Views.Home;

namespace TwitterCards.Controllers
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

		public ActionResult Timeline()
		{
			return View(new TimelineViewModel(_twitterRetriever.ListTweetsOnHomeTimeline(this.GetTwitterAccessToken()).ToList()));
		}
    }
}