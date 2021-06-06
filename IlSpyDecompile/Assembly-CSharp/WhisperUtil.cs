using bgs;

public static class WhisperUtil
{
	public static BnetPlayer GetSpeaker(BnetWhisper whisper)
	{
		return BnetUtils.GetPlayer(whisper.GetSpeakerId());
	}

	public static BnetPlayer GetReceiver(BnetWhisper whisper)
	{
		return BnetUtils.GetPlayer(whisper.GetReceiverId());
	}

	public static BnetPlayer GetTheirPlayer(BnetWhisper whisper)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null)
		{
			return null;
		}
		BnetPlayer speaker = GetSpeaker(whisper);
		BnetPlayer receiver = GetReceiver(whisper);
		if (myPlayer == speaker)
		{
			return receiver;
		}
		if (myPlayer == receiver)
		{
			return speaker;
		}
		return null;
	}

	public static BnetGameAccountId GetTheirGameAccountId(BnetWhisper whisper)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null)
		{
			return null;
		}
		if (myPlayer.HasGameAccount(whisper.GetSpeakerId()))
		{
			return whisper.GetReceiverId();
		}
		if (myPlayer.HasGameAccount(whisper.GetReceiverId()))
		{
			return whisper.GetSpeakerId();
		}
		return null;
	}

	public static bool IsDisplayable(BnetWhisper whisper)
	{
		BnetPlayer speaker = GetSpeaker(whisper);
		BnetPlayer receiver = GetReceiver(whisper);
		if (speaker == null)
		{
			return false;
		}
		if (!speaker.IsDisplayable())
		{
			return false;
		}
		if (receiver == null)
		{
			return false;
		}
		if (!receiver.IsDisplayable())
		{
			return false;
		}
		return true;
	}

	public static bool IsSpeaker(BnetPlayer player, BnetWhisper whisper)
	{
		return player?.HasGameAccount(whisper.GetSpeakerId()) ?? false;
	}

	public static bool IsReceiver(BnetPlayer player, BnetWhisper whisper)
	{
		return player?.HasGameAccount(whisper.GetReceiverId()) ?? false;
	}

	public static bool IsSpeakerOrReceiver(BnetPlayer player, BnetWhisper whisper)
	{
		if (player == null)
		{
			return false;
		}
		if (!player.HasGameAccount(whisper.GetSpeakerId()))
		{
			return player.HasGameAccount(whisper.GetReceiverId());
		}
		return true;
	}
}
