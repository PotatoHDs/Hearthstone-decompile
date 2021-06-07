namespace Blizzard.MobileAuth
{
	public interface IRegionListener
	{
		void OnRegionRetrieved(Region region);

		void OnRegionError(BlzMobileAuthError error);
	}
}
