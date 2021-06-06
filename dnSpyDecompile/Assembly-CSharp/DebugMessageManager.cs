using System;
using PegasusGame;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class DebugMessageManager : MonoBehaviour
{
	// Token: 0x060021AB RID: 8619 RVA: 0x000A58CC File Offset: 0x000A3ACC
	public static DebugMessageManager Get()
	{
		if (DebugMessageManager.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			DebugMessageManager.s_instance = gameObject.AddComponent<DebugMessageManager>();
			gameObject.name = "DebugMessageManager (Dynamically created)";
		}
		return DebugMessageManager.s_instance;
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x000A58FA File Offset: 0x000A3AFA
	public void OnDebugMessage(DebugMessage debugMessage)
	{
		Log.Gameplay.PrintAndForcePrintToScreen(Log.LogLevel.Info, false, debugMessage.Message);
	}

	// Token: 0x0400129F RID: 4767
	private static DebugMessageManager s_instance;
}
