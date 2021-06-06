using System;

namespace Hearthstone.Login
{
	// Token: 0x02001133 RID: 4403
	public interface ILoginStrategyCollection
	{
		// Token: 0x0600C0E7 RID: 49383
		bool AttemptExecuteLoginStrategy(LoginStrategyParameters parameters, TokenPromise promise);
	}
}
