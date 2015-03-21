using System.Web.Optimization;
using TwitterCards.App_Start;

namespace TwitterCards
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new LessBundle("~/Bundles/Main").Include("~/Presentation/Css/Main.less"));
		}
	}
}
