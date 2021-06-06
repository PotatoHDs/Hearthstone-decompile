using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class BaconParty : MonoBehaviour
{
	private class BaconPartyMemberInfo
	{
		public Status status;

		public BnetGameAccountId playerGameAccountId;
	}

	public enum Status
	{
		Inactive,
		Waiting,
		Ready,
		Leader,
		Leave,
		NotReady,
		Spectate
	}

	private class AnimatedEvent
	{
		public Event type;

		public BnetGameAccountId playerGameAccountId;

		public bool isReady;
	}

	private enum Event
	{
		Add,
		Remove,
		Ready
	}

	public AsyncReference m_PartyPanelReference;

	public AsyncReference m_Member0;

	public AsyncReference m_Member1;

	public AsyncReference m_Member2;

	public AsyncReference m_Member3;

	public AsyncReference m_Member4;

	public AsyncReference m_Member5;

	public AsyncReference m_Member6;

	public AsyncReference m_Member7;

	public GameObject m_ClickBlocker;

	private Widget m_partyPanel;

	private List<Widget> m_members;

	private Dictionary<int, Widget> m_memberWidgetByWidgetIndex = new Dictionary<int, Widget>();

	private List<BaconPartyMemberInfo> m_memberInfo;

	private Queue<AnimatedEvent> m_animQueue;

	private bool m_animating;

	private bool m_panelLoaded;

	private int m_membersLoaded;

	private static BaconParty s_instance;

	public static BaconParty Get()
	{
		return s_instance;
	}

	public void Start()
	{
		s_instance = this;
		m_animQueue = new Queue<AnimatedEvent>();
		m_members = new List<Widget>(PartyManager.BATTLEGROUNDS_PARTY_LIMIT);
		m_memberInfo = new List<BaconPartyMemberInfo>(PartyManager.BATTLEGROUNDS_PARTY_LIMIT);
		m_panelLoaded = false;
		m_membersLoaded = 0;
		for (int i = 0; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			m_members.Add(null);
			m_memberInfo.Add(new BaconPartyMemberInfo());
		}
		m_PartyPanelReference.RegisterReadyListener<Widget>(OnPartyPanelReady);
		m_Member0.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 0);
		});
		m_Member1.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 1);
		});
		m_Member2.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 2);
		});
		m_Member3.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 3);
		});
		m_Member4.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 4);
		});
		m_Member5.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 5);
		});
		m_Member6.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 6);
		});
		m_Member7.RegisterReadyListener(delegate(Widget c)
		{
			OnMemberReady(c, 7);
		});
		PartyManager.Get().AddChangedListener(OnPartyChanged);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPresenceUpdated);
		BnetNearbyPlayerMgr.Get().AddChangeListener(OnNearbyPlayersUpdated);
		SpectatorManager.Get().OnSpectateRejected += OnSpectateRejected;
		StartCoroutine(ReconcileWhenReady());
	}

	public void OnDestroy()
	{
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(OnPartyChanged);
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPresenceUpdated);
		}
		if (BnetNearbyPlayerMgr.Get() != null)
		{
			BnetNearbyPlayerMgr.Get().RemoveChangeListener(OnNearbyPlayersUpdated);
		}
		if (SpectatorManager.Get() != null)
		{
			SpectatorManager.Get().OnSpectateRejected -= OnSpectateRejected;
		}
	}

	private bool IsLoadedAndReady()
	{
		if (m_panelLoaded)
		{
			return m_membersLoaded == PartyManager.BATTLEGROUNDS_PARTY_LIMIT;
		}
		return false;
	}

	private IEnumerator ReconcileWhenReady()
	{
		while (!IsLoadedAndReady())
		{
			yield return new WaitForEndOfFrame();
		}
		if (PartyManager.Get().IsInBattlegroundsParty())
		{
			while (PartyManager.Get().GetCurrentAndPendingPartyMembers().Count == 0)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (PartyManager.Get().GetCurrentPartySize() == 1)
		{
			LeaveParty();
		}
		RefreshDisplay();
		UpdateDataModelData();
		FriendChallengeMgr.Get().UpdateMyAvailability();
	}

	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		Log.Party.PrintDebug("BaconParty.OnPartyChanged(): Event={0}, gameAccountId={1}", inviteEvent, playerGameAccountId);
		switch (inviteEvent)
		{
		case PartyManager.PartyInviteEvent.I_SENT_INVITE:
		case PartyManager.PartyInviteEvent.FRIEND_RECEIVED_INVITE:
			AddPartyMember(playerGameAccountId);
			break;
		case PartyManager.PartyInviteEvent.I_CREATED_PARTY:
			RefreshDisplay();
			break;
		case PartyManager.PartyInviteEvent.FRIEND_ACCEPTED_INVITE:
			SetReady(playerGameAccountId);
			break;
		case PartyManager.PartyInviteEvent.I_RESCINDED_INVITE:
		case PartyManager.PartyInviteEvent.FRIEND_DECLINED_INVITE:
		case PartyManager.PartyInviteEvent.INVITE_EXPIRED:
		case PartyManager.PartyInviteEvent.FRIEND_LEFT:
			RemovePartyMember(playerGameAccountId);
			break;
		case PartyManager.PartyInviteEvent.I_ACCEPTED_INVITE:
			StartCoroutine(ReconcileWhenReady());
			break;
		case PartyManager.PartyInviteEvent.LEADER_DISSOLVED_PARTY:
			RefreshDisplay();
			break;
		}
		UpdateDataModelData();
	}

	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		List<BnetPlayer> list = new List<BnetPlayer>();
		foreach (BnetPlayerChange change in changelist.GetChanges())
		{
			BnetPlayer player = change.GetPlayer();
			list.Add(player);
		}
		UpdateChangedPlayersFromPresenceUpdate(list);
	}

	private void OnNearbyPlayersUpdated(BnetNearbyPlayerChangelist changelist, object userData)
	{
		UpdateChangedPlayersFromPresenceUpdate(changelist.GetUpdatedStrangers());
	}

	private void UpdateChangedPlayersFromPresenceUpdate(List<BnetPlayer> changedPlayers)
	{
		if (changedPlayers == null)
		{
			return;
		}
		bool flag = false;
		foreach (BnetPlayer changedPlayer in changedPlayers)
		{
			if (changedPlayer == BnetPresenceMgr.Get().GetMyPlayer() && changedPlayer.IsAppearingOffline())
			{
				LeaveParty();
				UpdateDataModelData();
				return;
			}
			if (!PartyManager.Get().IsPlayerInCurrentPartyOrPending(changedPlayer.GetBestGameAccountId()))
			{
				continue;
			}
			for (int i = 0; i < m_memberInfo.Count; i++)
			{
				if (m_memberInfo[i].playerGameAccountId == changedPlayer.GetBestGameAccountId())
				{
					m_memberInfo[i].status = GetReadyStatusForPartyMember(m_memberInfo[i].playerGameAccountId);
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			RefreshVisuals();
		}
	}

	private void OnSpectateRejected()
	{
		CleanUpSpectateFx();
	}

	private void OnPartyPanelReady(Widget controller)
	{
		m_partyPanel = controller;
		m_panelLoaded = true;
	}

	private void OnMemberReady(Widget widget, int index)
	{
		m_memberWidgetByWidgetIndex.Add(index, widget);
		m_members[index] = widget;
		widget.RegisterEventListener(delegate(string s)
		{
			OnPartyMemberEvent(index, s);
		});
		m_membersLoaded++;
	}

	private void OnPartyMemberEvent(int index, string eventString)
	{
		if (!m_memberWidgetByWidgetIndex.ContainsKey(index))
		{
			Debug.LogErrorFormat("OnPartyMemberEvent() - No party member widget at index {0}", index);
		}
		else if (eventString == "SPECTATE_BUTTON_PRESSED")
		{
			Widget item = m_memberWidgetByWidgetIndex[index];
			int num = m_members.IndexOf(item);
			if (num == -1)
			{
				Debug.LogErrorFormat("OnPartyMemberEvent() - Widget at index {0} not found in m_members list.", index);
			}
			else
			{
				m_ClickBlocker.SetActive(value: true);
				BnetGameAccountId playerGameAccountId = m_memberInfo[num].playerGameAccountId;
				StartCoroutine(SpectatePlayerWithAnimations(playerGameAccountId));
			}
		}
	}

	public BaconPartyDataModel GetBaconPartyDataModel()
	{
		if (!m_partyPanel.GetDataModel(154, out var model))
		{
			model = new BaconPartyDataModel();
			m_partyPanel.BindDataModel(model);
		}
		return model as BaconPartyDataModel;
	}

	private void RefreshDisplay()
	{
		if (!PartyManager.Get().IsInBattlegroundsParty())
		{
			return;
		}
		BnetGameAccountId leaderGameAccountId = PartyManager.Get().GetLeader();
		BnetGameAccountId myselfGameAccountId = BnetPresenceMgr.Get().GetMyPlayer().GetBestGameAccountId();
		List<BnetGameAccountId> currentAndPendingPartyMembers = PartyManager.Get().GetCurrentAndPendingPartyMembers();
		if (!PartyManager.Get().IsPartyLeader())
		{
			currentAndPendingPartyMembers.Sort(delegate(BnetGameAccountId m1, BnetGameAccountId m2)
			{
				if (m1 == leaderGameAccountId)
				{
					return -1;
				}
				if (m2 == leaderGameAccountId)
				{
					return 1;
				}
				if (m1 == myselfGameAccountId)
				{
					return -1;
				}
				return (m2 == myselfGameAccountId) ? 1 : 0;
			});
		}
		for (int i = 0; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			BnetGameAccountId bnetGameAccountId = null;
			if (i < currentAndPendingPartyMembers.Count)
			{
				bnetGameAccountId = currentAndPendingPartyMembers[i];
			}
			if (PartyManager.Get().IsPlayerInCurrentParty(bnetGameAccountId))
			{
				m_memberInfo[i].status = GetReadyStatusForPartyMember(bnetGameAccountId);
				m_memberInfo[i].playerGameAccountId = bnetGameAccountId;
			}
			else if (bnetGameAccountId != null)
			{
				m_memberInfo[i].status = Status.Waiting;
				m_memberInfo[i].playerGameAccountId = bnetGameAccountId;
			}
			else
			{
				m_memberInfo[i].status = Status.Inactive;
				m_memberInfo[i].playerGameAccountId = null;
			}
		}
		RefreshVisuals();
	}

	private void UpdateDataModelData()
	{
		if (IsLoadedAndReady())
		{
			BaconPartyDataModel baconPartyDataModel = GetBaconPartyDataModel();
			baconPartyDataModel.Active = PartyManager.Get().IsInBattlegroundsParty();
			baconPartyDataModel.Size = PartyManager.Get().GetCurrentPartySize();
			baconPartyDataModel.PrivateGame = baconPartyDataModel.Size > PartyManager.Get().GetBattlegroundsMaxRankedPartySize();
		}
	}

	public void LeaveParty()
	{
		PartyManager.Get().LeaveParty();
	}

	private void AddPartyMember(BnetGameAccountId playerGameAccountId, bool isReady = false)
	{
		if (PartyManager.Get().GetCurrentPartySize() <= PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			AnimatedEvent animatedEvent = new AnimatedEvent();
			animatedEvent.type = Event.Add;
			animatedEvent.playerGameAccountId = playerGameAccountId;
			animatedEvent.isReady = isReady;
			m_animQueue.Enqueue(animatedEvent);
			Animate();
		}
	}

	private void Animate()
	{
		if (!m_animating && m_animQueue.Count != 0)
		{
			AnimatedEvent animatedEvent = m_animQueue.Dequeue();
			switch (animatedEvent.type)
			{
			case Event.Add:
				StartCoroutine(AddPartyMemberWithAnims(animatedEvent.playerGameAccountId, animatedEvent.isReady));
				break;
			case Event.Remove:
				StartCoroutine(RemovePartyMemberWithAnims(animatedEvent.playerGameAccountId));
				break;
			}
		}
	}

	private IEnumerator AddPartyMemberWithAnims(BnetGameAccountId playerGameAccountId, bool isReady)
	{
		m_animating = true;
		while (!IsLoadedAndReady())
		{
			yield return null;
		}
		int num = -1;
		for (int i = 0; i < m_memberInfo.Count; i++)
		{
			if (m_memberInfo[i].status == Status.Inactive)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			Log.Party.PrintError("AddPartyMemberWithAnims - No inactive members, unable to add new member.");
			m_animating = false;
			Animate();
			yield break;
		}
		m_memberInfo[num].status = ((!isReady) ? Status.Waiting : GetReadyStatusForPartyMember(playerGameAccountId));
		m_memberInfo[num].playerGameAccountId = playerGameAccountId;
		RefreshVisuals();
		yield return new WaitForSeconds(0.5f);
		m_animating = false;
		Animate();
	}

	private void RemovePartyMember(BnetGameAccountId playerGameAccountId)
	{
		if (PartyManager.Get().GetCurrentPartySize() == 1)
		{
			LeaveParty();
			return;
		}
		m_animQueue.Enqueue(new AnimatedEvent
		{
			type = Event.Remove,
			playerGameAccountId = playerGameAccountId
		});
		Animate();
	}

	private IEnumerator RemovePartyMemberWithAnims(BnetGameAccountId playerGameAccountId)
	{
		m_animating = true;
		while (!IsLoadedAndReady())
		{
			yield return null;
		}
		int index = GetIndexOfPartyMemberFromGameAccountId(playerGameAccountId);
		if (index < 0 || index >= PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			Log.Party.PrintError("RemovePartyMemberWithAnims() - Unable to find party member with id {0}.", playerGameAccountId);
			m_animating = false;
			Animate();
			yield break;
		}
		m_members[index].TriggerEvent("Leave");
		yield return new WaitForSeconds(0.5f);
		Vector3[] positions = new Vector3[PartyManager.BATTLEGROUNDS_PARTY_LIMIT];
		for (int i = index; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			positions[i] = new Vector3(m_members[i].gameObject.transform.localPosition.x, m_members[i].gameObject.transform.localPosition.y, m_members[i].gameObject.transform.localPosition.z);
		}
		for (int j = index + 1; j < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; j++)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["easetype"] = "easeOutBounce";
			hashtable["position"] = positions[j - 1];
			hashtable["islocal"] = true;
			hashtable["time"] = 0.5f;
			iTween.MoveTo(m_members[j].gameObject, hashtable);
		}
		yield return new WaitForSeconds(0.5f);
		m_memberInfo.RemoveAt(index);
		BaconPartyMemberInfo baconPartyMemberInfo = new BaconPartyMemberInfo();
		baconPartyMemberInfo.status = Status.Inactive;
		m_memberInfo.Add(baconPartyMemberInfo);
		Widget widget = m_members[index];
		m_members.RemoveAt(index);
		m_members.Add(widget);
		widget.transform.localPosition = positions[PartyManager.BATTLEGROUNDS_PARTY_LIMIT - 1];
		m_animating = false;
		Animate();
	}

	private int GetIndexOfPartyMemberFromGameAccountId(BnetGameAccountId playerGameAccountId)
	{
		for (int i = 0; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			if (m_memberInfo[i] != null && m_memberInfo[i].playerGameAccountId == playerGameAccountId)
			{
				return i;
			}
		}
		return -1;
	}

	private void SetReady(BnetGameAccountId playerGameAccountId)
	{
		int num = -1;
		for (int i = 0; i < m_memberInfo.Count; i++)
		{
			if (m_memberInfo[i].playerGameAccountId == playerGameAccountId)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			AddPartyMember(playerGameAccountId, isReady: true);
		}
		else if (num > 0 && num < PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			m_memberInfo[num].status = Status.Ready;
			m_members[num].TriggerEvent(Status.Ready.ToString());
		}
	}

	private void RefreshVisuals()
	{
		for (int i = 0; i < m_members.Count; i++)
		{
			m_members[i].TriggerEvent(m_memberInfo[i].status.ToString());
			if (m_memberInfo[i].playerGameAccountId != null)
			{
				string partyMemberName = PartyManager.Get().GetPartyMemberName(m_memberInfo[i].playerGameAccountId);
				m_members[i].transform.Find("BaconPartyMember/Root/Name").gameObject.GetComponent<UberText>().Text = partyMemberName;
			}
		}
	}

	private Status GetReadyStatusForPartyMember(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		bool flag = BnetFriendMgr.Get().IsFriend(player) || BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player);
		if (playerGameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			if (!PartyManager.Get().IsPartyLeader())
			{
				return Status.Ready;
			}
			return Status.Leader;
		}
		if (!PartyManager.Get().IsPlayerInCurrentParty(playerGameAccountId))
		{
			return Status.NotReady;
		}
		if (PartyManager.Get().CanSpectatePartyMember(playerGameAccountId))
		{
			return Status.Spectate;
		}
		if (PartyManager.Get().GetLeader() == playerGameAccountId)
		{
			if (!FriendChallengeMgr.Get().IsOpponentAvailable(player))
			{
				return Status.NotReady;
			}
			return Status.Leader;
		}
		if (player == null || !flag)
		{
			return Status.Ready;
		}
		if (FriendChallengeMgr.Get().IsOpponentAvailable(player))
		{
			return Status.Ready;
		}
		if (Network.Get().IsFindingGame())
		{
			return Status.Ready;
		}
		return Status.NotReady;
	}

	private IEnumerator SpectatePlayerWithAnimations(BnetGameAccountId playerGameAccountId)
	{
		float num = 0.25f;
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, num, iTween.EaseType.linear);
		yield return new WaitForSeconds(num);
		if (!PartyManager.Get().SpectatePartyMember(playerGameAccountId))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_BACON_PARTY_SPECTATE_ERROR_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_BACON_PARTY_SPECTATE_ERROR_BODY");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
			DialogManager.Get().ShowPopup(popupInfo);
			CleanUpSpectateFx();
		}
		m_ClickBlocker.SetActive(value: false);
	}

	private void CleanUpSpectateFx()
	{
		FullScreenFXMgr.Get().StopAllEffects();
	}
}
