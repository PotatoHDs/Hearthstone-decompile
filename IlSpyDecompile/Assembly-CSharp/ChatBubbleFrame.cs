using bgs;
using UnityEngine;

public class ChatBubbleFrame : MonoBehaviour
{
	public GameObject m_VisualRoot;

	public GameObject m_MyDecoration;

	public GameObject m_TheirDecoration;

	public UberText m_NameText;

	public UberText m_MessageText;

	public Vector3_MobileOverride m_ScaleOverride;

	private BnetWhisper m_whisper;

	private void Awake()
	{
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
	}

	public BnetWhisper GetWhisper()
	{
		return m_whisper;
	}

	public void SetWhisper(BnetWhisper whisper)
	{
		if (m_whisper != whisper)
		{
			m_whisper = whisper;
			UpdateWhisper();
		}
	}

	public bool DoesMessageFit()
	{
		return !m_MessageText.IsEllipsized();
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(WhisperUtil.GetTheirGameAccountId(m_whisper));
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(player);
		if (bnetPlayerChange != null)
		{
			BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
			BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
			if (oldPlayer == null || oldPlayer.IsOnline() != newPlayer.IsOnline())
			{
				UpdateWhisper();
			}
		}
	}

	private void UpdateWhisper()
	{
		if (m_whisper == null)
		{
			return;
		}
		if (m_whisper.GetSpeakerId() == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			m_MyDecoration.SetActive(value: true);
			m_TheirDecoration.SetActive(value: false);
			BnetPlayer receiver = WhisperUtil.GetReceiver(m_whisper);
			m_NameText.Text = GameStrings.Format("GLOBAL_CHAT_BUBBLE_RECEIVER_NAME", receiver.GetBestName());
		}
		else
		{
			m_MyDecoration.SetActive(value: false);
			m_TheirDecoration.SetActive(value: true);
			BnetPlayer speaker = WhisperUtil.GetSpeaker(m_whisper);
			if (speaker.IsOnline())
			{
				m_NameText.TextColor = GameColors.PLAYER_NAME_ONLINE;
			}
			else
			{
				m_NameText.TextColor = GameColors.PLAYER_NAME_OFFLINE;
			}
			m_NameText.Text = speaker.GetBestName();
		}
		m_MessageText.Text = ChatUtils.GetMessage(m_whisper);
		m_MessageText.Text += " ";
	}
}
