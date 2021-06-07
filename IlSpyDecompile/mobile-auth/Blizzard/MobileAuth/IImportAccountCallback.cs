namespace Blizzard.MobileAuth
{
	public interface IImportAccountCallback
	{
		void OnImportAccountSuccess(Account account);

		void OnImportAccountError(BlzMobileAuthError error);
	}
}
