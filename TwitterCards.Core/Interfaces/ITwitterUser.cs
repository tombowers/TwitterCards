namespace TwitterCards.Core.Interfaces
{
	public interface ITwitterUser
	{
		long Id { get; }
		string Name { get; }
		string ScreenName { get; }
		string ProfileImageUrl { get; }
	}
}
