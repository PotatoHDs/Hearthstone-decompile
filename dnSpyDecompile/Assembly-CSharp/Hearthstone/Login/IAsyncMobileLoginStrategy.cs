using System;

namespace Hearthstone.Login
{
	// Token: 0x0200112E RID: 4398
	public interface IAsyncMobileLoginStrategy
	{
		// Token: 0x0600C0D2 RID: 49362
		bool MeetsRequirements(LoginStrategyParameters parameters);

		// Token: 0x0600C0D3 RID: 49363
		void StartExecution(LoginStrategyParameters parameters, TokenPromise promise);
	}
}
