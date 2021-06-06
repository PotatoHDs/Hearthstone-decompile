using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class FiresideGatheringPresenceManager
{
	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x06002808 RID: 10248 RVA: 0x000C9195 File Offset: 0x000C7395
	private int CurrentSubscribedPlayerCount
	{
		get
		{
			return this.m_subscribedPatronList.Count;
		}
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x000C91A2 File Offset: 0x000C73A2
	public static FiresideGatheringPresenceManager Get()
	{
		if (FiresideGatheringPresenceManager.s_instance == null)
		{
			FiresideGatheringPresenceManager.s_instance = new FiresideGatheringPresenceManager();
			if (FiresideGatheringPresenceManager.IsRequestBattleTagEnabled)
			{
				BnetPresenceMgr.Get().OnGameAccountPresenceChange += FiresideGatheringPresenceManager.s_instance.BnetPresenceMgr_OnGameAccountPresenceChange;
			}
		}
		return FiresideGatheringPresenceManager.s_instance;
	}

	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x0600280A RID: 10250 RVA: 0x000C91DC File Offset: 0x000C73DC
	public static int MAX_SUBSCRIBED_PATRONS
	{
		get
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject == null || netObject.FsgMaxPresencePubscribedPatronCount < 0)
			{
				return 100;
			}
			return netObject.FsgMaxPresencePubscribedPatronCount;
		}
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x0600280B RID: 10251 RVA: 0x000C9209 File Offset: 0x000C7409
	public static int PERIODIC_SUBSCRIBE_CHECK_SECONDS
	{
		get
		{
			return Vars.Key("FSG.PeriodicPrunePatronOldSubscriptionsSeconds").GetInt(5);
		}
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x0600280C RID: 10252 RVA: 0x000C921B File Offset: 0x000C741B
	public static long PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS
	{
		get
		{
			return Vars.Key("FSG.PatronOldSubscriptionThresholdSeconds").GetLong(15L);
		}
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x0600280D RID: 10253 RVA: 0x000C922F File Offset: 0x000C742F
	public static bool IsRequestBattleTagEnabled
	{
		get
		{
			return FiresideGatheringPresenceManager.IsVerboseLogging && HearthstoneApplication.IsInternal();
		}
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x0600280E RID: 10254 RVA: 0x000C923F File Offset: 0x000C743F
	public static bool IsVerboseLogging
	{
		get
		{
			return Vars.Key("FSG.PresenceSubscriptionsVerboseLog").GetBool(false);
		}
	}

	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x0600280F RID: 10255 RVA: 0x000C9251 File Offset: 0x000C7451
	public static bool IsVerboseLoggingToScreen
	{
		get
		{
			return Vars.Key("FSG.PresenceSubscriptionsVerboseLogToScreen").GetBool(false);
		}
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x000C9263 File Offset: 0x000C7463
	public static bool IsDisplayable(BnetPlayer player)
	{
		return player != null && player.IsDisplayable() && player.IsOnline() && !(player.GetBestProgramId() != BnetProgramId.HEARTHSTONE);
	}

	// Token: 0x06002811 RID: 10257 RVA: 0x000C9294 File Offset: 0x000C7494
	private FiresideGatheringPresenceManager.PreviouslySubscribedPatron UpdatePreviouslySubscribedPatron(BnetGameAccountId gameAccountId, long timestamp)
	{
		FiresideGatheringPresenceManager.PreviouslySubscribedPatron previouslySubscribedPatron;
		if (!this.m_previouslySubscribedPatrons.TryGetValue(gameAccountId, out previouslySubscribedPatron))
		{
			previouslySubscribedPatron = new FiresideGatheringPresenceManager.PreviouslySubscribedPatron();
			this.m_previouslySubscribedPatrons.Add(gameAccountId, previouslySubscribedPatron);
		}
		previouslySubscribedPatron.m_lastSubscribeUnixTimestamp = timestamp;
		return previouslySubscribedPatron;
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x000C92CC File Offset: 0x000C74CC
	private static void PrintLog(string format, params object[] args)
	{
		string text = string.Format(format, args);
		global::Log.FiresideGatherings.PrintInfo(text, Array.Empty<object>());
		if (HearthstoneApplication.IsInternal() && FiresideGatheringPresenceManager.IsVerboseLoggingToScreen)
		{
			float num = 5f * Time.timeScale;
			if (InputCollection.GetKey(KeyCode.LeftShift) || InputCollection.GetKey(KeyCode.LeftControl) || Input.touchCount >= 2)
			{
				num *= 2f;
				if ((InputCollection.GetKey(KeyCode.LeftShift) && InputCollection.GetKey(KeyCode.LeftControl)) || Input.touchCount >= 3)
				{
					num *= 2f;
				}
			}
			UIStatus.Get().AddInfo(text, num);
		}
	}

	// Token: 0x06002813 RID: 10259 RVA: 0x000C9368 File Offset: 0x000C7568
	public void AddRemovePatronSubscriptions(List<FSGPatron> addedPatrons, List<FSGPatron> removedPatrons)
	{
		BnetGameAccountId bnetGameAccountId = (BnetPresenceMgr.Get() == null) ? null : BnetPresenceMgr.Get().GetMyGameAccountId();
		ulong num = (bnetGameAccountId == null) ? 0UL : bnetGameAccountId.GetLo();
		List<FSGPatron> patronsSubscribed = null;
		List<FSGPatron> patronsUnsubscribed = null;
		Action<FSGPatron> action = null;
		Action<FSGPatron> action2 = null;
		if (FiresideGatheringPresenceManager.IsVerboseLogging)
		{
			patronsSubscribed = new List<FSGPatron>();
			patronsUnsubscribed = new List<FSGPatron>();
			action = delegate(FSGPatron p)
			{
				if (patronsSubscribed.Count < 10)
				{
					patronsSubscribed.Add(p);
				}
			};
			action2 = delegate(FSGPatron p)
			{
				if (patronsUnsubscribed.Count < 10)
				{
					patronsUnsubscribed.Add(p);
				}
			};
		}
		if (removedPatrons != null)
		{
			foreach (FSGPatron fsgpatron in removedPatrons)
			{
				if (fsgpatron != null)
				{
					BnetGameAccountId bnetGameAccountId2 = BnetGameAccountId.CreateFromNet(fsgpatron.GameAccount);
					EntityId entityId = default(EntityId);
					entityId.hi = bnetGameAccountId2.GetHi();
					entityId.lo = bnetGameAccountId2.GetLo();
					if (this.m_subscribedPatronList.Remove(bnetGameAccountId2))
					{
						BattleNet.PresenceUnsubscribe(entityId);
						BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetGameAccountId2);
						if (action2 != null)
						{
							action2(fsgpatron);
						}
					}
					this.m_previouslySubscribedPatrons.Remove(bnetGameAccountId2);
				}
			}
		}
		if (addedPatrons != null)
		{
			BnetFriendMgr bnetFriendMgr = BnetFriendMgr.Get();
			foreach (FSGPatron fsgpatron2 in addedPatrons)
			{
				if (fsgpatron2 != null)
				{
					if (this.CurrentSubscribedPlayerCount >= FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS)
					{
						break;
					}
					BnetGameAccountId bnetGameAccountId3 = BnetGameAccountId.CreateFromNet(fsgpatron2.GameAccount);
					if (bnetGameAccountId3.GetLo() != num && (bnetFriendMgr == null || !bnetFriendMgr.IsFriend(bnetGameAccountId3)) && !this.m_subscribedPatronList.ContainsKey(bnetGameAccountId3))
					{
						EntityId entityId2 = new EntityId
						{
							hi = bnetGameAccountId3.GetHi(),
							lo = bnetGameAccountId3.GetLo()
						};
						BattleNet.PresenceSubscribe(entityId2);
						long unixTimestampSeconds = global::TimeUtils.UnixTimestampSeconds;
						this.m_subscribedPatronList.Add(bnetGameAccountId3, unixTimestampSeconds);
						this.UpdatePreviouslySubscribedPatron(bnetGameAccountId3, unixTimestampSeconds);
						if (action != null)
						{
							action(fsgpatron2);
						}
						if (FiresideGatheringPresenceManager.IsRequestBattleTagEnabled)
						{
							this.RequestGameAccountBattleTag(entityId2);
						}
					}
				}
			}
		}
		if (FiresideGatheringPresenceManager.IsVerboseLogging && patronsSubscribed != null && patronsUnsubscribed != null && (patronsSubscribed.Count > 0 || patronsUnsubscribed.Count > 0))
		{
			string format = "FSGPresence patron delta added={0} removed={1}\nadded=({2})\nremoved=({3})";
			object[] array = new object[4];
			array[0] = patronsSubscribed.Count;
			array[1] = patronsUnsubscribed.Count;
			array[2] = string.Join(", ", (from p in patronsSubscribed
			select this.GetKnownPatronName(BnetGameAccountId.CreateFromNet(p.GameAccount)) into n
			orderby n
			select n).ToArray<string>());
			array[3] = string.Join(", ", (from p in patronsUnsubscribed
			select this.GetKnownPatronName(BnetGameAccountId.CreateFromNet(p.GameAccount)) into n
			orderby n
			select n).ToArray<string>());
			FiresideGatheringPresenceManager.PrintLog(format, array);
		}
	}

	// Token: 0x06002814 RID: 10260 RVA: 0x000C96C0 File Offset: 0x000C78C0
	private void UpdateLastOnlineValuesForPreviouslySubscribedPatrons()
	{
		BnetPresenceMgr bnetPresenceMgr = BnetPresenceMgr.Get();
		long unixTimestampSeconds = global::TimeUtils.UnixTimestampSeconds;
		foreach (KeyValuePair<BnetGameAccountId, FiresideGatheringPresenceManager.PreviouslySubscribedPatron> keyValuePair in this.m_previouslySubscribedPatrons)
		{
			BnetPlayer player = bnetPresenceMgr.GetPlayer(keyValuePair.Key);
			if (player != null)
			{
				BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
				if (!(hearthstoneGameAccount == null))
				{
					if (hearthstoneGameAccount.IsOnline())
					{
						keyValuePair.Value.m_lastOnlineUnixTimestamp = unixTimestampSeconds;
					}
					else if (keyValuePair.Value.m_lastOnlineUnixTimestamp != 0L && unixTimestampSeconds - keyValuePair.Value.m_lastOnlineUnixTimestamp >= FiresideGatheringPresenceManager.PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS * 2L)
					{
						keyValuePair.Value.m_lastOnlineUnixTimestamp = 0L;
					}
				}
			}
		}
	}

	// Token: 0x06002815 RID: 10261 RVA: 0x000C9790 File Offset: 0x000C7990
	private IEnumerable<BnetPlayer> GetOlderSubscribedPatronsThatAreNotDisplayable()
	{
		long now = global::TimeUtils.UnixTimestampSeconds;
		if (now - this.m_lastCheckPruneOlderPatronsUnixTimestamp < (long)FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS)
		{
			return null;
		}
		this.m_lastCheckPruneOlderPatronsUnixTimestamp = now;
		this.UpdateLastOnlineValuesForPreviouslySubscribedPatrons();
		BnetPresenceMgr presenceMgr = BnetPresenceMgr.Get();
		return from kv in this.m_subscribedPatronList
		where now - kv.Value >= FiresideGatheringPresenceManager.PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS
		select presenceMgr.GetPlayer(kv.Key) into p
		where p != null && !FiresideGatheringPresenceManager.IsDisplayable(p)
		select p;
	}

	// Token: 0x06002816 RID: 10262 RVA: 0x000C9830 File Offset: 0x000C7A30
	public void CheckForMoreSubscribeOpportunities(List<BnetPlayer> patronsNoLongerDisplayable, IEnumerable<BnetPlayer> pendingPatrons)
	{
		bool flag = patronsNoLongerDisplayable == null;
		List<BnetGameAccountId> patronsSubscribed = null;
		List<BnetGameAccountId> patronsUnsubscribed = null;
		List<BnetPlayer> patronsOldPruned = null;
		Action<BnetGameAccountId> action = null;
		Action<BnetGameAccountId> action2 = null;
		Action<BnetPlayer> action3 = null;
		if (FiresideGatheringPresenceManager.IsVerboseLogging)
		{
			patronsSubscribed = new List<BnetGameAccountId>();
			patronsUnsubscribed = new List<BnetGameAccountId>();
			patronsOldPruned = new List<BnetPlayer>();
			action = delegate(BnetGameAccountId p)
			{
				if (patronsSubscribed.Count < 10)
				{
					patronsSubscribed.Add(p);
				}
			};
			action2 = delegate(BnetGameAccountId p)
			{
				if (patronsUnsubscribed.Count < 10)
				{
					patronsUnsubscribed.Add(p);
				}
			};
			action3 = delegate(BnetPlayer p)
			{
				if (patronsOldPruned.Count < 10)
				{
					patronsOldPruned.Add(p);
				}
			};
		}
		List<BnetPlayer> list = (patronsNoLongerDisplayable == null) ? new List<BnetPlayer>() : new List<BnetPlayer>(patronsNoLongerDisplayable);
		IEnumerable<BnetPlayer> enumerable = flag ? this.GetOlderSubscribedPatronsThatAreNotDisplayable() : null;
		if (enumerable != null)
		{
			int count = list.Count;
			list.AddRange(enumerable);
			if (action3 != null)
			{
				enumerable.Take(10).ForEach(action3);
			}
		}
		if (list.Count == 0 && this.CurrentSubscribedPlayerCount >= FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS)
		{
			return;
		}
		BnetGameAccountId bnetGameAccountId = (BnetPresenceMgr.Get() == null) ? null : BnetPresenceMgr.Get().GetMyGameAccountId();
		ulong myselfGameAccountIdLo = (bnetGameAccountId == null) ? 0UL : bnetGameAccountId.GetLo();
		HashSet<EntityId> hashSet = null;
		foreach (BnetPlayer bnetPlayer in list)
		{
			if (bnetPlayer != null)
			{
				BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
				if (!(hearthstoneGameAccountId == null) && this.m_subscribedPatronList.ContainsKey(hearthstoneGameAccountId))
				{
					EntityId item = default(EntityId);
					item.hi = hearthstoneGameAccountId.GetHi();
					item.lo = hearthstoneGameAccountId.GetLo();
					if (hashSet == null)
					{
						hashSet = new HashSet<EntityId>();
					}
					hashSet.Add(item);
				}
			}
		}
		if (this.CurrentSubscribedPlayerCount - ((hashSet == null) ? 0 : hashSet.Count) < FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS)
		{
			BnetFriendMgr friendMgr = BnetFriendMgr.Get();
			List<BnetPlayer> list2 = (pendingPatrons == null) ? new List<BnetPlayer>() : pendingPatrons.Where(delegate(BnetPlayer p)
			{
				BnetGameAccountId bnetGameAccountId3 = (p == null) ? null : p.GetHearthstoneGameAccountId();
				return p != null && !(bnetGameAccountId3 == null) && bnetGameAccountId3.GetLo() != myselfGameAccountIdLo && (friendMgr == null || !friendMgr.IsFriend(bnetGameAccountId3)) && !this.m_subscribedPatronList.ContainsKey(bnetGameAccountId3);
			}).ToList<BnetPlayer>();
			this.SortPotentialPatronForSubscription(list2);
			foreach (BnetPlayer bnetPlayer2 in list2)
			{
				if (bnetPlayer2 != null)
				{
					if (this.CurrentSubscribedPlayerCount - ((hashSet == null) ? 0 : hashSet.Count) >= FiresideGatheringPresenceManager.MAX_SUBSCRIBED_PATRONS)
					{
						break;
					}
					BnetGameAccountId hearthstoneGameAccountId2 = bnetPlayer2.GetHearthstoneGameAccountId();
					if (!(hearthstoneGameAccountId2 == null))
					{
						EntityId entityId = default(EntityId);
						entityId.hi = hearthstoneGameAccountId2.GetHi();
						entityId.lo = hearthstoneGameAccountId2.GetLo();
						bool flag2 = this.m_subscribedPatronList.ContainsKey(hearthstoneGameAccountId2);
						bool flag3 = hashSet != null && hashSet.Contains(entityId);
						if (!flag2 || flag3)
						{
							if (flag3)
							{
								hashSet.Remove(entityId);
							}
							if (!flag2)
							{
								BattleNet.PresenceSubscribe(entityId);
								long unixTimestampSeconds = global::TimeUtils.UnixTimestampSeconds;
								this.m_subscribedPatronList.Add(hearthstoneGameAccountId2, unixTimestampSeconds);
								this.UpdatePreviouslySubscribedPatron(hearthstoneGameAccountId2, unixTimestampSeconds);
								if (action != null)
								{
									action(hearthstoneGameAccountId2);
								}
								if (FiresideGatheringPresenceManager.IsRequestBattleTagEnabled)
								{
									this.RequestGameAccountBattleTag(entityId);
								}
							}
						}
					}
				}
			}
		}
		if (hashSet != null)
		{
			foreach (EntityId entityId2 in hashSet)
			{
				BnetGameAccountId bnetGameAccountId2 = BnetGameAccountId.CreateFromEntityId(entityId2);
				this.m_subscribedPatronList.Remove(bnetGameAccountId2);
				BattleNet.PresenceUnsubscribe(entityId2);
				BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetGameAccountId2);
				if (action2 != null)
				{
					action2(bnetGameAccountId2);
				}
			}
		}
		if (FiresideGatheringPresenceManager.IsVerboseLogging && patronsSubscribed != null && patronsUnsubscribed != null && (patronsSubscribed.Count != 0 || patronsUnsubscribed.Count != 0))
		{
			int num = (pendingPatrons == null) ? 0 : pendingPatrons.Count<BnetPlayer>();
			string format = "FSGPresence {0} newSubscribe={1} old={2} unsubscribed={3} total={4}\nnew=({5})\nold=({6})\nunsubscribed=({7})";
			object[] array = new object[8];
			array[0] = (flag ? "periodic" : "update");
			array[1] = patronsSubscribed.Count;
			array[2] = ((patronsOldPruned == null) ? 0 : patronsOldPruned.Count);
			array[3] = patronsUnsubscribed.Count;
			array[4] = num;
			array[5] = string.Join(", ", (from id in patronsSubscribed
			select this.GetKnownPatronName(id) into n
			orderby n
			select n).ToArray<string>());
			int num2 = 6;
			object obj;
			if (patronsOldPruned != null)
			{
				obj = string.Join(", ", (from n in patronsOldPruned.Select(delegate(BnetPlayer p)
				{
					if (!(p.GetHearthstoneGameAccountId() == null))
					{
						return this.GetKnownPatronName(p.GetHearthstoneGameAccountId());
					}
					return string.Empty;
				})
				orderby n
				select n).ToArray<string>());
			}
			else
			{
				obj = "";
			}
			array[num2] = obj;
			array[7] = string.Join(", ", (from id in patronsUnsubscribed
			select this.GetKnownPatronName(id) into n
			orderby n
			select n).ToArray<string>());
			FiresideGatheringPresenceManager.PrintLog(format, array);
		}
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x000C9D90 File Offset: 0x000C7F90
	private void SortPotentialPatronForSubscription(List<BnetPlayer> potentialPatrons)
	{
		GeneralUtils.Shuffle<BnetPlayer>(potentialPatrons);
		potentialPatrons.Sort(delegate(BnetPlayer a, BnetPlayer b)
		{
			BnetGameAccountId hearthstoneGameAccountId = a.GetHearthstoneGameAccountId();
			BnetGameAccountId hearthstoneGameAccountId2 = b.GetHearthstoneGameAccountId();
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			FiresideGatheringPresenceManager.PreviouslySubscribedPatron previouslySubscribedPatron;
			if (this.m_previouslySubscribedPatrons.TryGetValue(hearthstoneGameAccountId, out previouslySubscribedPatron))
			{
				num = previouslySubscribedPatron.m_lastSubscribeUnixTimestamp;
				num3 = previouslySubscribedPatron.m_lastOnlineUnixTimestamp;
			}
			FiresideGatheringPresenceManager.PreviouslySubscribedPatron previouslySubscribedPatron2;
			if (this.m_previouslySubscribedPatrons.TryGetValue(hearthstoneGameAccountId2, out previouslySubscribedPatron2))
			{
				num2 = previouslySubscribedPatron2.m_lastSubscribeUnixTimestamp;
				num4 = previouslySubscribedPatron2.m_lastOnlineUnixTimestamp;
			}
			if (num != num2)
			{
				if (num == 0L)
				{
					return -1;
				}
				if (num2 == 0L)
				{
					return 1;
				}
			}
			if (num3 != num4)
			{
				if (num3 == 0L)
				{
					return 1;
				}
				if (num4 == 0L)
				{
					return -1;
				}
				return num4.CompareTo(num3);
			}
			else
			{
				if (num != num2)
				{
					return num.CompareTo(num2);
				}
				return 0;
			}
		});
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x000C9DAC File Offset: 0x000C7FAC
	public void RequestGameAccountBattleTag(EntityId patronEntity)
	{
		BattleNet.RequestPresenceFields(true, patronEntity, new PresenceFieldKey[]
		{
			new PresenceFieldKey
			{
				programId = BnetProgramId.BNET.GetValue(),
				groupId = 2U,
				fieldId = 5U,
				uniqueId = 0UL
			}
		});
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x000C9E00 File Offset: 0x000C8000
	private void BnetPresenceMgr_OnGameAccountPresenceChange(PresenceUpdate[] updates)
	{
		long unixTimestampSeconds = global::TimeUtils.UnixTimestampSeconds;
		foreach (PresenceUpdate presenceUpdate in updates)
		{
			if (presenceUpdate.programId == BnetProgramId.BNET.GetValue() && presenceUpdate.groupId == 2U && (presenceUpdate.fieldId == 5U || presenceUpdate.fieldId == 1U))
			{
				BnetGameAccountId key = BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId);
				if (this.m_previouslySubscribedPatrons.ContainsKey(key))
				{
					if (presenceUpdate.fieldId == 5U)
					{
						if (this.m_previouslySubscribedPatronBattleTags == null)
						{
							this.m_previouslySubscribedPatronBattleTags = new global::Map<BnetGameAccountId, string>();
						}
						this.m_previouslySubscribedPatronBattleTags[key] = presenceUpdate.stringVal;
					}
					else if (presenceUpdate.fieldId == 1U && presenceUpdate.boolVal)
					{
						this.m_previouslySubscribedPatrons[key].m_lastOnlineUnixTimestamp = unixTimestampSeconds;
					}
				}
			}
		}
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x000C9ED8 File Offset: 0x000C80D8
	private string GetKnownPatronName(BnetGameAccountId gameAccountId)
	{
		string result;
		if (this.m_previouslySubscribedPatronBattleTags != null && this.m_previouslySubscribedPatronBattleTags.TryGetValue(gameAccountId, out result))
		{
			return result;
		}
		return gameAccountId.GetLo().ToString();
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x000C9F10 File Offset: 0x000C8110
	public void ClearSubscribedPatrons()
	{
		foreach (KeyValuePair<BnetGameAccountId, long> keyValuePair in this.m_subscribedPatronList)
		{
			EntityId entityId;
			entityId.hi = keyValuePair.Key.GetHi();
			entityId.lo = keyValuePair.Key.GetLo();
			BattleNet.PresenceUnsubscribe(entityId);
			BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(BnetGameAccountId.CreateFromEntityId(entityId));
		}
		this.m_subscribedPatronList.Clear();
		this.m_previouslySubscribedPatrons.Clear();
		if (this.m_previouslySubscribedPatronBattleTags != null)
		{
			this.m_previouslySubscribedPatronBattleTags.Clear();
		}
	}

	// Token: 0x040016C2 RID: 5826
	private static FiresideGatheringPresenceManager s_instance;

	// Token: 0x040016C3 RID: 5827
	private global::Map<BnetGameAccountId, long> m_subscribedPatronList = new global::Map<BnetGameAccountId, long>();

	// Token: 0x040016C4 RID: 5828
	private global::Map<BnetGameAccountId, FiresideGatheringPresenceManager.PreviouslySubscribedPatron> m_previouslySubscribedPatrons = new global::Map<BnetGameAccountId, FiresideGatheringPresenceManager.PreviouslySubscribedPatron>();

	// Token: 0x040016C5 RID: 5829
	private global::Map<BnetGameAccountId, string> m_previouslySubscribedPatronBattleTags;

	// Token: 0x040016C6 RID: 5830
	private long m_lastCheckPruneOlderPatronsUnixTimestamp = -1L;

	// Token: 0x040016C7 RID: 5831
	private const int MAX_PATRONS_TO_LOG = 10;

	// Token: 0x0200161B RID: 5659
	private class PreviouslySubscribedPatron
	{
		// Token: 0x0400AFD1 RID: 45009
		public long m_lastSubscribeUnixTimestamp;

		// Token: 0x0400AFD2 RID: 45010
		public long m_lastOnlineUnixTimestamp;
	}
}
