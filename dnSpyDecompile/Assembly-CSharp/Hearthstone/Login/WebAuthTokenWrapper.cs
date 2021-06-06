using System;

namespace Hearthstone.Login
{
	// Token: 0x0200114D RID: 4429
	internal class WebAuthTokenWrapper : ILegacyTokenStorage
	{
		// Token: 0x0600C20C RID: 49676 RVA: 0x003ADE25 File Offset: 0x003AC025
		public string GetStoredToken()
		{
			return WebAuth.GetStoredToken();
		}

		// Token: 0x0600C20D RID: 49677 RVA: 0x003ADE2C File Offset: 0x003AC02C
		public void ClearStoredToken()
		{
			WebAuth.DeleteStoredToken();
		}
	}
}
