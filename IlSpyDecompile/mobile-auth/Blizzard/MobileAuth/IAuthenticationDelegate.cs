namespace Blizzard.MobileAuth
{
	public interface IAuthenticationDelegate
	{
		void Authenticated(Account authenticatedAccount);

		void AuthenticationCancelled();

		void AuthenticationError(BlzMobileAuthError error);
	}
}
