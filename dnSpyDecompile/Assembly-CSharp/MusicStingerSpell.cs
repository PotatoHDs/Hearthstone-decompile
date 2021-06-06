using System;

// Token: 0x02000786 RID: 1926
public class MusicStingerSpell : CardSoundSpell
{
	// Token: 0x06006C3A RID: 27706 RVA: 0x00230838 File Offset: 0x0022EA38
	private bool CanPlay()
	{
		if (GameState.Get() == null)
		{
			return true;
		}
		Player controller = base.GetSourceCard().GetController();
		return controller == null || controller.IsLocalUser();
	}

	// Token: 0x0400579F RID: 22431
	public MusicStingerData m_MusicStingerData = new MusicStingerData();
}
