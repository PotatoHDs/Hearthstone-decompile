using System;
using UnityEngine;

// Token: 0x02000744 RID: 1860
public class TutorialLesson1 : MonoBehaviour
{
	// Token: 0x06006965 RID: 26981 RVA: 0x00225B14 File Offset: 0x00223D14
	private void Awake()
	{
		this.m_health.SetGameStringText("GLOBAL_TUTORIAL_HEALTH");
		this.m_attack.SetGameStringText("GLOBAL_TUTORIAL_ATTACK");
		this.m_minion.SetGameStringText("GLOBAL_TUTORIAL_MINION");
	}

	// Token: 0x0400563C RID: 22076
	public UberText m_health;

	// Token: 0x0400563D RID: 22077
	public UberText m_attack;

	// Token: 0x0400563E RID: 22078
	public UberText m_minion;
}
