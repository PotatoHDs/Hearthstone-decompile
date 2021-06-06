public class MusicStingerSpell : CardSoundSpell
{
	public MusicStingerData m_MusicStingerData = new MusicStingerData();

	private bool CanPlay()
	{
		if (GameState.Get() == null)
		{
			return true;
		}
		return GetSourceCard().GetController()?.IsLocalUser() ?? true;
	}
}
