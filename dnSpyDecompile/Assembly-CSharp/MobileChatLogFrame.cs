using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class MobileChatLogFrame : MonoBehaviour
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000999 RID: 2457 RVA: 0x000379D1 File Offset: 0x00035BD1
	public bool HasFocus
	{
		get
		{
			return this.inputTextField.Active;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600099A RID: 2458 RVA: 0x000379DE File Offset: 0x00035BDE
	// (set) Token: 0x0600099B RID: 2459 RVA: 0x000379E8 File Offset: 0x00035BE8
	public BnetPlayer Receiver
	{
		get
		{
			return this.receiver;
		}
		set
		{
			if (this.receiver == value)
			{
				return;
			}
			this.receiver = value;
			if (this.receiver != null)
			{
				this.playerIcon.SetPlayer(this.receiver);
				this.UpdateReceiver();
				this.chatLog.Receiver = this.receiver;
			}
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600099C RID: 2460 RVA: 0x00037A38 File Offset: 0x00035C38
	public bool IsWaitingOnMedal
	{
		get
		{
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.receiver.GetBestGameAccount());
			return rankPresenceField != null && rankPresenceField.IsDisplayable() && (this.m_rankedMedalWidget == null || !this.m_rankedMedalWidget.IsReady || this.m_rankedMedalWidget.IsChangingStates);
		}
	}

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x0600099D RID: 2461 RVA: 0x00037A90 File Offset: 0x00035C90
	// (remove) Token: 0x0600099E RID: 2462 RVA: 0x00037AC8 File Offset: 0x00035CC8
	public event Action InputCanceled;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x0600099F RID: 2463 RVA: 0x00037B00 File Offset: 0x00035D00
	// (remove) Token: 0x060009A0 RID: 2464 RVA: 0x00037B38 File Offset: 0x00035D38
	public event Action CloseButtonReleased;

	// Token: 0x060009A1 RID: 2465 RVA: 0x00037B70 File Offset: 0x00035D70
	private void Awake()
	{
		this.playerIcon = this.playerIconRef.Spawn<PlayerIcon>();
		this.UpdateBackgroundCollider();
		this.inputTextField.maxCharacters = 512;
		this.inputTextField.Changed += this.OnInputChanged;
		this.inputTextField.Submitted += this.OnInputComplete;
		this.inputTextField.Canceled += this.OnInputCanceled;
		this.closeButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCloseButtonReleased));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00037C1C File Offset: 0x00035E1C
	private void Start()
	{
		if (this.receiver == null)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			string pendingMessage = ChatMgr.Get().GetPendingMessage(this.receiver.GetAccountId());
			if (pendingMessage != null)
			{
				this.inputTextField.Text = pendingMessage;
			}
		}
		this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00037C7B File Offset: 0x00035E7B
	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00037C94 File Offset: 0x00035E94
	public void Focus(bool focus)
	{
		if (focus && !this.inputTextField.Active)
		{
			this.inputTextField.Activate();
			return;
		}
		if (!focus && this.inputTextField.Active)
		{
			this.inputTextField.Deactivate();
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00037CD0 File Offset: 0x00035ED0
	public void SetWorldRect(float x, float y, float width, float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(true);
		float viewWindowMaxValue = this.messageFrames.ViewWindowMaxValue;
		this.window.SetEntireSize(width, height);
		Vector3 vector = TransformUtil.ComputeWorldPoint(TransformUtil.ComputeSetPointBounds(this.window), new Vector3(0f, 1f, 0f));
		Vector3 translation = new Vector3(x, y, vector.z) - vector;
		base.transform.Translate(translation);
		this.messageFrames.transform.position = (this.messageInfo.messagesTopLeft.position + this.messageInfo.messagesBottomRight.position) / 2f;
		Vector3 vector2 = this.messageInfo.messagesBottomRight.position - this.messageInfo.messagesTopLeft.position;
		this.messageFrames.ClipSize = new Vector2(vector2.x, Math.Abs(vector2.y));
		this.messageFrames.ViewWindowMaxValue = viewWindowMaxValue;
		this.messageFrames.ScrollValue = Mathf.Clamp01(this.messageFrames.ScrollValue);
		this.chatLog.OnResize();
		this.UpdateBackgroundCollider();
		this.UpdateFollowers();
		base.gameObject.SetActive(activeSelf);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00037E27 File Offset: 0x00036027
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(this.receiver) == null)
		{
			return;
		}
		this.UpdateReceiver();
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00037E3E File Offset: 0x0003603E
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		this.m_rankedMedalWidget = widget;
		this.m_rankedMedal = widget.GetComponentInChildren<RankedMedal>();
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00037E59 File Offset: 0x00036059
	private void OnCloseButtonReleased(UIEvent e)
	{
		if (this.CloseButtonReleased != null)
		{
			this.CloseButtonReleased();
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00037E70 File Offset: 0x00036070
	private bool IsFullScreenKeyboard()
	{
		return ChatMgr.Get().KeyboardRect.height == (float)Screen.height;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00037E97 File Offset: 0x00036097
	private void OnInputChanged(string input)
	{
		ChatMgr.Get().SetPendingMessage(this.receiver.GetAccountId(), input);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00037EB0 File Offset: 0x000360B0
	private void OnInputComplete(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return;
		}
		if (!BnetWhisperMgr.Get().SendWhisper(this.receiver, input))
		{
			this.chatLog.OnWhisperFailed();
		}
		ChatMgr.Get().SetPendingMessage(this.receiver.GetAccountId(), null);
		ChatMgr.Get().AddRecentWhisperPlayerToTop(this.receiver);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00037F0A File Offset: 0x0003610A
	private void OnInputCanceled()
	{
		if (this.InputCanceled != null)
		{
			this.InputCanceled();
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00037F20 File Offset: 0x00036120
	private void UpdateReceiver()
	{
		this.playerIcon.UpdateIcon();
		string arg = this.receiver.IsOnline() ? "5ecaf0ff" : "999999ff";
		string bestName = this.receiver.GetBestName();
		this.nameText.Text = string.Format("<color=#{0}>{1}</color>", arg, bestName);
		if (this.receiver != null && this.receiver.IsDisplayable() && this.receiver.IsOnline())
		{
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.receiver.GetBestGameAccount());
			if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
			{
				this.playerIcon.Show();
			}
			else
			{
				this.playerIcon.Hide();
				rankPresenceField.CreateOrUpdateDataModel(rankPresenceField.GetBestCurrentRankFormatType(), ref this.m_rankedDataModel, RankedMedal.DisplayMode.Default, false, false, null);
			}
		}
		else if (!this.receiver.IsOnline())
		{
			this.playerIcon.Show();
		}
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00038008 File Offset: 0x00036208
	private void UpdateRankedMedalWidget()
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(this.receiver.GetBestGameAccount());
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

	// Token: 0x060009B0 RID: 2480 RVA: 0x00038070 File Offset: 0x00036270
	private void UpdateBackgroundCollider()
	{
		BoxCollider boxCollider = base.GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			boxCollider.center = new Vector3(0f, 0f, -100f);
			boxCollider.size = new Vector3(10000f, 10000f, 0f);
			return;
		}
		Bounds bounds = this.window.GetComponentsInChildren<Renderer>().Aggregate(new Bounds(base.transform.position, Vector3.zero), delegate(Bounds aggregate, Renderer renderer)
		{
			if (renderer.bounds.size.x != 0f && renderer.bounds.size.y != 0f && renderer.bounds.size.z != 0f)
			{
				aggregate.Encapsulate(renderer.bounds);
			}
			return aggregate;
		});
		Vector3 vector = base.transform.InverseTransformPoint(bounds.min);
		Vector3 vector2 = base.transform.InverseTransformPoint(bounds.max);
		boxCollider.center = (vector + vector2) / 2f + Vector3.forward;
		boxCollider.size = vector2 - vector;
		boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0f);
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00038199 File Offset: 0x00036399
	private void UpdateFollowers()
	{
		this.followers.UpdateFollowPosition();
	}

	// Token: 0x0400065B RID: 1627
	public Spawner playerIconRef;

	// Token: 0x0400065C RID: 1628
	public TouchList messageFrames;

	// Token: 0x0400065D RID: 1629
	public MobileChatLogFrame.InputInfo inputInfo;

	// Token: 0x0400065E RID: 1630
	public TextField inputTextField;

	// Token: 0x0400065F RID: 1631
	public MobileChatLogFrame.MessageInfo messageInfo;

	// Token: 0x04000660 RID: 1632
	public NineSliceElement window;

	// Token: 0x04000661 RID: 1633
	public UberText nameText;

	// Token: 0x04000662 RID: 1634
	public UIBButton closeButton;

	// Token: 0x04000663 RID: 1635
	public MobileChatNotification notifications;

	// Token: 0x04000664 RID: 1636
	public GameObject m_medalPatch;

	// Token: 0x04000665 RID: 1637
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x04000666 RID: 1638
	public ChatLog chatLog;

	// Token: 0x04000667 RID: 1639
	public MobileChatLogFrame.Followers followers;

	// Token: 0x04000668 RID: 1640
	private PlayerIcon playerIcon;

	// Token: 0x04000669 RID: 1641
	private BnetPlayer receiver;

	// Token: 0x0400066A RID: 1642
	private RankedMedal m_rankedMedal;

	// Token: 0x0400066B RID: 1643
	private RankedPlayDataModel m_rankedDataModel;

	// Token: 0x0400066C RID: 1644
	private Widget m_rankedMedalWidget;

	// Token: 0x0200139E RID: 5022
	[Serializable]
	public class MessageInfo
	{
		// Token: 0x0400A73B RID: 42811
		public Transform messagesTopLeft;

		// Token: 0x0400A73C RID: 42812
		public Transform messagesBottomRight;
	}

	// Token: 0x0200139F RID: 5023
	[Serializable]
	public class InputInfo
	{
		// Token: 0x0400A73D RID: 42813
		public Transform inputTopLeft;

		// Token: 0x0400A73E RID: 42814
		public Transform inputBottomRight;
	}

	// Token: 0x020013A0 RID: 5024
	[Serializable]
	public class Followers
	{
		// Token: 0x0600D80D RID: 55309 RVA: 0x003ED7E4 File Offset: 0x003EB9E4
		public void UpdateFollowPosition()
		{
			this.playerInfoFollower.UpdateFollowPosition();
			this.closeButtonFollower.UpdateFollowPosition();
			this.bubbleFollower.UpdateFollowPosition();
		}

		// Token: 0x0400A73F RID: 42815
		public UIBFollowObject playerInfoFollower;

		// Token: 0x0400A740 RID: 42816
		public UIBFollowObject closeButtonFollower;

		// Token: 0x0400A741 RID: 42817
		public UIBFollowObject bubbleFollower;
	}
}
