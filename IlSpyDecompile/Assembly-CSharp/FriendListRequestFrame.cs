using UnityEngine;

public class FriendListRequestFrame : MonoBehaviour
{
	public GameObject m_Background;

	public FriendListUIElement m_AcceptButton;

	public FriendListUIElement m_DeclineButton;

	public UberText m_PlayerNameText;

	public UberText m_TimeText;

	private BnetInvitation m_invite;

	private void Awake()
	{
		m_AcceptButton.AddEventListener(UIEventType.RELEASE, OnAcceptButtonPressed);
		m_DeclineButton.AddEventListener(UIEventType.RELEASE, OnDeclineButtonPressed);
	}

	private void Update()
	{
		UpdateTimeText();
	}

	private void OnEnable()
	{
		UpdateInvite();
	}

	public BnetInvitation GetInvite()
	{
		return m_invite;
	}

	public void SetInvite(BnetInvitation invite)
	{
		if (!(m_invite == invite))
		{
			m_invite = invite;
			UpdateInvite();
		}
	}

	public void UpdateInvite()
	{
		if (base.gameObject.activeSelf && !(m_invite == null))
		{
			m_PlayerNameText.Text = m_invite.GetInviterName();
			UpdateTimeText();
		}
	}

	private void UpdateTimeText()
	{
		string requestElapsedTimeString = FriendUtils.GetRequestElapsedTimeString((long)m_invite.GetCreationTimeMicrosec());
		m_TimeText.Text = GameStrings.Format("GLOBAL_FRIENDLIST_REQUEST_SENT_TIME", requestElapsedTimeString);
	}

	private void OnAcceptButtonPressed(UIEvent e)
	{
		BnetFriendMgr.Get().AcceptInvite(m_invite.GetId());
	}

	private void OnDeclineButtonPressed(UIEvent e)
	{
		BnetFriendMgr.Get().DeclineInvite(m_invite.GetId());
	}
}
