using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class FriendListChatIcon : MonoBehaviour
{
	// Token: 0x06000866 RID: 2150 RVA: 0x000318C8 File Offset: 0x0002FAC8
	public BnetPlayer GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x000318D0 File Offset: 0x0002FAD0
	public bool SetPlayer(BnetPlayer player)
	{
		if (this.m_player == player)
		{
			return false;
		}
		this.m_player = player;
		this.UpdateIcon();
		return true;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000318EC File Offset: 0x0002FAEC
	public void UpdateIcon()
	{
		if (this.m_player == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(this.m_player);
		if (whispersWithPlayer == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (WhisperUtil.IsSpeaker(BnetPresenceMgr.Get().GetMyPlayer(), whispersWithPlayer[whispersWithPlayer.Count - 1]))
		{
			base.gameObject.SetActive(false);
			return;
		}
		PlayerChatInfo playerChatInfo = ChatMgr.Get().GetPlayerChatInfo(this.m_player);
		if (playerChatInfo != null && whispersWithPlayer.LastOrDefault((BnetWhisper whisper) => WhisperUtil.IsSpeaker(this.m_player, whisper)) == playerChatInfo.GetLastSeenWhisper())
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x040005C3 RID: 1475
	private BnetPlayer m_player;
}
