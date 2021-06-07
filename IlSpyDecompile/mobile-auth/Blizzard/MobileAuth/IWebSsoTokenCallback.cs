namespace Blizzard.MobileAuth
{
	public interface IWebSsoTokenCallback
	{
		void OnWebSsoToken(string webSsoToken);

		void OnWebSsoTokenError(BlzMobileAuthError error);
	}
}
