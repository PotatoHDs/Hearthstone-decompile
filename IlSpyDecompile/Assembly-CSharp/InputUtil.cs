using UnityEngine;

public class InputUtil
{
	public static InputScheme GetInputScheme()
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
		{
			return InputScheme.TOUCH;
		}
		return InputScheme.KEYBOARD_MOUSE;
	}

	public static bool IsMouseOnScreen()
	{
		if (UniversalInputManager.Get() == null)
		{
			return false;
		}
		Vector3 mousePosition = UniversalInputManager.Get().GetMousePosition();
		if (mousePosition.x >= 0f && mousePosition.x <= (float)Screen.width && mousePosition.y >= 0f)
		{
			return mousePosition.y <= (float)Screen.height;
		}
		return false;
	}

	public static bool IsPlayMakerMouseInputAllowed(GameObject go)
	{
		if (UniversalInputManager.Get() == null)
		{
			return false;
		}
		if (ShouldCheckGameplayForPlayMakerMouseInput(go))
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

	private static bool ShouldCheckGameplayForPlayMakerMouseInput(GameObject go)
	{
		if (SceneMgr.Get() == null)
		{
			return false;
		}
		if (!SceneMgr.Get().IsInGame())
		{
			return false;
		}
		if (LoadingScreen.Get() != null && LoadingScreen.Get().IsPreviousSceneActive() && SceneUtils.FindComponentInThisOrParents<LoadingScreen>(go) != null)
		{
			return false;
		}
		if (SceneUtils.FindComponentInThisOrParents<BaseUI>(go) != null)
		{
			return false;
		}
		return true;
	}
}
