using System.Collections.Generic;

namespace Hearthstone.Login
{
	public class LoginStrategyCollection : ILoginStrategyCollection
	{
		private IEnumerable<IAsyncMobileLoginStrategy> m_strategies;

		public LoginStrategyCollection(IEnumerable<IAsyncMobileLoginStrategy> strategies)
		{
			m_strategies = strategies;
		}

		public bool AttemptExecuteLoginStrategy(LoginStrategyParameters parameters, TokenPromise promise)
		{
			if (m_strategies == null)
			{
				return false;
			}
			foreach (IAsyncMobileLoginStrategy strategy in m_strategies)
			{
				if (strategy.MeetsRequirements(parameters))
				{
					strategy.StartExecution(parameters, promise);
					return true;
				}
			}
			return false;
		}
	}
}
