using System;
using UnityEngine;

// Token: 0x02000748 RID: 1864
public class TutorialLesson5 : MonoBehaviour
{
	// Token: 0x0600696D RID: 26989 RVA: 0x00225BBC File Offset: 0x00223DBC
	private void Awake()
	{
		this.m_heroPower.SetGameStringText("GLOBAL_TUTORIAL_HERO_POWER");
		this.m_used.SetGameStringText("GLOBAL_TUTORIAL_USED");
		this.m_yourTurn.SetGameStringText("GLOBAL_TUTORIAL_YOUR_TURN");
	}

	// Token: 0x04005646 RID: 22086
	public UberText m_heroPower;

	// Token: 0x04005647 RID: 22087
	public UberText m_used;

	// Token: 0x04005648 RID: 22088
	public UberText m_yourTurn;
}
