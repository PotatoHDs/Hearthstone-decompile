using System;
using UnityEngine;

// Token: 0x0200096C RID: 2412
public class Spell_SuppressPlayAudio : Spell
{
	// Token: 0x0600850D RID: 34061 RVA: 0x002AFC20 File Offset: 0x002ADE20
	public override void SetSource(GameObject go)
	{
		this.m_source = go;
		if (this.m_source == null)
		{
			return;
		}
		Card component = this.m_source.GetComponent<Card>();
		if (component != null)
		{
			component.SuppressPlaySounds(true);
		}
	}
}
