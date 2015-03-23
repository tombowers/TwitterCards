using System.Web.Optimization;

namespace TwitterCards
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.UseCdn = true;
			BundleTable.EnableOptimizations = true; // force optimizations while debugging (including loading from cdn)

			bundles.Add(new LessBundle("~/Bundles/MainCss").Include("~/Presentation/Css/Main.less"));
			bundles.Add(new LessBundle("~/Bundles/DemoCss").Include("~/Presentation/Css/Demo.less"));

			// jQuery from CDN with local backup
			var jQueryBundle = new ScriptBundle("~/Bundles/jQuery", "//code.jquery.com/jquery-2.1.3.min.js").Include("~/Presentation/Javascript/jQuery/jquery-2.1.3.min.js");
			jQueryBundle.CdnFallbackExpression = "window.jQuery";
			bundles.Add(jQueryBundle);

			bundles.Add(new ScriptBundle("~/Bundles/jQueryTransit").Include("~/Presentation/Javascript/jQuery/jquery.transit.js"));

			bundles.Add(new ScriptBundle("~/Bundles/DemoJavascript").Include("~/Presentation/Javascript/Demo.js"));
		}
	}
}
