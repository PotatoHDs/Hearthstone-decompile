namespace Hearthstone.Login
{
	public interface IAsyncMobileLoginStrategy
	{
		bool MeetsRequirements(LoginStrategyParameters parameters);

		void StartExecution(LoginStrategyParameters parameters, TokenPromise promise);
	}
}
