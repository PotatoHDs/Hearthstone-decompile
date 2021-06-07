namespace Blizzard.MobileAuth
{
	public interface IGuestSoftAccountIdListener
	{
		void OnGuestSoftAccountIdRetrieved(GuestSoftAccountId guestSoftAccountId);

		void OnGuestSoftAccountIdError(BlzMobileAuthError error);
	}
}
