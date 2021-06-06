namespace Hearthstone.Login
{
	internal class WebAuthTokenWrapper : ILegacyTokenStorage
	{
		public string GetStoredToken()
		{
			return WebAuth.GetStoredToken();
		}

		public void ClearStoredToken()
		{
			WebAuth.DeleteStoredToken();
		}
	}
}
