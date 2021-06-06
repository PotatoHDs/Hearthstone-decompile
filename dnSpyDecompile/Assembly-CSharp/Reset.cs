using System;
using System.Collections;
using Hearthstone;

// Token: 0x02000652 RID: 1618
public class Reset : PegasusScene
{
	// Token: 0x06005B86 RID: 23430 RVA: 0x001DD54B File Offset: 0x001DB74B
	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
		base.StartCoroutine("WaitThenReset");
	}

	// Token: 0x06005B87 RID: 23431 RVA: 0x001DD563 File Offset: 0x001DB763
	private IEnumerator WaitThenReset()
	{
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		HearthstoneApplication.Get().Reset();
		yield break;
	}
}
