namespace TwitterCards.Core.Interfaces
{
	public interface ITweet
	{
		long Id { get; }
		string Author { get; }
		string Text { get; }
	}
}
