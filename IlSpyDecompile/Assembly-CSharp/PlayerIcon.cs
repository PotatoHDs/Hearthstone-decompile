using bgs;
using UnityEngine;

public class PlayerIcon : PegUIElement
{
	public GameObject m_OfflineIcon;

	public GameObject m_OnlineIcon;

	public PlayerPortrait m_OnlinePortrait;

	private bool m_hidden;

	private BnetPlayer m_player;

	public void Hide()
	{
		m_hidden = true;
		base.gameObject.SetActive(value: false);
	}

	public void Show()
	{
		m_hidden = false;
		base.gameObject.SetActive(value: true);
	}

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
			return;
		}
		BnetProgramId bestProgramId = m_player.GetBestProgramId();
		bool flag = false;
		if (bestProgramId != null)
		{
			flag = bestProgramId.IsGame();
		}
		if (m_player.IsOnline() && flag)
		{
			if (!m_hidden)
			{
				base.gameObject.SetActive(value: true);
			}
			m_OnlinePortrait.SetProgramId(bestProgramId);
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
