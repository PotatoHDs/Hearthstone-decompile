using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class ChatLogFrame : MonoBehaviour
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x00028B1C File Offset: 0x00026D1C
	// (set) Token: 0x06000719 RID: 1817 RVA: 0x00028B24 File Offset: 0x00026D24
	public BnetPlayer Receiver
	{
		get
		{
			return this.m_receiver;
		}
		set
		{
			if (this.m_receiver == value)
			{
				return;
			}
			this.m_receiver = value;
			if (this.m_receiver != null)
			{
				this.m_playerIcon.SetPlayer(this.m_receiver);
				this.UpdateReceiver();
				this.m_chatLog.Receiver = this.m_receiver;
			}
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x00028B74 File Offset: 0x00026D74
	public bool IsWaitingOnMedal
	{
		get
		{
			if (this.Receiver == null)
			{
				return true;
			}
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.Receiver.GetBestGameAccount());
			return rankPresenceField != null && rankPresenceField.IsDisplayable() && (this.m_rankedMedalWidget == null || !this.m_rankedMedal.IsReady);
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00028BCC File Offset: 0x00026DCC
	private void Awake()
	{
		this.InitPlayerIcon();
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00028BEB File Offset: 0x00026DEB
	private void Start()
	{
		this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
		this.UpdateLayout();
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00028C0A File Offset: 0x00026E0A
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00028C23 File Offset: 0x00026E23
	public void UpdateLayout()
	{
		this.OnResize();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00028C2B File Offset: 0x00026E2B
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(this.m_receiver) == null)
		{
			return;
		}
		this.UpdateReceiver();
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00028C42 File Offset: 0x00026E42
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		this.m_rankedMedalWidget = widget;
		this.m_rankedMedal = widget.GetComponentInChildren<RankedMedal>();
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00028C60 File Offset: 0x00026E60
	private void InitPlayerIcon()
	{
		this.m_playerIcon = UnityEngine.Object.Instantiate<PlayerIcon>(this.m_Prefabs.m_PlayerIcon);
		this.m_playerIcon.transform.parent = base.transform;
		TransformUtil.CopyWorld(this.m_playerIcon, this.m_Bones.m_PlayerIcon);
		SceneUtils.SetLayer(this.m_playerIcon, base.gameObject.layer);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00028CC8 File Offset: 0x00026EC8
	private void OnResize()
	{
		float viewWindowMaxValue = this.m_chatLog.messageFrames.ViewWindowMaxValue;
		this.m_chatLog.messageFrames.transform.position = (this.m_Bones.m_MessagesTopLeft.position + this.m_Bones.m_MessagesBottomRight.position) / 2f;
		Vector3 vector = this.m_Bones.m_MessagesBottomRight.localPosition - this.m_Bones.m_MessagesTopLeft.localPosition;
		this.m_chatLog.messageFrames.ClipSize = new Vector2(vector.x, Math.Abs(vector.y));
		this.m_chatLog.messageFrames.ViewWindowMaxValue = viewWindowMaxValue;
		this.m_chatLog.messageFrames.ScrollValue = Mathf.Clamp01(this.m_chatLog.messageFrames.ScrollValue);
		this.m_chatLog.OnResize();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00028DB8 File Offset: 0x00026FB8
	private void UpdateReceiver()
	{
		this.m_playerIcon.UpdateIcon();
		this.m_NameText.Text = FriendUtils.GetUniqueNameWithColor(this.m_receiver);
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.m_receiver.GetBestGameAccount());
		if (this.m_receiver != null && this.m_receiver.IsDisplayable() && this.m_receiver.IsOnline())
		{
			if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
			{
				this.m_playerIcon.Show();
			}
			else
			{
				this.m_playerIcon.Hide();
			}
		}
		else if (!this.m_receiver.IsOnline())
		{
			this.m_playerIcon.Show();
		}
		if (rankPresenceField != null && rankPresenceField.IsDisplayable())
		{
			rankPresenceField.CreateOrUpdateDataModel(rankPresenceField.GetBestCurrentRankFormatType(), ref this.m_rankedDataModel, RankedMedal.DisplayMode.Default, false, false, null);
		}
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00028E84 File Offset: 0x00027084
	private void UpdateRankedMedalWidget()
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.m_receiver.GetBestGameAccount());
		if (rankPresenceField != null && rankPresenceField.IsDisplayable())
		{
			this.m_medalPatch.SetActive(true);
			if (this.m_rankedMedal != null)
			{
				this.m_rankedMedal.BindRankedPlayDataModel(this.m_rankedDataModel);
				return;
			}
		}
		else
		{
			this.m_medalPatch.SetActive(false);
		}
	}

	// Token: 0x040004E2 RID: 1250
	public ChatLogFrameBones m_Bones;

	// Token: 0x040004E3 RID: 1251
	public ChatLogFramePrefabs m_Prefabs;

	// Token: 0x040004E4 RID: 1252
	public UberText m_NameText;

	// Token: 0x040004E5 RID: 1253
	public ChatLog m_chatLog;

	// Token: 0x040004E6 RID: 1254
	public GameObject m_medalPatch;

	// Token: 0x040004E7 RID: 1255
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x040004E8 RID: 1256
	private PlayerIcon m_playerIcon;

	// Token: 0x040004E9 RID: 1257
	private BnetPlayer m_receiver;

	// Token: 0x040004EA RID: 1258
	private RankedMedal m_rankedMedal;

	// Token: 0x040004EB RID: 1259
	private RankedPlayDataModel m_rankedDataModel;

	// Token: 0x040004EC RID: 1260
	private Widget m_rankedMedalWidget;
}
