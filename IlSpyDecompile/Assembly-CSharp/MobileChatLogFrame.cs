using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class MobileChatLogFrame : MonoBehaviour
{
	[Serializable]
	public class MessageInfo
	{
		public Transform messagesTopLeft;

		public Transform messagesBottomRight;
	}

	[Serializable]
	public class InputInfo
	{
		public Transform inputTopLeft;

		public Transform inputBottomRight;
	}

	[Serializable]
	public class Followers
	{
		public UIBFollowObject playerInfoFollower;

		public UIBFollowObject closeButtonFollower;

		public UIBFollowObject bubbleFollower;

		public void UpdateFollowPosition()
		{
			playerInfoFollower.UpdateFollowPosition();
			closeButtonFollower.UpdateFollowPosition();
			bubbleFollower.UpdateFollowPosition();
		}
	}

	public Spawner playerIconRef;

	public TouchList messageFrames;

	public InputInfo inputInfo;

	public TextField inputTextField;

	public MessageInfo messageInfo;

	public NineSliceElement window;

	public UberText nameText;

	public UIBButton closeButton;

	public MobileChatNotification notifications;

	public GameObject m_medalPatch;

	public AsyncReference m_rankedMedalWidgetReference;

	public ChatLog chatLog;

	public Followers followers;

	private PlayerIcon playerIcon;

	private BnetPlayer receiver;

	private RankedMedal m_rankedMedal;

	private RankedPlayDataModel m_rankedDataModel;

	private Widget m_rankedMedalWidget;

	public bool HasFocus => inputTextField.Active;

	public BnetPlayer Receiver
	{
		get
		{
			return receiver;
		}
		set
		{
			if (receiver != value)
			{
				receiver = value;
				if (receiver != null)
				{
					playerIcon.SetPlayer(receiver);
					UpdateReceiver();
					chatLog.Receiver = receiver;
				}
			}
		}
	}

	public bool IsWaitingOnMedal
	{
		get
		{
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(receiver.GetBestGameAccount());
			if (rankPresenceField != null && rankPresenceField.IsDisplayable())
			{
				if (!(m_rankedMedalWidget == null) && m_rankedMedalWidget.IsReady)
				{
					return m_rankedMedalWidget.IsChangingStates;
				}
				return true;
			}
			return false;
		}
	}

	public event Action InputCanceled;

	public event Action CloseButtonReleased;

	private void Awake()
	{
		playerIcon = playerIconRef.Spawn<PlayerIcon>();
		UpdateBackgroundCollider();
		inputTextField.maxCharacters = 512;
		inputTextField.Changed += OnInputChanged;
		inputTextField.Submitted += OnInputComplete;
		inputTextField.Canceled += OnInputCanceled;
		closeButton.AddEventListener(UIEventType.RELEASE, OnCloseButtonReleased);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
	}

	private void Start()
	{
		if (receiver == null)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			string pendingMessage = ChatMgr.Get().GetPendingMessage(receiver.GetAccountId());
			if (pendingMessage != null)
			{
				inputTextField.Text = pendingMessage;
			}
		}
		m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnRankedMedalWidgetReady);
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
	}

	private void Update()
	{
	}

	public void Focus(bool focus)
	{
		if (focus && !inputTextField.Active)
		{
			inputTextField.Activate();
		}
		else if (!focus && inputTextField.Active)
		{
			inputTextField.Deactivate();
		}
	}

	public void SetWorldRect(float x, float y, float width, float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(value: true);
		float viewWindowMaxValue = messageFrames.ViewWindowMaxValue;
		window.SetEntireSize(width, height);
		Vector3 vector = TransformUtil.ComputeWorldPoint(TransformUtil.ComputeSetPointBounds(window), new Vector3(0f, 1f, 0f));
		Vector3 translation = new Vector3(x, y, vector.z) - vector;
		base.transform.Translate(translation);
		messageFrames.transform.position = (messageInfo.messagesTopLeft.position + messageInfo.messagesBottomRight.position) / 2f;
		Vector3 vector2 = messageInfo.messagesBottomRight.position - messageInfo.messagesTopLeft.position;
		messageFrames.ClipSize = new Vector2(vector2.x, Math.Abs(vector2.y));
		messageFrames.ViewWindowMaxValue = viewWindowMaxValue;
		messageFrames.ScrollValue = Mathf.Clamp01(messageFrames.ScrollValue);
		chatLog.OnResize();
		UpdateBackgroundCollider();
		UpdateFollowers();
		base.gameObject.SetActive(activeSelf);
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(receiver) != null)
		{
			UpdateReceiver();
		}
	}

	private void OnRankedMedalWidgetReady(Widget widget)
	{
		m_rankedMedalWidget = widget;
		m_rankedMedal = widget.GetComponentInChildren<RankedMedal>();
		UpdateRankedMedalWidget();
	}

	private void OnCloseButtonReleased(UIEvent e)
	{
		if (this.CloseButtonReleased != null)
		{
			this.CloseButtonReleased();
		}
	}

	private bool IsFullScreenKeyboard()
	{
		return ChatMgr.Get().KeyboardRect.height == (float)Screen.height;
	}

	private void OnInputChanged(string input)
	{
		ChatMgr.Get().SetPendingMessage(receiver.GetAccountId(), input);
	}

	private void OnInputComplete(string input)
	{
		if (!string.IsNullOrEmpty(input))
		{
			if (!BnetWhisperMgr.Get().SendWhisper(receiver, input))
			{
				chatLog.OnWhisperFailed();
			}
			ChatMgr.Get().SetPendingMessage(receiver.GetAccountId(), null);
			ChatMgr.Get().AddRecentWhisperPlayerToTop(receiver);
		}
	}

	private void OnInputCanceled()
	{
		if (this.InputCanceled != null)
		{
			this.InputCanceled();
		}
	}

	private void UpdateReceiver()
	{
		playerIcon.UpdateIcon();
		string arg = (receiver.IsOnline() ? "5ecaf0ff" : "999999ff");
		string bestName = receiver.GetBestName();
		nameText.Text = $"<color=#{arg}>{bestName}</color>";
		if (receiver != null && receiver.IsDisplayable() && receiver.IsOnline())
		{
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(receiver.GetBestGameAccount());
			if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
			{
				playerIcon.Show();
			}
			else
			{
				playerIcon.Hide();
				rankPresenceField.CreateOrUpdateDataModel(rankPresenceField.GetBestCurrentRankFormatType(), ref m_rankedDataModel, RankedMedal.DisplayMode.Default);
			}
		}
		else if (!receiver.IsOnline())
		{
			playerIcon.Show();
		}
		UpdateRankedMedalWidget();
	}

	private void UpdateRankedMedalWidget()
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(receiver.GetBestGameAccount());
		if (rankPresenceField != null && rankPresenceField.IsDisplayable())
		{
			m_medalPatch.SetActive(value: true);
			if (m_rankedMedal != null)
			{
				m_rankedMedal.BindRankedPlayDataModel(m_rankedDataModel);
			}
		}
		else
		{
			m_medalPatch.SetActive(value: false);
		}
	}

	private void UpdateBackgroundCollider()
	{
		BoxCollider boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			boxCollider.center = new Vector3(0f, 0f, -100f);
			boxCollider.size = new Vector3(10000f, 10000f, 0f);
			return;
		}
		Bounds bounds = window.GetComponentsInChildren<Renderer>().Aggregate(new Bounds(base.transform.position, Vector3.zero), delegate(Bounds aggregate, Renderer renderer)
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

	private void UpdateFollowers()
	{
		followers.UpdateFollowPosition();
	}
}
