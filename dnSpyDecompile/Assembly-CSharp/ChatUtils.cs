using System;
using bgs;

// Token: 0x02000082 RID: 130
public static class ChatUtils
{
	// Token: 0x0600077D RID: 1917 RVA: 0x0002AAC8 File Offset: 0x00028CC8
	public static string GetMessage(BnetWhisper whisper)
	{
		return ChatUtils.GetMessage(whisper.GetMessage());
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0002AAD5 File Offset: 0x00028CD5
	public static string GetMessage(string message)
	{
		if (Localization.GetLocale() == Locale.zhCN)
		{
			return BattleNet.FilterProfanity(message);
		}
		return message;
	}

	// Token: 0x04000516 RID: 1302
	public const int MAX_INPUT_CHARACTERS = 512;

	// Token: 0x04000517 RID: 1303
	public const int MAX_RECENT_WHISPER_RECEIVERS = 10;

	// Token: 0x04000518 RID: 1304
	public const float FRIENDLIST_CHATICON_INACTIVE_SEC = 10f;
}
