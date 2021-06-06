using System.Collections.Generic;
using bgs;

namespace Hearthstone.Login
{
	public class TemporaryAccountAccountWrapper : ILegacyGuestAccountStorage
	{
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
			foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount item2 in temporaryAccounts)
			{
				if (!item2.m_isHealedUp && !string.IsNullOrEmpty(item2.m_temporaryAccountId))
				{
					GuestAccountInfo item = ConvertTempAccountToGuestAccountInfo(item2);
					list.Add(item);
				}
			}
			return list;
		}

		public void ClearGuestAccounts()
		{
			TemporaryAccountManager.Get().DeleteTemporaryAccountData();
		}

		public string GetSelectedGuestAccountId()
		{
			return TemporaryAccountManager.Get().GetSelectedTemporaryAccountId();
		}

		private static GuestAccountInfo ConvertTempAccountToGuestAccountInfo(TemporaryAccountManager.TemporaryAccountData.TemporaryAccount account)
		{
			string regionId = MASDKRegionHelper.GetRegionIdForBGSRegion((constants.BnetRegion)account.m_regionId);
			if (HearthstoneApplication.IsInternal())
			{
				regionId = MASDKRegionHelper.GetRegionIdForInternalEnvironment(BattleNet.GetEnvironment());
			}
			GuestAccountInfo result = default(GuestAccountInfo);
			result.GuestId = account.m_temporaryAccountId;
			result.RegionId = regionId;
			return result;
		}
	}
}
