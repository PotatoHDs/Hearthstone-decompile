using System.Collections;
using Hearthstone;

public class Reset : PegasusScene
{
	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
		StartCoroutine("WaitThenReset");
	}

	private IEnumerator WaitThenReset()
	{
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		HearthstoneApplication.Get().Reset();
	}
}
