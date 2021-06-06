using System.Collections.Generic;
using System.Linq;
using bgs;
using UnityEngine;

public class FriendListChatIcon : MonoBehaviour
{
	private BnetPlayer m_player;

	public BnetPlayer GetPlayer()
	{
		return m_player;
	}

	public bool SetPlayer(BnetPlayer player)
	{
		if (m_player == player)
		{
			return false;
		}
		m_player = player;
		UpdateIcon();
		return true;
	}

	public void UpdateIcon()
	{
		if (m_player == null)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(m_player);
		if (whispersWithPlayer == null)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		if (WhisperUtil.IsSpeaker(BnetPresenceMgr.Get().GetMyPlayer(), whispersWithPlayer[whispersWithPlayer.Count - 1]))
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		PlayerChatInfo playerChatInfo = ChatMgr.Get().GetPlayerChatInfo(m_player);
		if (playerChatInfo != null && whispersWithPlayer.LastOrDefault((BnetWhisper whisper) => WhisperUtil.IsSpeaker(m_player, whisper)) == playerChatInfo.GetLastSeenWhisper())
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.SetActive(value: true);
		}
	}
}
