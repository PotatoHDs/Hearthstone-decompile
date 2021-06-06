using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ChatLogFrame : MonoBehaviour
{
	public ChatLogFrameBones m_Bones;

	public ChatLogFramePrefabs m_Prefabs;

	public UberText m_NameText;

	public ChatLog m_chatLog;

	public GameObject m_medalPatch;

	public AsyncReference m_rankedMedalWidgetReference;

	private PlayerIcon m_playerIcon;

	private BnetPlayer m_receiver;

	private RankedMedal m_rankedMedal;

	private RankedPlayDataModel m_rankedDataModel;

	private Widget m_rankedMedalWidget;

	public BnetPlayer Receiver
	{
		get
		{
			return m_receiver;
		}
		set
		{
			if (m_receiver != value)
			{
				m_receiver = value;
				if (m_receiver != null)
				{
					m_playerIcon.SetPlayer(m_receiver);
					UpdateReceiver();
					m_chatLog.Receiver = m_receiver;
				}
			}
		}
	}

	public bool IsWaitingOnMedal
	{
		get
		{
			if (Receiver == null)
			{
				return true;
			}
			MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(Receiver.GetBestGameAccount());
			if (rankPresenceField != null && rankPresenceField.IsDisplayable())
			{
				if (!(m_rankedMedalWidget == null))
				{
					return !m_rankedMedal.IsReady;
				}
				return true;
			}
			return false;
		}
	}

	private void Awake()
	{
		InitPlayerIcon();
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
	}

	private void Start()
	{
		m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnRankedMedalWidgetReady);
		UpdateLayout();
	}

	private void OnDestroy()
	{
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
	}

	public void UpdateLayout()
	{
		OnResize();
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(m_receiver) != null)
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

	private void InitPlayerIcon()
	{
		m_playerIcon = UnityEngine.Object.Instantiate(m_Prefabs.m_PlayerIcon);
		m_playerIcon.transform.parent = base.transform;
		TransformUtil.CopyWorld(m_playerIcon, m_Bones.m_PlayerIcon);
		SceneUtils.SetLayer(m_playerIcon, base.gameObject.layer);
	}

	private void OnResize()
	{
		float viewWindowMaxValue = m_chatLog.messageFrames.ViewWindowMaxValue;
		m_chatLog.messageFrames.transform.position = (m_Bones.m_MessagesTopLeft.position + m_Bones.m_MessagesBottomRight.position) / 2f;
		Vector3 vector = m_Bones.m_MessagesBottomRight.localPosition - m_Bones.m_MessagesTopLeft.localPosition;
		m_chatLog.messageFrames.ClipSize = new Vector2(vector.x, Math.Abs(vector.y));
		m_chatLog.messageFrames.ViewWindowMaxValue = viewWindowMaxValue;
		m_chatLog.messageFrames.ScrollValue = Mathf.Clamp01(m_chatLog.messageFrames.ScrollValue);
		m_chatLog.OnResize();
	}

	private void UpdateReceiver()
	{
		m_playerIcon.UpdateIcon();
		m_NameText.Text = FriendUtils.GetUniqueNameWithColor(m_receiver);
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(m_receiver.GetBestGameAccount());
		if (m_receiver != null && m_receiver.IsDisplayable() && m_receiver.IsOnline())
		{
			if (rankPresenceField == null || !rankPresenceField.IsDisplayable())
			{
				m_playerIcon.Show();
			}
			else
			{
				m_playerIcon.Hide();
			}
		}
		else if (!m_receiver.IsOnline())
		{
			m_playerIcon.Show();
		}
		if (rankPresenceField != null && rankPresenceField.IsDisplayable())
		{
			rankPresenceField.CreateOrUpdateDataModel(rankPresenceField.GetBestCurrentRankFormatType(), ref m_rankedDataModel, RankedMedal.DisplayMode.Default);
		}
		UpdateRankedMedalWidget();
	}

	private void UpdateRankedMedalWidget()
	{
		MedalInfoTranslator rankPresenceField = RankMgr.Get().GetRankPresenceField(m_receiver.GetBestGameAccount());
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
}
