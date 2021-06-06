using System;
using System.Collections.Generic;
using bgs;

namespace Hearthstone.Login
{
	// Token: 0x0200114B RID: 4427
	public class TemporaryAccountAccountWrapper : ILegacyGuestAccountStorage
	{
		// Token: 0x0600C1F1 RID: 49649 RVA: 0x003ADA04 File Offset: 0x003ABC04
		public IEnumerable<GuestAccountInfo> GetStoredGuestAccounts()
		{
			TemporaryAccountManager.TemporaryAccountData temporaryAccountData = TemporaryAccountManager.Get().GetTemporaryAccountData();
			if (temporaryAccountData == null)
			{
				return null;
			}
			List<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> temporaryAccounts = temporaryAccountData.m_temporaryAccounts;
			if (temporaryAccounts == null)
			{
				return null;
			}
			List<GuestAccountInfo> list = new List<GuestAccountInfo>();
			foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount in temporaryAccounts)
			{
				if (!temporaryAccount.m_isHealedUp && !string.IsNullOrEmpty(temporaryAccount.m_temporaryAccountId))
				{
					GuestAccountInfo item = TemporaryAccountAccountWrapper.ConvertTempAccountToGuestAccountInfo(temporaryAccount);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600C1F2 RID: 49650 RVA: 0x003ADA98 File Offset: 0x003ABC98
		public void ClearGuestAccounts()
		{
			TemporaryAccountManager.Get().DeleteTemporaryAccountData();
		}

		// Token: 0x0600C1F3 RID: 49651 RVA: 0x003ADAA4 File Offset: 0x003ABCA4
		public string GetSelectedGuestAccountId()
		{
			return TemporaryAccountManager.Get().GetSelectedTemporaryAccountId();
		}

		// Token: 0x0600C1F4 RID: 49652 RVA: 0x003ADAB0 File Offset: 0x003ABCB0
		private static GuestAccountInfo ConvertTempAccountToGuestAccountInfo(TemporaryAccountManager.TemporaryAccountData.TemporaryAccount account)
		{
			string regionId = MASDKRegionHelper.GetRegionIdForBGSRegion((constants.BnetRegion)account.m_regionId);
			if (HearthstoneApplication.IsInternal())
			{
				regionId = MASDKRegionHelper.GetRegionIdForInternalEnvironment(BattleNet.GetEnvironment());
			}
			return new GuestAccountInfo
			{
				GuestId = account.m_temporaryAccountId,
				RegionId = regionId
			};
		}
	}
}
