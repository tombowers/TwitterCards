using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Runtime.Caching;

namespace TwitterCards.Core.Caching
{
	/// <summary>
	/// When applied to a class, property, or method, the result is cached.
	/// If supplied with the cacheForMinutes argument, the result is cached for that period.
	/// If the argument is not supplied, the result is cached for the life of the current request only.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
	public class CacheResultAttribute : HandlerAttribute
	{
		private readonly ICache _cache;

		public CacheResultAttribute()
		{
			_cache = RequestLifecycleCache.Current;
		}

		public CacheResultAttribute(int cacheForMinutes)
		{
			_cache = new TimeoutExpiringCache(MemoryCache.Default, TimeSpan.FromMinutes(cacheForMinutes));
		}

		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return new CacheHandler(_cache);
		}
	}
}
