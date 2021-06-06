using System;

namespace Hearthstone.Login
{
	// Token: 0x02001131 RID: 4401
	public interface ILegacyTokenStorage
	{
		// Token: 0x0600C0DB RID: 49371
		string GetStoredToken();

		// Token: 0x0600C0DC RID: 49372
		void ClearStoredToken();
	}
}
