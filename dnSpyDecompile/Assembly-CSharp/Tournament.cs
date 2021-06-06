using System;
using UnityEngine;

// Token: 0x02000740 RID: 1856
public class Tournament
{
	// Token: 0x06006931 RID: 26929 RVA: 0x00224A12 File Offset: 0x00222C12
	public static void Init()
	{
		if (Tournament.s_instance != null)
		{
			return;
		}
		Tournament.s_instance = new Tournament();
	}

	// Token: 0x06006932 RID: 26930 RVA: 0x00224A26 File Offset: 0x00222C26
	public static Tournament Get()
	{
		if (Tournament.s_instance == null)
		{
			Debug.LogError("Trying to retrieve the Tournament without calling Tournament.Init()!");
		}
		return Tournament.s_instance;
	}

	// Token: 0x06006933 RID: 26931 RVA: 0x00224A3E File Offset: 0x00222C3E
	public void NotifyOfBoxTransitionStart()
	{
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
	}

	// Token: 0x06006934 RID: 26932 RVA: 0x00224A56 File Offset: 0x00222C56
	public void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		if (!Options.Get().GetBool(Option.HAS_SEEN_TOURNAMENT, false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_TOURNAMENT, true);
		}
	}

	// Token: 0x04005611 RID: 22033
	private static Tournament s_instance;
}
