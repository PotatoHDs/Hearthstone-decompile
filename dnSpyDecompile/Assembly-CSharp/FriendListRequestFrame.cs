using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class FriendListRequestFrame : MonoBehaviour
{
	// Token: 0x0600095A RID: 2394 RVA: 0x00036F0C File Offset: 0x0003510C
	private void Awake()
	{
		this.m_AcceptButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnAcceptButtonPressed));
		this.m_DeclineButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeclineButtonPressed));
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00036F40 File Offset: 0x00035140
	private void Update()
	{
		this.UpdateTimeText();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00036F48 File Offset: 0x00035148
	private void OnEnable()
	{
		this.UpdateInvite();
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00036F50 File Offset: 0x00035150
	public BnetInvitation GetInvite()
	{
		return this.m_invite;
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00036F58 File Offset: 0x00035158
	public void SetInvite(BnetInvitation invite)
	{
		if (this.m_invite == invite)
		{
			return;
		}
		this.m_invite = invite;
		this.UpdateInvite();
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00036F76 File Offset: 0x00035176
	public void UpdateInvite()
	{
		if (!base.gameObject.activeSelf || this.m_invite == null)
		{
			return;
		}
		this.m_PlayerNameText.Text = this.m_invite.GetInviterName();
		this.UpdateTimeText();
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00036FB0 File Offset: 0x000351B0
	private void UpdateTimeText()
	{
		string requestElapsedTimeString = FriendUtils.GetRequestElapsedTimeString((long)this.m_invite.GetCreationTimeMicrosec());
		this.m_TimeText.Text = GameStrings.Format("GLOBAL_FRIENDLIST_REQUEST_SENT_TIME", new object[]
		{
			requestElapsedTimeString
		});
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00036FED File Offset: 0x000351ED
	private void OnAcceptButtonPressed(UIEvent e)
	{
		BnetFriendMgr.Get().AcceptInvite(this.m_invite.GetId());
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00037004 File Offset: 0x00035204
	private void OnDeclineButtonPressed(UIEvent e)
	{
		BnetFriendMgr.Get().DeclineInvite(this.m_invite.GetId());
	}

	// Token: 0x0400064C RID: 1612
	public GameObject m_Background;

	// Token: 0x0400064D RID: 1613
	public FriendListUIElement m_AcceptButton;

	// Token: 0x0400064E RID: 1614
	public FriendListUIElement m_DeclineButton;

	// Token: 0x0400064F RID: 1615
	public UberText m_PlayerNameText;

	// Token: 0x04000650 RID: 1616
	public UberText m_TimeText;

	// Token: 0x04000651 RID: 1617
	private BnetInvitation m_invite;
}
