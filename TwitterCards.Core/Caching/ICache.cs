namespace TwitterCards.Core.Caching
{
	public interface ICache
	{
		object this[string key] { get; }
		void Add(string key, object value);
		void Remove(string key);
	}
}
