using System;
using bgs;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class ChatBubbleFrame : MonoBehaviour
{
	// Token: 0x060006E2 RID: 1762 RVA: 0x00027E7F File Offset: 0x0002607F
	private void Awake()
	{
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00027E98 File Offset: 0x00026098
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00027EB1 File Offset: 0x000260B1
	public BnetWhisper GetWhisper()
	{
		return this.m_whisper;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00027EB9 File Offset: 0x000260B9
	public void SetWhisper(BnetWhisper whisper)
	{
		if (this.m_whisper == whisper)
		{
			return;
		}
		this.m_whisper = whisper;
		this.UpdateWhisper();
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00027ED2 File Offset: 0x000260D2
	public bool DoesMessageFit()
	{
		return !this.m_MessageText.IsEllipsized();
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00027EE4 File Offset: 0x000260E4
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(WhisperUtil.GetTheirGameAccountId(this.m_whisper));
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(player);
		if (bnetPlayerChange == null)
		{
			return;
		}
		BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
		BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
		if (oldPlayer != null && oldPlayer.IsOnline() == newPlayer.IsOnline())
		{
			return;
		}
		this.UpdateWhisper();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00027F3C File Offset: 0x0002613C
	private void UpdateWhisper()
	{
		if (this.m_whisper == null)
		{
			return;
		}
		if (this.m_whisper.GetSpeakerId() == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			this.m_MyDecoration.SetActive(true);
			this.m_TheirDecoration.SetActive(false);
			BnetPlayer receiver = WhisperUtil.GetReceiver(this.m_whisper);
			this.m_NameText.Text = GameStrings.Format("GLOBAL_CHAT_BUBBLE_RECEIVER_NAME", new object[]
			{
				receiver.GetBestName()
			});
		}
		else
		{
			this.m_MyDecoration.SetActive(false);
			this.m_TheirDecoration.SetActive(true);
			BnetPlayer speaker = WhisperUtil.GetSpeaker(this.m_whisper);
			if (speaker.IsOnline())
			{
				this.m_NameText.TextColor = GameColors.PLAYER_NAME_ONLINE;
			}
			else
			{
				this.m_NameText.TextColor = GameColors.PLAYER_NAME_OFFLINE;
			}
			this.m_NameText.Text = speaker.GetBestName();
		}
		this.m_MessageText.Text = ChatUtils.GetMessage(this.m_whisper);
		UberText messageText = this.m_MessageText;
		messageText.Text += " ";
	}

	// Token: 0x040004CC RID: 1228
	public GameObject m_VisualRoot;

	// Token: 0x040004CD RID: 1229
	public GameObject m_MyDecoration;

	// Token: 0x040004CE RID: 1230
	public GameObject m_TheirDecoration;

	// Token: 0x040004CF RID: 1231
	public UberText m_NameText;

	// Token: 0x040004D0 RID: 1232
	public UberText m_MessageText;

	// Token: 0x040004D1 RID: 1233
	public Vector3_MobileOverride m_ScaleOverride;

	// Token: 0x040004D2 RID: 1234
	private BnetWhisper m_whisper;
}
