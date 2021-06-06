using System;
using UnityEngine;

// Token: 0x020009BC RID: 2492
public class InputUtil
{
	// Token: 0x0600882B RID: 34859 RVA: 0x002BDD54 File Offset: 0x002BBF54
	public static InputScheme GetInputScheme()
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
		{
			return InputScheme.TOUCH;
		}
		return InputScheme.KEYBOARD_MOUSE;
	}

	// Token: 0x0600882C RID: 34860 RVA: 0x002BDD74 File Offset: 0x002BBF74
	public static bool IsMouseOnScreen()
	{
		if (UniversalInputManager.Get() == null)
		{
			return false;
		}
		Vector3 mousePosition = UniversalInputManager.Get().GetMousePosition();
		return mousePosition.x >= 0f && mousePosition.x <= (float)Screen.width && mousePosition.y >= 0f && mousePosition.y <= (float)Screen.height;
	}

	// Token: 0x0600882D RID: 34861 RVA: 0x002BDDD0 File Offset: 0x002BBFD0
	public static bool IsPlayMakerMouseInputAllowed(GameObject go)
	{
		if (UniversalInputManager.Get() == null)
		{
			return false;
		}
		if (InputUtil.ShouldCheckGameplayForPlayMakerMouseInput(go))
		{
			GameState gameState = GameState.Get();
			if (gameState != null && gameState.IsMulliganManagerActive())
			{
				return false;
			}
			TargetReticleManager targetReticleManager = TargetReticleManager.Get();
			if (targetReticleManager != null && targetReticleManager.IsLocalArrowActive())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600882E RID: 34862 RVA: 0x002BDE1C File Offset: 0x002BC01C
	private static bool ShouldCheckGameplayForPlayMakerMouseInput(GameObject go)
	{
		return SceneMgr.Get() != null && SceneMgr.Get().IsInGame() && (!(LoadingScreen.Get() != null) || !LoadingScreen.Get().IsPreviousSceneActive() || !(SceneUtils.FindComponentInThisOrParents<LoadingScreen>(go) != null)) && !(SceneUtils.FindComponentInThisOrParents<BaseUI>(go) != null);
	}
}
