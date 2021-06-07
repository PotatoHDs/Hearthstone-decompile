namespace Blizzard.MobileAuth
{
	public interface IBattleTagInfoListener
	{
		void OnBattleTagInfoRetrieved(BattleTagInfo battleTagInfo);

		void OnBatleTagInfoError(BlzMobileAuthError error);
	}
}
