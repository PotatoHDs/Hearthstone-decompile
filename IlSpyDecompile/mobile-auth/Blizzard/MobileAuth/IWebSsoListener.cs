namespace Blizzard.MobileAuth
{
	public interface IWebSsoListener
	{
		void OnWebSsoUrlReady(string webSsoUrl);

		void OnSsoUrlError(BlzMobileAuthError error);
	}
}
