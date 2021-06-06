using HutongGames.PlayMaker;
using UnityEngine;

public static class PlayMakerUtils
{
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
			Debug.LogWarning($"PlayMakerUtils.DetermineAnimData() - GameObject {go} is missing an animation component");
			return false;
		}
		animName = DetermineAnimName(animNameScreens, animNames, defaultAnimName);
		if (string.IsNullOrEmpty(animName))
		{
			Debug.LogWarning($"PlayMakerUtils.DetermineAnimData() - GameObject {go} has an animation action with no animation name");
			return false;
		}
		animState = go.GetComponent<Animation>()[animName];
		if (animState == null)
		{
			Debug.LogWarning($"PlayMakerUtils.DetermineAnimData() - GameObject {go} is missing animation {animName}");
			return false;
		}
		return true;
	}

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
