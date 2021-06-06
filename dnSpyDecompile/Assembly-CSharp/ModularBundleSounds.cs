using System;
using UnityEngine;

// Token: 0x02000711 RID: 1809
public class ModularBundleSounds : MonoBehaviour
{
	// Token: 0x0600650C RID: 25868 RVA: 0x0020FAAD File Offset: 0x0020DCAD
	public void Initialize(string entrySound, string landingSound, string exitSound)
	{
		this.m_entrySound = entrySound;
		this.m_landingSound = landingSound;
		this.m_exitSound = exitSound;
	}

	// Token: 0x0600650D RID: 25869 RVA: 0x0020FAC4 File Offset: 0x0020DCC4
	private void PlayEntrySound()
	{
		if (!string.IsNullOrEmpty(this.m_entrySound))
		{
			SoundManager.Get().LoadAndPlay(this.m_entrySound);
		}
	}

	// Token: 0x0600650E RID: 25870 RVA: 0x0020FAE8 File Offset: 0x0020DCE8
	private void PlayLandingSound()
	{
		if (!string.IsNullOrEmpty(this.m_landingSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_landingSound);
		}
	}

	// Token: 0x0600650F RID: 25871 RVA: 0x0020FB0C File Offset: 0x0020DD0C
	private void PlayExitSound()
	{
		if (!string.IsNullOrEmpty(this.m_exitSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_exitSound);
		}
	}

	// Token: 0x040053DF RID: 21471
	private string m_entrySound;

	// Token: 0x040053E0 RID: 21472
	private string m_landingSound;

	// Token: 0x040053E1 RID: 21473
	private string m_exitSound;
}
