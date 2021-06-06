using System;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x020009DF RID: 2527
public static class PlayMakerUtils
{
	// Token: 0x06008923 RID: 35107 RVA: 0x002C1884 File Offset: 0x002BFA84
	public static bool DetermineAnimData(GameObject go, ScreenCategory[] animNameScreens, FsmString[] animNames, FsmString defaultAnimName, out string animName, out AnimationState animState)
	{
		animName = null;
		animState = null;
		if (go == null)
		{
			return false;
		}
		if (go.GetComponent<Animation>() == null)
		{
			Debug.LogWarning(string.Format("PlayMakerUtils.DetermineAnimData() - GameObject {0} is missing an animation component", go));
			return false;
		}
		animName = PlayMakerUtils.DetermineAnimName(animNameScreens, animNames, defaultAnimName);
		if (string.IsNullOrEmpty(animName))
		{
			Debug.LogWarning(string.Format("PlayMakerUtils.DetermineAnimData() - GameObject {0} has an animation action with no animation name", go));
			return false;
		}
		animState = go.GetComponent<Animation>()[animName];
		if (animState == null)
		{
			Debug.LogWarning(string.Format("PlayMakerUtils.DetermineAnimData() - GameObject {0} is missing animation {1}", go, animName));
			return false;
		}
		return true;
	}

	// Token: 0x06008924 RID: 35108 RVA: 0x002C1920 File Offset: 0x002BFB20
	public static string DetermineAnimName(ScreenCategory[] animNameScreens, FsmString[] animNames, FsmString defaultAnimName)
	{
		if (animNameScreens != null)
		{
			for (int i = 0; i < animNameScreens.Length; i++)
			{
				if (animNameScreens[i] == PlatformSettings.Screen)
				{
					return animNames[i].Value;
				}
			}
		}
		return defaultAnimName.Value;
	}
}
