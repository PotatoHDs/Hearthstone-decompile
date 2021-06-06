namespace Hearthstone.Login
{
	public interface ILoginStrategyCollection
	{
		bool AttemptExecuteLoginStrategy(LoginStrategyParameters parameters, TokenPromise promise);
	}
}
