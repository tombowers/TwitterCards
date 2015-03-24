namespace TwitterCards.Core.Interfaces
{
	public interface ITweet
	{
		long Id { get; }
		ITwitterUser Author { get; }
		string Text { get; }
	}
}
