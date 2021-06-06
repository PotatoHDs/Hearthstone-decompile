using System;
using UnityEngine;

// Token: 0x02000745 RID: 1861
public class TutorialLesson2 : MonoBehaviour
{
	// Token: 0x06006967 RID: 26983 RVA: 0x00225B46 File Offset: 0x00223D46
	private void Awake()
	{
		this.m_cost.SetGameStringText("GLOBAL_TUTORIAL_COST");
		this.m_yourMana.SetGameStringText("GLOBAL_TUTORIAL_YOUR_MANA");
	}

	// Token: 0x0400563F RID: 22079
	public UberText m_cost;

	// Token: 0x04005640 RID: 22080
	public UberText m_yourMana;
}
