namespace Hearthstone.Login
{
	public interface IPlatformLoginTokenFetcher
	{
		TokenPromise FetchToken(string challengeUrl);

		void ClearCachedAuthentication();
	}
}
