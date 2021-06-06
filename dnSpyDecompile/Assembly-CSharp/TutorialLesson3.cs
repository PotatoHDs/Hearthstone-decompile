using System;
using UnityEngine;

// Token: 0x02000746 RID: 1862
public class TutorialLesson3 : MonoBehaviour
{
	// Token: 0x06006969 RID: 26985 RVA: 0x00225B68 File Offset: 0x00223D68
	private void Awake()
	{
		this.m_attacker.SetGameStringText("GLOBAL_TUTORIAL_ATTACKER");
		this.m_defender.SetGameStringText("GLOBAL_TUTORIAL_DEFENDER");
	}

	// Token: 0x04005641 RID: 22081
	public UberText m_attacker;

	// Token: 0x04005642 RID: 22082
	public UberText m_defender;
}
