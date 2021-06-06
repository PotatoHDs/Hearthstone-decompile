using System;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class TutorialLesson4 : MonoBehaviour
{
	// Token: 0x0600696B RID: 26987 RVA: 0x00225B8A File Offset: 0x00223D8A
	private void Awake()
	{
		this.m_tauntDescriptionTitle.SetGameStringText("GLOBAL_TUTORIAL_TAUNT");
		this.m_tauntDescription.SetGameStringText("GLOBAL_TUTORIAL_TAUNT_DESCRIPTION");
		this.m_taunt.SetGameStringText("GLOBAL_TUTORIAL_TAUNT");
	}

	// Token: 0x04005643 RID: 22083
	public UberText m_tauntDescriptionTitle;

	// Token: 0x04005644 RID: 22084
	public UberText m_tauntDescription;

	// Token: 0x04005645 RID: 22085
	public UberText m_taunt;
}
