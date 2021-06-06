using System;

namespace Hearthstone.Login
{
	// Token: 0x02001136 RID: 4406
	public interface IPlatformLoginTokenFetcher
	{
		// Token: 0x0600C0FF RID: 49407
		TokenPromise FetchToken(string challengeUrl);

		// Token: 0x0600C100 RID: 49408
		void ClearCachedAuthentication();
	}
}
