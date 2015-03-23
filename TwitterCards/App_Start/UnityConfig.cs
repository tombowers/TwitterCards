using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using TwitterCards.Core.Implementations.TweetSharp;
using TwitterCards.Core.Interfaces;

namespace TwitterCards
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="unityContainer">The unity unityContainer to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
			unityContainer.AddNewExtension<Interception>();

			var interfaceInterceptor = new Interceptor<InterfaceInterceptor>();
			var interceptionBehaviour = new InterceptionBehavior<PolicyInjectionBehavior>();

            unityContainer.RegisterType<ITwitterRetriever, TweetSharpTwitterRetriever>(
				interfaceInterceptor,
				interceptionBehaviour,
				new InjectionConstructor(
				ConfigurationManager.AppSettings["Twitter:ConsumerKey"],
				ConfigurationManager.AppSettings["Twitter:ConsumerSecret"]
				));
        }
    }
}
