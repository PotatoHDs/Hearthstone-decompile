using UnityEngine;

public static class GameplayDebug
{
	public static int LOST_SLUSH_TIME_ERROR_THRESHOLD_SECONDS = 1;

	public static int LOST_FRAME_TIME_ERROR_THRESHOLD_SECONDS = 5;

	private static string FormatStringWithErrorThreshold(float input, int errorValue)
	{
		return string.Format("{0}{1}{2}", ((int)input >= errorValue) ? "<color=red>" : "", input, ((int)input >= errorValue) ? "</color>" : "");
	}

	public static Rect LayoutUI(Rect space)
	{
		GameState.Get();
		return space;
	}
}
