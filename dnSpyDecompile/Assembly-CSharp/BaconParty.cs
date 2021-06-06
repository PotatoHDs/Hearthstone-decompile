using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class BaconParty : MonoBehaviour
{
	// Token: 0x0600060F RID: 1551 RVA: 0x00023BCF File Offset: 0x00021DCF
	public static BaconParty Get()
	{
		return BaconParty.s_instance;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00023BD8 File Offset: 0x00021DD8
	public void Start()
	{
		BaconParty.s_instance = this;
		this.m_animQueue = new Queue<BaconParty.AnimatedEvent>();
		this.m_members = new List<Widget>(PartyManager.BATTLEGROUNDS_PARTY_LIMIT);
		this.m_memberInfo = new List<BaconParty.BaconPartyMemberInfo>(PartyManager.BATTLEGROUNDS_PARTY_LIMIT);
		this.m_panelLoaded = false;
		this.m_membersLoaded = 0;
		for (int i = 0; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			this.m_members.Add(null);
			this.m_memberInfo.Add(new BaconParty.BaconPartyMemberInfo());
		}
		this.m_PartyPanelReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnPartyPanelReady));
		this.m_Member0.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 0);
		});
		this.m_Member1.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 1);
		});
		this.m_Member2.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 2);
		});
		this.m_Member3.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 3);
		});
		this.m_Member4.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 4);
		});
		this.m_Member5.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 5);
		});
		this.m_Member6.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 6);
		});
		this.m_Member7.RegisterReadyListener<Widget>(delegate(Widget c)
		{
			this.OnMemberReady(c, 7);
		});
		PartyManager.Get().AddChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		BnetNearbyPlayerMgr.Get().AddChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersUpdated));
		SpectatorManager.Get().OnSpectateRejected += this.OnSpectateRejected;
		base.StartCoroutine(this.ReconcileWhenReady());
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00023D88 File Offset: 0x00021F88
	public void OnDestroy()
	{
		if (PartyManager.Get() != null)
		{
			PartyManager.Get().RemoveChangedListener(new PartyManager.ChangedCallback(this.OnPartyChanged));
		}
		if (BnetPresenceMgr.Get() != null)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		}
		if (BnetNearbyPlayerMgr.Get() != null)
		{
			BnetNearbyPlayerMgr.Get().RemoveChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersUpdated));
		}
		if (SpectatorManager.Get() != null)
		{
			SpectatorManager.Get().OnSpectateRejected -= this.OnSpectateRejected;
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00023E0C File Offset: 0x0002200C
	private bool IsLoadedAndReady()
	{
		return this.m_panelLoaded && this.m_membersLoaded == PartyManager.BATTLEGROUNDS_PARTY_LIMIT;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00023E25 File Offset: 0x00022025
	private IEnumerator ReconcileWhenReady()
	{
		while (!this.IsLoadedAndReady())
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
			this.LeaveParty();
		}
		this.RefreshDisplay();
		this.UpdateDataModelData();
		FriendChallengeMgr.Get().UpdateMyAvailability();
		yield break;
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00023E34 File Offset: 0x00022034
	private void OnPartyChanged(PartyManager.PartyInviteEvent inviteEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData data, object userData)
	{
		global::Log.Party.PrintDebug("BaconParty.OnPartyChanged(): Event={0}, gameAccountId={1}", new object[]
		{
			inviteEvent,
			playerGameAccountId
		});
		switch (inviteEvent)
		{
		case PartyManager.PartyInviteEvent.I_CREATED_PARTY:
			this.RefreshDisplay();
			break;
		case PartyManager.PartyInviteEvent.I_SENT_INVITE:
		case PartyManager.PartyInviteEvent.FRIEND_RECEIVED_INVITE:
			this.AddPartyMember(playerGameAccountId, false);
			break;
		case PartyManager.PartyInviteEvent.I_RESCINDED_INVITE:
		case PartyManager.PartyInviteEvent.FRIEND_DECLINED_INVITE:
		case PartyManager.PartyInviteEvent.INVITE_EXPIRED:
		case PartyManager.PartyInviteEvent.FRIEND_LEFT:
			this.RemovePartyMember(playerGameAccountId);
			break;
		case PartyManager.PartyInviteEvent.FRIEND_ACCEPTED_INVITE:
			this.SetReady(playerGameAccountId);
			break;
		case PartyManager.PartyInviteEvent.I_ACCEPTED_INVITE:
			base.StartCoroutine(this.ReconcileWhenReady());
			break;
		case PartyManager.PartyInviteEvent.LEADER_DISSOLVED_PARTY:
			this.RefreshDisplay();
			break;
		}
		this.UpdateDataModelData();
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00023EDC File Offset: 0x000220DC
	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		List<BnetPlayer> list = new List<BnetPlayer>();
		foreach (BnetPlayerChange bnetPlayerChange in changelist.GetChanges())
		{
			BnetPlayer player = bnetPlayerChange.GetPlayer();
			list.Add(player);
		}
		this.UpdateChangedPlayersFromPresenceUpdate(list);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00023F44 File Offset: 0x00022144
	private void OnNearbyPlayersUpdated(BnetNearbyPlayerChangelist changelist, object userData)
	{
		this.UpdateChangedPlayersFromPresenceUpdate(changelist.GetUpdatedStrangers());
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00023F54 File Offset: 0x00022154
	private void UpdateChangedPlayersFromPresenceUpdate(List<BnetPlayer> changedPlayers)
	{
		if (changedPlayers == null)
		{
			return;
		}
		bool flag = false;
		foreach (BnetPlayer bnetPlayer in changedPlayers)
		{
			if (bnetPlayer == BnetPresenceMgr.Get().GetMyPlayer() && bnetPlayer.IsAppearingOffline())
			{
				this.LeaveParty();
				this.UpdateDataModelData();
				return;
			}
			if (PartyManager.Get().IsPlayerInCurrentPartyOrPending(bnetPlayer.GetBestGameAccountId()))
			{
				for (int i = 0; i < this.m_memberInfo.Count; i++)
				{
					if (this.m_memberInfo[i].playerGameAccountId == bnetPlayer.GetBestGameAccountId())
					{
						this.m_memberInfo[i].status = this.GetReadyStatusForPartyMember(this.m_memberInfo[i].playerGameAccountId);
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			this.RefreshVisuals();
		}
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x00024048 File Offset: 0x00022248
	private void OnSpectateRejected()
	{
		this.CleanUpSpectateFx();
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x00024050 File Offset: 0x00022250
	private void OnPartyPanelReady(Widget controller)
	{
		this.m_partyPanel = controller;
		this.m_panelLoaded = true;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00024060 File Offset: 0x00022260
	private void OnMemberReady(Widget widget, int index)
	{
		this.m_memberWidgetByWidgetIndex.Add(index, widget);
		this.m_members[index] = widget;
		widget.RegisterEventListener(delegate(string s)
		{
			this.OnPartyMemberEvent(index, s);
		});
		this.m_membersLoaded++;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x000240C8 File Offset: 0x000222C8
	private void OnPartyMemberEvent(int index, string eventString)
	{
		if (!this.m_memberWidgetByWidgetIndex.ContainsKey(index))
		{
			Debug.LogErrorFormat("OnPartyMemberEvent() - No party member widget at index {0}", new object[]
			{
				index
			});
			return;
		}
		if (eventString == "SPECTATE_BUTTON_PRESSED")
		{
			Widget item = this.m_memberWidgetByWidgetIndex[index];
			int num = this.m_members.IndexOf(item);
			if (num == -1)
			{
				Debug.LogErrorFormat("OnPartyMemberEvent() - Widget at index {0} not found in m_members list.", new object[]
				{
					index
				});
				return;
			}
			this.m_ClickBlocker.SetActive(true);
			BnetGameAccountId playerGameAccountId = this.m_memberInfo[num].playerGameAccountId;
			base.StartCoroutine(this.SpectatePlayerWithAnimations(playerGameAccountId));
		}
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00024170 File Offset: 0x00022370
	public BaconPartyDataModel GetBaconPartyDataModel()
	{
		IDataModel dataModel;
		if (!this.m_partyPanel.GetDataModel(154, out dataModel))
		{
			dataModel = new BaconPartyDataModel();
			this.m_partyPanel.BindDataModel(dataModel, false);
		}
		return dataModel as BaconPartyDataModel;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x000241AC File Offset: 0x000223AC
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
				if (m2 == myselfGameAccountId)
				{
					return 1;
				}
				return 0;
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
				this.m_memberInfo[i].status = this.GetReadyStatusForPartyMember(bnetGameAccountId);
				this.m_memberInfo[i].playerGameAccountId = bnetGameAccountId;
			}
			else if (bnetGameAccountId != null)
			{
				this.m_memberInfo[i].status = BaconParty.Status.Waiting;
				this.m_memberInfo[i].playerGameAccountId = bnetGameAccountId;
			}
			else
			{
				this.m_memberInfo[i].status = BaconParty.Status.Inactive;
				this.m_memberInfo[i].playerGameAccountId = null;
			}
		}
		this.RefreshVisuals();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x000242D8 File Offset: 0x000224D8
	private void UpdateDataModelData()
	{
		if (!this.IsLoadedAndReady())
		{
			return;
		}
		BaconPartyDataModel baconPartyDataModel = this.GetBaconPartyDataModel();
		baconPartyDataModel.Active = PartyManager.Get().IsInBattlegroundsParty();
		baconPartyDataModel.Size = PartyManager.Get().GetCurrentPartySize();
		baconPartyDataModel.PrivateGame = (baconPartyDataModel.Size > PartyManager.Get().GetBattlegroundsMaxRankedPartySize());
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002432B File Offset: 0x0002252B
	public void LeaveParty()
	{
		PartyManager.Get().LeaveParty();
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00024338 File Offset: 0x00022538
	private void AddPartyMember(BnetGameAccountId playerGameAccountId, bool isReady = false)
	{
		if (PartyManager.Get().GetCurrentPartySize() > PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			return;
		}
		BaconParty.AnimatedEvent animatedEvent = new BaconParty.AnimatedEvent();
		animatedEvent.type = BaconParty.Event.Add;
		animatedEvent.playerGameAccountId = playerGameAccountId;
		animatedEvent.isReady = isReady;
		this.m_animQueue.Enqueue(animatedEvent);
		this.Animate();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00024384 File Offset: 0x00022584
	private void Animate()
	{
		if (this.m_animating)
		{
			return;
		}
		if (this.m_animQueue.Count == 0)
		{
			return;
		}
		BaconParty.AnimatedEvent animatedEvent = this.m_animQueue.Dequeue();
		BaconParty.Event type = animatedEvent.type;
		if (type == BaconParty.Event.Add)
		{
			base.StartCoroutine(this.AddPartyMemberWithAnims(animatedEvent.playerGameAccountId, animatedEvent.isReady));
			return;
		}
		if (type != BaconParty.Event.Remove)
		{
			return;
		}
		base.StartCoroutine(this.RemovePartyMemberWithAnims(animatedEvent.playerGameAccountId));
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x000243F0 File Offset: 0x000225F0
	private IEnumerator AddPartyMemberWithAnims(BnetGameAccountId playerGameAccountId, bool isReady)
	{
		this.m_animating = true;
		while (!this.IsLoadedAndReady())
		{
			yield return null;
		}
		int num = -1;
		for (int i = 0; i < this.m_memberInfo.Count; i++)
		{
			if (this.m_memberInfo[i].status == BaconParty.Status.Inactive)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			global::Log.Party.PrintError("AddPartyMemberWithAnims - No inactive members, unable to add new member.", Array.Empty<object>());
			this.m_animating = false;
			this.Animate();
			yield break;
		}
		this.m_memberInfo[num].status = (isReady ? this.GetReadyStatusForPartyMember(playerGameAccountId) : BaconParty.Status.Waiting);
		this.m_memberInfo[num].playerGameAccountId = playerGameAccountId;
		this.RefreshVisuals();
		yield return new WaitForSeconds(0.5f);
		this.m_animating = false;
		this.Animate();
		yield break;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002440D File Offset: 0x0002260D
	private void RemovePartyMember(BnetGameAccountId playerGameAccountId)
	{
		if (PartyManager.Get().GetCurrentPartySize() == 1)
		{
			this.LeaveParty();
			return;
		}
		this.m_animQueue.Enqueue(new BaconParty.AnimatedEvent
		{
			type = BaconParty.Event.Remove,
			playerGameAccountId = playerGameAccountId
		});
		this.Animate();
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00024447 File Offset: 0x00022647
	private IEnumerator RemovePartyMemberWithAnims(BnetGameAccountId playerGameAccountId)
	{
		this.m_animating = true;
		while (!this.IsLoadedAndReady())
		{
			yield return null;
		}
		int index = this.GetIndexOfPartyMemberFromGameAccountId(playerGameAccountId);
		if (index < 0 || index >= PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			global::Log.Party.PrintError("RemovePartyMemberWithAnims() - Unable to find party member with id {0}.", new object[]
			{
				playerGameAccountId
			});
			this.m_animating = false;
			this.Animate();
			yield break;
		}
		this.m_members[index].TriggerEvent("Leave", default(Widget.TriggerEventParameters));
		yield return new WaitForSeconds(0.5f);
		Vector3[] positions = new Vector3[PartyManager.BATTLEGROUNDS_PARTY_LIMIT];
		for (int i = index; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			positions[i] = new Vector3(this.m_members[i].gameObject.transform.localPosition.x, this.m_members[i].gameObject.transform.localPosition.y, this.m_members[i].gameObject.transform.localPosition.z);
		}
		for (int j = index + 1; j < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; j++)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["easetype"] = "easeOutBounce";
			hashtable["position"] = positions[j - 1];
			hashtable["islocal"] = true;
			hashtable["time"] = 0.5f;
			iTween.MoveTo(this.m_members[j].gameObject, hashtable);
		}
		yield return new WaitForSeconds(0.5f);
		this.m_memberInfo.RemoveAt(index);
		BaconParty.BaconPartyMemberInfo baconPartyMemberInfo = new BaconParty.BaconPartyMemberInfo();
		baconPartyMemberInfo.status = BaconParty.Status.Inactive;
		this.m_memberInfo.Add(baconPartyMemberInfo);
		Widget widget = this.m_members[index];
		this.m_members.RemoveAt(index);
		this.m_members.Add(widget);
		widget.transform.localPosition = positions[PartyManager.BATTLEGROUNDS_PARTY_LIMIT - 1];
		this.m_animating = false;
		this.Animate();
		yield break;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00024460 File Offset: 0x00022660
	private int GetIndexOfPartyMemberFromGameAccountId(BnetGameAccountId playerGameAccountId)
	{
		for (int i = 0; i < PartyManager.BATTLEGROUNDS_PARTY_LIMIT; i++)
		{
			if (this.m_memberInfo[i] != null && this.m_memberInfo[i].playerGameAccountId == playerGameAccountId)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x000244A8 File Offset: 0x000226A8
	private void SetReady(BnetGameAccountId playerGameAccountId)
	{
		int num = -1;
		for (int i = 0; i < this.m_memberInfo.Count; i++)
		{
			if (this.m_memberInfo[i].playerGameAccountId == playerGameAccountId)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			this.AddPartyMember(playerGameAccountId, true);
			return;
		}
		if (num <= 0 || num >= PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			return;
		}
		this.m_memberInfo[num].status = BaconParty.Status.Ready;
		this.m_members[num].TriggerEvent(BaconParty.Status.Ready.ToString(), default(Widget.TriggerEventParameters));
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00024540 File Offset: 0x00022740
	private void RefreshVisuals()
	{
		for (int i = 0; i < this.m_members.Count; i++)
		{
			this.m_members[i].TriggerEvent(this.m_memberInfo[i].status.ToString(), default(Widget.TriggerEventParameters));
			if (this.m_memberInfo[i].playerGameAccountId != null)
			{
				string partyMemberName = PartyManager.Get().GetPartyMemberName(this.m_memberInfo[i].playerGameAccountId);
				this.m_members[i].transform.Find("BaconPartyMember/Root/Name").gameObject.GetComponent<UberText>().Text = partyMemberName;
			}
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00024600 File Offset: 0x00022800
	private BaconParty.Status GetReadyStatusForPartyMember(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		bool flag = BnetFriendMgr.Get().IsFriend(player) || BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player);
		if (playerGameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			if (!PartyManager.Get().IsPartyLeader())
			{
				return BaconParty.Status.Ready;
			}
			return BaconParty.Status.Leader;
		}
		else
		{
			if (!PartyManager.Get().IsPlayerInCurrentParty(playerGameAccountId))
			{
				return BaconParty.Status.NotReady;
			}
			if (PartyManager.Get().CanSpectatePartyMember(playerGameAccountId))
			{
				return BaconParty.Status.Spectate;
			}
			if (PartyManager.Get().GetLeader() == playerGameAccountId)
			{
				if (!FriendChallengeMgr.Get().IsOpponentAvailable(player))
				{
					return BaconParty.Status.NotReady;
				}
				return BaconParty.Status.Leader;
			}
			else
			{
				if (player == null || !flag)
				{
					return BaconParty.Status.Ready;
				}
				if (FriendChallengeMgr.Get().IsOpponentAvailable(player))
				{
					return BaconParty.Status.Ready;
				}
				if (Network.Get().IsFindingGame())
				{
					return BaconParty.Status.Ready;
				}
				return BaconParty.Status.NotReady;
			}
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x000246B9 File Offset: 0x000228B9
	private IEnumerator SpectatePlayerWithAnimations(BnetGameAccountId playerGameAccountId)
	{
		float num = 0.25f;
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, num, iTween.EaseType.linear, null);
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
			this.CleanUpSpectateFx();
		}
		this.m_ClickBlocker.SetActive(false);
		yield break;
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x000246CF File Offset: 0x000228CF
	private void CleanUpSpectateFx()
	{
		FullScreenFXMgr.Get().StopAllEffects(0f);
	}

	// Token: 0x0400043F RID: 1087
	public AsyncReference m_PartyPanelReference;

	// Token: 0x04000440 RID: 1088
	public AsyncReference m_Member0;

	// Token: 0x04000441 RID: 1089
	public AsyncReference m_Member1;

	// Token: 0x04000442 RID: 1090
	public AsyncReference m_Member2;

	// Token: 0x04000443 RID: 1091
	public AsyncReference m_Member3;

	// Token: 0x04000444 RID: 1092
	public AsyncReference m_Member4;

	// Token: 0x04000445 RID: 1093
	public AsyncReference m_Member5;

	// Token: 0x04000446 RID: 1094
	public AsyncReference m_Member6;

	// Token: 0x04000447 RID: 1095
	public AsyncReference m_Member7;

	// Token: 0x04000448 RID: 1096
	public GameObject m_ClickBlocker;

	// Token: 0x04000449 RID: 1097
	private Widget m_partyPanel;

	// Token: 0x0400044A RID: 1098
	private List<Widget> m_members;

	// Token: 0x0400044B RID: 1099
	private Dictionary<int, Widget> m_memberWidgetByWidgetIndex = new Dictionary<int, Widget>();

	// Token: 0x0400044C RID: 1100
	private List<BaconParty.BaconPartyMemberInfo> m_memberInfo;

	// Token: 0x0400044D RID: 1101
	private Queue<BaconParty.AnimatedEvent> m_animQueue;

	// Token: 0x0400044E RID: 1102
	private bool m_animating;

	// Token: 0x0400044F RID: 1103
	private bool m_panelLoaded;

	// Token: 0x04000450 RID: 1104
	private int m_membersLoaded;

	// Token: 0x04000451 RID: 1105
	private static BaconParty s_instance;

	// Token: 0x02001364 RID: 4964
	private class BaconPartyMemberInfo
	{
		// Token: 0x0400A699 RID: 42649
		public BaconParty.Status status;

		// Token: 0x0400A69A RID: 42650
		public BnetGameAccountId playerGameAccountId;
	}

	// Token: 0x02001365 RID: 4965
	public enum Status
	{
		// Token: 0x0400A69C RID: 42652
		Inactive,
		// Token: 0x0400A69D RID: 42653
		Waiting,
		// Token: 0x0400A69E RID: 42654
		Ready,
		// Token: 0x0400A69F RID: 42655
		Leader,
		// Token: 0x0400A6A0 RID: 42656
		Leave,
		// Token: 0x0400A6A1 RID: 42657
		NotReady,
		// Token: 0x0400A6A2 RID: 42658
		Spectate
	}

	// Token: 0x02001366 RID: 4966
	private class AnimatedEvent
	{
		// Token: 0x0400A6A3 RID: 42659
		public BaconParty.Event type;

		// Token: 0x0400A6A4 RID: 42660
		public BnetGameAccountId playerGameAccountId;

		// Token: 0x0400A6A5 RID: 42661
		public bool isReady;
	}

	// Token: 0x02001367 RID: 4967
	private enum Event
	{
		// Token: 0x0400A6A7 RID: 42663
		Add,
		// Token: 0x0400A6A8 RID: 42664
		Remove,
		// Token: 0x0400A6A9 RID: 42665
		Ready
	}
}
