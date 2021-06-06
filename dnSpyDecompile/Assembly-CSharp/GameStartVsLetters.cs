using System;
using UnityEngine;

// Token: 0x020008C8 RID: 2248
public class GameStartVsLetters : MonoBehaviour
{
	// Token: 0x06007C41 RID: 31809 RVA: 0x00286595 File Offset: 0x00284795
	private void Awake()
	{
		this.m_anim = base.GetComponentInChildren<Animation>();
		if (this.m_anim == null)
		{
			Log.All.PrintError("GameStartVsLetters.Awake(): No Animator component found in children.", Array.Empty<object>());
		}
	}

	// Token: 0x06007C42 RID: 31810 RVA: 0x002865C8 File Offset: 0x002847C8
	public void FadeIn()
	{
		if (this.m_anim != null)
		{
			this.m_anim[this.m_fadeAnimName].speed = -1f;
			this.m_anim[this.m_fadeAnimName].time = 1f;
			this.m_anim.Play(this.m_fadeAnimName);
		}
	}

	// Token: 0x06007C43 RID: 31811 RVA: 0x0028662C File Offset: 0x0028482C
	public void FadeOut()
	{
		if (this.m_anim != null)
		{
			this.m_anim[this.m_fadeAnimName].speed = 1f;
			this.m_anim[this.m_fadeAnimName].time = 0f;
			this.m_anim.Play(this.m_fadeAnimName);
		}
	}

	// Token: 0x04006540 RID: 25920
	public string m_fadeAnimName;

	// Token: 0x04006541 RID: 25921
	private Animation m_anim;
}
