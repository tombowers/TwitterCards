using System.Web.Mvc;

namespace TwitterCards.App.Home
{
    public class HomeController : Controller
    {
		[AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Demo()
		{
			return View();
		}
    }
}
