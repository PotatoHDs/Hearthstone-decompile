using UnityEngine;

public class Tournament
{
	private static Tournament s_instance;

	public static void Init()
	{
		if (s_instance == null)
		{
			s_instance = new Tournament();
		}
	}

	public static Tournament Get()
	{
		if (s_instance == null)
		{
			Debug.LogError("Trying to retrieve the Tournament without calling Tournament.Init()!");
		}
		return s_instance;
	}

	public void NotifyOfBoxTransitionStart()
	{
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
	}

	public void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		if (!Options.Get().GetBool(Option.HAS_SEEN_TOURNAMENT, defaultVal: false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_TOURNAMENT, val: true);
		}
	}
}
