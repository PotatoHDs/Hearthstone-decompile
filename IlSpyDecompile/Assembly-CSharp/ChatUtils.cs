using bgs;

public static class ChatUtils
{
	public const int MAX_INPUT_CHARACTERS = 512;

	public const int MAX_RECENT_WHISPER_RECEIVERS = 10;

	public const float FRIENDLIST_CHATICON_INACTIVE_SEC = 10f;

	public static string GetMessage(BnetWhisper whisper)
	{
		return GetMessage(whisper.GetMessage());
	}

	public static string GetMessage(string message)
	{
		if (Localization.GetLocale() == Locale.zhCN)
		{
			return BattleNet.FilterProfanity(message);
		}
		return message;
	}
}
