namespace Blizzard.MobileAuth
{
	internal interface IBlzAuthCallbackHandler
	{
		void OnBlzAccountReceived(string message);

		void OnCancelled(string message);

		void OnAuthError(string message);
	}
}
