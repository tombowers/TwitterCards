using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web.UI;

namespace TwitterCards.Core.Caching
{
	/// <summary>
	/// An <see cref="ICallHandler"/> that implements caching of the return values of methods.
	/// </summary>
	[ConfigurationElementType(typeof(CacheHandler)), Synchronization]
	public class CacheHandler : ICallHandler
	{
		private readonly ICache _cache;

		public CacheHandler(ICache cache)
		{
			if (cache == null)
				throw new ArgumentNullException("cache");

			_cache = cache;
		}

		#region ICallHandler Members

		/// <summary>
		/// Implements the caching behavior of this handler.
		/// </summary>
		/// <param name="input"><see cref="IMethodInvocation"/> object describing the current call.</param>
		/// <param name="getNext">delegate used to get the next handler in the current pipeline.</param>
		/// <returns>Return value from target method, or cached result if previous inputs have been seen.</returns>
		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			// We need to synchronize calls to the CacheHandler on method level
			// to prevent duplicate calls to methods that could be cached.
			lock (input.MethodBase)
			{
				if (TargetMethodReturnsVoid(input))
					return getNext()(input, getNext);

				var inputs = new object[input.Inputs.Count];
				for (var i = 0; i < inputs.Length; ++i)
					inputs[i] = input.Inputs[i];

				var cacheKey = CreateCacheKey(input.MethodBase, inputs);
				if (cacheKey != null)
				{
					var cachedResult = _cache[cacheKey];

					if (cachedResult != null)
						return input.CreateMethodReturn(cachedResult, input.Arguments);
				}

				var realReturn = getNext()(input, getNext);

				if (realReturn.Exception == null && realReturn.ReturnValue != null && cacheKey != null)
					AddToCache(cacheKey, realReturn.ReturnValue);

				return realReturn;
			}
		}

		public int Order
		{
			get { return 0; }
			set { }
		}

		#endregion

		private static bool TargetMethodReturnsVoid(IMethodInvocation input)
		{
			var targetMethod = input.MethodBase as MethodInfo;
			return targetMethod != null && targetMethod.ReturnType == typeof(void);
		}

		private void AddToCache(string key, object valueToCache)
		{
			_cache.Add(key, valueToCache);
		}

        /// <summary>
        /// Create a cache key for the given method and set of input arguments.
        /// </summary>
        /// <param name="method">Method being called.</param>
        /// <param name="inputs">Input arguments.</param>
        /// <returns>A (hopefully) unique string to be used as a cache key.</returns>
        private static string CreateCacheKey(MethodBase method, params object[] inputs)
        {
			var serializer = new LosFormatter(false, "");

            try
            {
                var sb = new StringBuilder();

                if (method.DeclaringType != null)
                {
                    sb.Append(method.DeclaringType.FullName);
                }

                sb.Append(':');
                sb.Append(method.Name);

                TextWriter writer = new StringWriter(sb);

	            if (inputs == null)
					return sb.ToString();

	            foreach (var input in inputs)
	            {
		            sb.Append(':');

		            if (input == null)
						continue;

		            // Different instances of DateTime which represents the same value
		            // sometimes serialize differently due to some internal variables which are different.
		            // We therefore serialize it using Ticks instead. instead.
		            var inputDateTime = input as DateTime?;
		            if (inputDateTime.HasValue)
		            {
			            sb.Append(inputDateTime.Value.Ticks);
		            }
		            else
		            {
			            if (input as IEnumerable != null)
			            {
				            foreach (var argument in (IEnumerable)input)
					            serializer.Serialize(writer, argument);
			            }
			            else
						{
				            // Serialize the input and write it to the key StringBuilder.
				            serializer.Serialize(writer, input);
			            }
		            }
	            }

	            return sb.ToString();
            }
            catch
            {
                // Something went wrong when generating the key probably an input-value was not serializable.
                // Return a null key.
                return null;
            }
        }
	}
}
