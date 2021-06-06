using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
public static class GameplayDebug
{
	// Token: 0x06002AC4 RID: 10948 RVA: 0x000D7949 File Offset: 0x000D5B49
	private static string FormatStringWithErrorThreshold(float input, int errorValue)
	{
		return string.Format("{0}{1}{2}", ((int)input >= errorValue) ? "<color=red>" : "", input, ((int)input >= errorValue) ? "</color>" : "");
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x000D797D File Offset: 0x000D5B7D
	public static Rect LayoutUI(Rect space)
	{
		GameState.Get();
		return space;
	}

	// Token: 0x040017F1 RID: 6129
	public static int LOST_SLUSH_TIME_ERROR_THRESHOLD_SECONDS = 1;

	// Token: 0x040017F2 RID: 6130
	public static int LOST_FRAME_TIME_ERROR_THRESHOLD_SECONDS = 5;
}
