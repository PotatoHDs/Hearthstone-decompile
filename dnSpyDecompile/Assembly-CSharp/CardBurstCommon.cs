using System;
using UnityEngine;

// Token: 0x020007D3 RID: 2003
public class CardBurstCommon : Spell
{
	// Token: 0x06006E19 RID: 28185 RVA: 0x00237E71 File Offset: 0x00236071
	protected override void OnBirth(SpellStateType prevStateType)
	{
		if (this.m_BurstMotes)
		{
			this.m_BurstMotes.Play();
		}
		if (this.m_EdgeGlow)
		{
			this.m_EdgeGlow.GetComponent<Renderer>().enabled = true;
		}
		this.OnSpellFinished();
	}

	// Token: 0x04005844 RID: 22596
	public ParticleSystem m_BurstMotes;

	// Token: 0x04005845 RID: 22597
	public GameObject m_EdgeGlow;
}
