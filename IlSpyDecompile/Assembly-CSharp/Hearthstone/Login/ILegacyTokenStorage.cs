namespace Hearthstone.Login
{
	public interface ILegacyTokenStorage
	{
		string GetStoredToken();

		void ClearStoredToken();
	}
}
