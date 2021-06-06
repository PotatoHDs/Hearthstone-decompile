using System.Collections.Generic;

namespace Hearthstone.Login
{
	public interface ILegacyGuestAccountStorage
	{
		IEnumerable<GuestAccountInfo> GetStoredGuestAccounts();

		void ClearGuestAccounts();

		string GetSelectedGuestAccountId();
	}
}
