using System;
using bgs;

// Token: 0x02000774 RID: 1908
public static class WhisperUtil
{
	// Token: 0x06006BDF RID: 27615 RVA: 0x0022F610 File Offset: 0x0022D810
	public static BnetPlayer GetSpeaker(BnetWhisper whisper)
	{
		return BnetUtils.GetPlayer(whisper.GetSpeakerId());
	}

	// Token: 0x06006BE0 RID: 27616 RVA: 0x0022F61D File Offset: 0x0022D81D
	public static BnetPlayer GetReceiver(BnetWhisper whisper)
	{
		return BnetUtils.GetPlayer(whisper.GetReceiverId());
	}

	// Token: 0x06006BE1 RID: 27617 RVA: 0x0022F62C File Offset: 0x0022D82C
	public static BnetPlayer GetTheirPlayer(BnetWhisper whisper)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null)
		{
			return null;
		}
		BnetPlayer speaker = WhisperUtil.GetSpeaker(whisper);
		BnetPlayer receiver = WhisperUtil.GetReceiver(whisper);
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

	// Token: 0x06006BE2 RID: 27618 RVA: 0x0022F664 File Offset: 0x0022D864
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

	// Token: 0x06006BE3 RID: 27619 RVA: 0x0022F6AC File Offset: 0x0022D8AC
	public static bool IsDisplayable(BnetWhisper whisper)
	{
		BnetPlayer speaker = WhisperUtil.GetSpeaker(whisper);
		BnetPlayer receiver = WhisperUtil.GetReceiver(whisper);
		return speaker != null && speaker.IsDisplayable() && receiver != null && receiver.IsDisplayable();
	}

	// Token: 0x06006BE4 RID: 27620 RVA: 0x0022F6E6 File Offset: 0x0022D8E6
	public static bool IsSpeaker(BnetPlayer player, BnetWhisper whisper)
	{
		return player != null && player.HasGameAccount(whisper.GetSpeakerId());
	}

	// Token: 0x06006BE5 RID: 27621 RVA: 0x0022F6F9 File Offset: 0x0022D8F9
	public static bool IsReceiver(BnetPlayer player, BnetWhisper whisper)
	{
		return player != null && player.HasGameAccount(whisper.GetReceiverId());
	}

	// Token: 0x06006BE6 RID: 27622 RVA: 0x0022F70C File Offset: 0x0022D90C
	public static bool IsSpeakerOrReceiver(BnetPlayer player, BnetWhisper whisper)
	{
		return player != null && (player.HasGameAccount(whisper.GetSpeakerId()) || player.HasGameAccount(whisper.GetReceiverId()));
	}
}
