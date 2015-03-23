using System;
using System.Runtime.Caching;

namespace TwitterCards.Core.Caching
{
	public class TimeoutExpiringCache : ICache
	{
		private readonly ObjectCache _cache;
		private readonly TimeSpan _cacheDuration;

		public TimeoutExpiringCache(ObjectCache cache, TimeSpan cacheDuration)
		{
			if (cache == null)
				throw new ArgumentNullException("cache");
			if (cacheDuration.Ticks < 1)
				throw new ArgumentOutOfRangeException("cacheDuration", "cacheDuration must be a positive integer");

			_cache = cache;
			_cacheDuration = cacheDuration;
		}

		public object this[string key]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(key))
					throw new ArgumentException("key must not be null, empty, or whitespace");

				return _cache[key.Trim()];
			}
		}

		public void Add(string key, object value)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("key must not be null, empty, or whitespace");
			if (value == null)
				throw new ArgumentNullException("value");

			// Don't overflow if a large cacheDuration (such as TimeSpan.MaxValue) was specified
			var expirationPoint = DateTime.MaxValue.Subtract(DateTime.Now) < _cacheDuration ? DateTime.MaxValue : DateTime.Now.Add(_cacheDuration);

			_cache.Add(key.Trim(), value, expirationPoint);
		}

		public void Remove(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("key must not be null, empty, or whitespace");

			_cache.Remove(key.Trim());
		}
	}
}
