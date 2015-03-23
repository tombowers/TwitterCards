using System;
using System.Collections.Generic;
using System.Web;

namespace TwitterCards.Core.Caching
{
	public class RequestLifecycleCache : ICache
	{
		private readonly Dictionary<string, object> _items;

		private RequestLifecycleCache()
		{
			_items = new Dictionary<string, object>();
		}

		public static RequestLifecycleCache Current
		{
			get
			{
				return (HttpContext.Current.Items["RequestLifecycleCache"]
					?? (HttpContext.Current.Items["RequestLifecycleCache"] = new RequestLifecycleCache())) as RequestLifecycleCache;
			}
		}

		public object this[string key]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(key))
					throw new ArgumentException("key must not be null, empty, or whitespace");

				return _items.ContainsKey(key) ? _items[key] : null;
			}
		}

		public void Add(string key, object value)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("key must not be null, empty, or whitespace");
			if (value == null)
				throw new ArgumentNullException("value");

			_items[key] = value;
		}

		public void Remove(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("key must not be null, empty, or whitespace");

			_items.Remove(key);
		}
	}
}
