namespace Hearthstone.Core.Streaming
{
	public interface IIndicatorChecker
	{
		bool Exists(string[] files);
	}
}
