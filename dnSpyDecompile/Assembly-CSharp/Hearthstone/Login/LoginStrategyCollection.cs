using System;
using System.Collections.Generic;

namespace Hearthstone.Login
{
	// Token: 0x0200113D RID: 4413
	public class LoginStrategyCollection : ILoginStrategyCollection
	{
		// Token: 0x0600C15A RID: 49498 RVA: 0x003ABDEC File Offset: 0x003A9FEC
		public LoginStrategyCollection(IEnumerable<IAsyncMobileLoginStrategy> strategies)
		{
			this.m_strategies = strategies;
		}

		// Token: 0x0600C15B RID: 49499 RVA: 0x003ABDFC File Offset: 0x003A9FFC
		public bool AttemptExecuteLoginStrategy(LoginStrategyParameters parameters, TokenPromise promise)
		{
			if (this.m_strategies == null)
			{
				return false;
			}
			foreach (IAsyncMobileLoginStrategy asyncMobileLoginStrategy in this.m_strategies)
			{
				if (asyncMobileLoginStrategy.MeetsRequirements(parameters))
				{
					asyncMobileLoginStrategy.StartExecution(parameters, promise);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04009C19 RID: 39961
		private IEnumerable<IAsyncMobileLoginStrategy> m_strategies;
	}
}
