namespace Blizzard.MobileAuth
{
	public interface IBattleTagChangeListener
	{
		void OnBattleTagChange(BattleTagChangeValue battleTagChangeValue);

		void OnBattleTagChangeError(BlzMobileAuthError? error, BattleTagChangeErrorContainer? battleTagChangeErrorContainer);
	}
}
