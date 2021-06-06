using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using Hearthstone;
using PegasusShared;
using UnityEngine;

public class FiresideGatheringPresenceManager
{
	private class PreviouslySubscribedPatron
	{
		public long m_lastSubscribeUnixTimestamp;

		public long m_lastOnlineUnixTimestamp;
	}

	private static FiresideGatheringPresenceManager s_instance;

	private Map<BnetGameAccountId, long> m_subscribedPatronList = new Map<BnetGameAccountId, long>();

	private Map<BnetGameAccountId, PreviouslySubscribedPatron> m_previouslySubscribedPatrons = new Map<BnetGameAccountId, PreviouslySubscribedPatron>();

	private Map<BnetGameAccountId, string> m_previouslySubscribedPatronBattleTags;

	private long m_lastCheckPruneOlderPatronsUnixTimestamp = -1L;

	private const int MAX_PATRONS_TO_LOG = 10;

	private int CurrentSubscribedPlayerCount => m_subscribedPatronList.Count;

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

	public static int PERIODIC_SUBSCRIBE_CHECK_SECONDS => Vars.Key("FSG.PeriodicPrunePatronOldSubscriptionsSeconds").GetInt(5);

	public static long PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS => Vars.Key("FSG.PatronOldSubscriptionThresholdSeconds").GetLong(15L);

	public static bool IsRequestBattleTagEnabled
	{
		get
		{
			if (IsVerboseLogging)
			{
				return HearthstoneApplication.IsInternal();
			}
			return false;
		}
	}

	public static bool IsVerboseLogging => Vars.Key("FSG.PresenceSubscriptionsVerboseLog").GetBool(def: false);

	public static bool IsVerboseLoggingToScreen => Vars.Key("FSG.PresenceSubscriptionsVerboseLogToScreen").GetBool(def: false);

	public static FiresideGatheringPresenceManager Get()
	{
		if (s_instance == null)
		{
			s_instance = new FiresideGatheringPresenceManager();
			if (IsRequestBattleTagEnabled)
			{
				BnetPresenceMgr.Get().OnGameAccountPresenceChange += s_instance.BnetPresenceMgr_OnGameAccountPresenceChange;
			}
		}
		return s_instance;
	}

	public static bool IsDisplayable(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (!player.IsDisplayable())
		{
			return false;
		}
		if (!player.IsOnline())
		{
			return false;
		}
		if (player.GetBestProgramId() != BnetProgramId.HEARTHSTONE)
		{
			return false;
		}
		return true;
	}

	private PreviouslySubscribedPatron UpdatePreviouslySubscribedPatron(BnetGameAccountId gameAccountId, long timestamp)
	{
		if (!m_previouslySubscribedPatrons.TryGetValue(gameAccountId, out var value))
		{
			value = new PreviouslySubscribedPatron();
			m_previouslySubscribedPatrons.Add(gameAccountId, value);
		}
		value.m_lastSubscribeUnixTimestamp = timestamp;
		return value;
	}

	private static void PrintLog(string format, params object[] args)
	{
		string text = string.Format(format, args);
		Log.FiresideGatherings.PrintInfo(text);
		if (!HearthstoneApplication.IsInternal() || !IsVerboseLoggingToScreen)
		{
			return;
		}
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

	public void AddRemovePatronSubscriptions(List<FSGPatron> addedPatrons, List<FSGPatron> removedPatrons)
	{
		BnetGameAccountId bnetGameAccountId = ((BnetPresenceMgr.Get() == null) ? null : BnetPresenceMgr.Get().GetMyGameAccountId());
		ulong num = ((bnetGameAccountId == null) ? 0 : bnetGameAccountId.GetLo());
		List<FSGPatron> patronsSubscribed = null;
		List<FSGPatron> patronsUnsubscribed = null;
		Action<FSGPatron> action = null;
		Action<FSGPatron> action2 = null;
		if (IsVerboseLogging)
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
			foreach (FSGPatron removedPatron in removedPatrons)
			{
				if (removedPatron != null)
				{
					BnetGameAccountId bnetGameAccountId2 = BnetGameAccountId.CreateFromNet(removedPatron.GameAccount);
					EntityId entityId = default(EntityId);
					entityId.hi = bnetGameAccountId2.GetHi();
					entityId.lo = bnetGameAccountId2.GetLo();
					if (m_subscribedPatronList.Remove(bnetGameAccountId2))
					{
						BattleNet.PresenceUnsubscribe(entityId);
						BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetGameAccountId2);
						action2?.Invoke(removedPatron);
					}
					m_previouslySubscribedPatrons.Remove(bnetGameAccountId2);
				}
			}
		}
		if (addedPatrons != null)
		{
			BnetFriendMgr bnetFriendMgr = BnetFriendMgr.Get();
			foreach (FSGPatron addedPatron in addedPatrons)
			{
				if (addedPatron == null)
				{
					continue;
				}
				if (CurrentSubscribedPlayerCount >= MAX_SUBSCRIBED_PATRONS)
				{
					break;
				}
				BnetGameAccountId bnetGameAccountId3 = BnetGameAccountId.CreateFromNet(addedPatron.GameAccount);
				if (bnetGameAccountId3.GetLo() != num && (bnetFriendMgr == null || !bnetFriendMgr.IsFriend(bnetGameAccountId3)) && !m_subscribedPatronList.ContainsKey(bnetGameAccountId3))
				{
					EntityId entityId2 = default(EntityId);
					entityId2.hi = bnetGameAccountId3.GetHi();
					entityId2.lo = bnetGameAccountId3.GetLo();
					BattleNet.PresenceSubscribe(entityId2);
					long unixTimestampSeconds = TimeUtils.UnixTimestampSeconds;
					m_subscribedPatronList.Add(bnetGameAccountId3, unixTimestampSeconds);
					UpdatePreviouslySubscribedPatron(bnetGameAccountId3, unixTimestampSeconds);
					action?.Invoke(addedPatron);
					if (IsRequestBattleTagEnabled)
					{
						RequestGameAccountBattleTag(entityId2);
					}
				}
			}
		}
		if (IsVerboseLogging && patronsSubscribed != null && patronsUnsubscribed != null && (patronsSubscribed.Count > 0 || patronsUnsubscribed.Count > 0))
		{
			PrintLog("FSGPresence patron delta added={0} removed={1}\nadded=({2})\nremoved=({3})", patronsSubscribed.Count, patronsUnsubscribed.Count, string.Join(", ", (from p in patronsSubscribed
				select GetKnownPatronName(BnetGameAccountId.CreateFromNet(p.GameAccount)) into n
				orderby n
				select n).ToArray()), string.Join(", ", (from p in patronsUnsubscribed
				select GetKnownPatronName(BnetGameAccountId.CreateFromNet(p.GameAccount)) into n
				orderby n
				select n).ToArray()));
		}
	}

	private void UpdateLastOnlineValuesForPreviouslySubscribedPatrons()
	{
		BnetPresenceMgr bnetPresenceMgr = BnetPresenceMgr.Get();
		long unixTimestampSeconds = TimeUtils.UnixTimestampSeconds;
		foreach (KeyValuePair<BnetGameAccountId, PreviouslySubscribedPatron> previouslySubscribedPatron in m_previouslySubscribedPatrons)
		{
			BnetPlayer player = bnetPresenceMgr.GetPlayer(previouslySubscribedPatron.Key);
			if (player == null)
			{
				continue;
			}
			BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
			if (!(hearthstoneGameAccount == null))
			{
				if (hearthstoneGameAccount.IsOnline())
				{
					previouslySubscribedPatron.Value.m_lastOnlineUnixTimestamp = unixTimestampSeconds;
				}
				else if (previouslySubscribedPatron.Value.m_lastOnlineUnixTimestamp != 0L && unixTimestampSeconds - previouslySubscribedPatron.Value.m_lastOnlineUnixTimestamp >= PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS * 2)
				{
					previouslySubscribedPatron.Value.m_lastOnlineUnixTimestamp = 0L;
				}
			}
		}
	}

	private IEnumerable<BnetPlayer> GetOlderSubscribedPatronsThatAreNotDisplayable()
	{
		long now = TimeUtils.UnixTimestampSeconds;
		if (now - m_lastCheckPruneOlderPatronsUnixTimestamp < PERIODIC_SUBSCRIBE_CHECK_SECONDS)
		{
			return null;
		}
		m_lastCheckPruneOlderPatronsUnixTimestamp = now;
		UpdateLastOnlineValuesForPreviouslySubscribedPatrons();
		BnetPresenceMgr presenceMgr = BnetPresenceMgr.Get();
		return from kv in m_subscribedPatronList
			where now - kv.Value >= PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS
			select presenceMgr.GetPlayer(kv.Key) into p
			where p != null && !IsDisplayable(p)
			select p;
	}

	public void CheckForMoreSubscribeOpportunities(List<BnetPlayer> patronsNoLongerDisplayable, IEnumerable<BnetPlayer> pendingPatrons)
	{
		bool flag = patronsNoLongerDisplayable == null;
		List<BnetGameAccountId> patronsSubscribed = null;
		List<BnetGameAccountId> patronsUnsubscribed = null;
		List<BnetPlayer> patronsOldPruned = null;
		Action<BnetGameAccountId> action = null;
		Action<BnetGameAccountId> action2 = null;
		Action<BnetPlayer> action3 = null;
		if (IsVerboseLogging)
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
		List<BnetPlayer> list = ((patronsNoLongerDisplayable == null) ? new List<BnetPlayer>() : new List<BnetPlayer>(patronsNoLongerDisplayable));
		IEnumerable<BnetPlayer> enumerable = (flag ? GetOlderSubscribedPatronsThatAreNotDisplayable() : null);
		if (enumerable != null)
		{
			_ = list.Count;
			list.AddRange(enumerable);
			if (action3 != null)
			{
				enumerable.Take(10).ForEach(action3);
			}
		}
		if (list.Count == 0 && CurrentSubscribedPlayerCount >= MAX_SUBSCRIBED_PATRONS)
		{
			return;
		}
		BnetGameAccountId bnetGameAccountId = ((BnetPresenceMgr.Get() == null) ? null : BnetPresenceMgr.Get().GetMyGameAccountId());
		ulong myselfGameAccountIdLo = ((bnetGameAccountId == null) ? 0 : bnetGameAccountId.GetLo());
		HashSet<EntityId> hashSet = null;
		foreach (BnetPlayer item2 in list)
		{
			if (item2 == null)
			{
				continue;
			}
			BnetGameAccountId hearthstoneGameAccountId = item2.GetHearthstoneGameAccountId();
			if (!(hearthstoneGameAccountId == null) && m_subscribedPatronList.ContainsKey(hearthstoneGameAccountId))
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
		if (CurrentSubscribedPlayerCount - (hashSet?.Count ?? 0) < MAX_SUBSCRIBED_PATRONS)
		{
			BnetFriendMgr friendMgr = BnetFriendMgr.Get();
			List<BnetPlayer> list2 = ((pendingPatrons == null) ? new List<BnetPlayer>() : pendingPatrons.Where(delegate(BnetPlayer p)
			{
				BnetGameAccountId bnetGameAccountId3 = p?.GetHearthstoneGameAccountId();
				if (p == null || bnetGameAccountId3 == null)
				{
					return false;
				}
				if (bnetGameAccountId3.GetLo() == myselfGameAccountIdLo || (friendMgr != null && friendMgr.IsFriend(bnetGameAccountId3)))
				{
					return false;
				}
				return (!m_subscribedPatronList.ContainsKey(bnetGameAccountId3)) ? true : false;
			}).ToList());
			SortPotentialPatronForSubscription(list2);
			foreach (BnetPlayer item3 in list2)
			{
				if (item3 == null)
				{
					continue;
				}
				if (CurrentSubscribedPlayerCount - (hashSet?.Count ?? 0) >= MAX_SUBSCRIBED_PATRONS)
				{
					break;
				}
				BnetGameAccountId hearthstoneGameAccountId2 = item3.GetHearthstoneGameAccountId();
				if (hearthstoneGameAccountId2 == null)
				{
					continue;
				}
				EntityId entityId = default(EntityId);
				entityId.hi = hearthstoneGameAccountId2.GetHi();
				entityId.lo = hearthstoneGameAccountId2.GetLo();
				bool flag2 = m_subscribedPatronList.ContainsKey(hearthstoneGameAccountId2);
				bool flag3 = hashSet?.Contains(entityId) ?? false;
				if (flag2 && !flag3)
				{
					continue;
				}
				if (flag3)
				{
					hashSet.Remove(entityId);
				}
				if (!flag2)
				{
					BattleNet.PresenceSubscribe(entityId);
					long unixTimestampSeconds = TimeUtils.UnixTimestampSeconds;
					m_subscribedPatronList.Add(hearthstoneGameAccountId2, unixTimestampSeconds);
					UpdatePreviouslySubscribedPatron(hearthstoneGameAccountId2, unixTimestampSeconds);
					action?.Invoke(hearthstoneGameAccountId2);
					if (IsRequestBattleTagEnabled)
					{
						RequestGameAccountBattleTag(entityId);
					}
				}
			}
		}
		if (hashSet != null)
		{
			foreach (EntityId item4 in hashSet)
			{
				BnetGameAccountId bnetGameAccountId2 = BnetGameAccountId.CreateFromEntityId(item4);
				m_subscribedPatronList.Remove(bnetGameAccountId2);
				BattleNet.PresenceUnsubscribe(item4);
				BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetGameAccountId2);
				action2?.Invoke(bnetGameAccountId2);
			}
		}
		if (IsVerboseLogging && patronsSubscribed != null && patronsUnsubscribed != null && (patronsSubscribed.Count != 0 || patronsUnsubscribed.Count != 0))
		{
			int num = pendingPatrons?.Count() ?? 0;
			PrintLog("FSGPresence {0} newSubscribe={1} old={2} unsubscribed={3} total={4}\nnew=({5})\nold=({6})\nunsubscribed=({7})", flag ? "periodic" : "update", patronsSubscribed.Count, (patronsOldPruned != null) ? patronsOldPruned.Count : 0, patronsUnsubscribed.Count, num, string.Join(", ", (from id in patronsSubscribed
				select GetKnownPatronName(id) into n
				orderby n
				select n).ToArray()), (patronsOldPruned == null) ? "" : string.Join(", ", (from p in patronsOldPruned
				select (!(p.GetHearthstoneGameAccountId() == null)) ? GetKnownPatronName(p.GetHearthstoneGameAccountId()) : string.Empty into n
				orderby n
				select n).ToArray()), string.Join(", ", (from id in patronsUnsubscribed
				select GetKnownPatronName(id) into n
				orderby n
				select n).ToArray()));
		}
	}

	private void SortPotentialPatronForSubscription(List<BnetPlayer> potentialPatrons)
	{
		GeneralUtils.Shuffle(potentialPatrons);
		potentialPatrons.Sort(delegate(BnetPlayer a, BnetPlayer b)
		{
			BnetGameAccountId hearthstoneGameAccountId = a.GetHearthstoneGameAccountId();
			BnetGameAccountId hearthstoneGameAccountId2 = b.GetHearthstoneGameAccountId();
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			if (m_previouslySubscribedPatrons.TryGetValue(hearthstoneGameAccountId, out var value))
			{
				num = value.m_lastSubscribeUnixTimestamp;
				num3 = value.m_lastOnlineUnixTimestamp;
			}
			if (m_previouslySubscribedPatrons.TryGetValue(hearthstoneGameAccountId2, out var value2))
			{
				num2 = value2.m_lastSubscribeUnixTimestamp;
				num4 = value2.m_lastOnlineUnixTimestamp;
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
			return (num != num2) ? num.CompareTo(num2) : 0;
		});
	}

	public void RequestGameAccountBattleTag(EntityId patronEntity)
	{
		PresenceFieldKey presenceFieldKey = default(PresenceFieldKey);
		presenceFieldKey.programId = BnetProgramId.BNET.GetValue();
		presenceFieldKey.groupId = 2u;
		presenceFieldKey.fieldId = 5u;
		presenceFieldKey.uniqueId = 0uL;
		BattleNet.RequestPresenceFields(isGameAccountEntityId: true, patronEntity, new PresenceFieldKey[1] { presenceFieldKey });
	}

	private void BnetPresenceMgr_OnGameAccountPresenceChange(PresenceUpdate[] updates)
	{
		long unixTimestampSeconds = TimeUtils.UnixTimestampSeconds;
		for (int i = 0; i < updates.Length; i++)
		{
			PresenceUpdate presenceUpdate = updates[i];
			if (presenceUpdate.programId != BnetProgramId.BNET.GetValue() || presenceUpdate.groupId != 2 || (presenceUpdate.fieldId != 5 && presenceUpdate.fieldId != 1))
			{
				continue;
			}
			BnetGameAccountId key = BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId);
			if (!m_previouslySubscribedPatrons.ContainsKey(key))
			{
				continue;
			}
			if (presenceUpdate.fieldId == 5)
			{
				if (m_previouslySubscribedPatronBattleTags == null)
				{
					m_previouslySubscribedPatronBattleTags = new Map<BnetGameAccountId, string>();
				}
				m_previouslySubscribedPatronBattleTags[key] = presenceUpdate.stringVal;
			}
			else if (presenceUpdate.fieldId == 1 && presenceUpdate.boolVal)
			{
				m_previouslySubscribedPatrons[key].m_lastOnlineUnixTimestamp = unixTimestampSeconds;
			}
		}
	}

	private string GetKnownPatronName(BnetGameAccountId gameAccountId)
	{
		if (m_previouslySubscribedPatronBattleTags != null && m_previouslySubscribedPatronBattleTags.TryGetValue(gameAccountId, out var value))
		{
			return value;
		}
		return gameAccountId.GetLo().ToString();
	}

	public void ClearSubscribedPatrons()
	{
		EntityId entityId = default(EntityId);
		foreach (KeyValuePair<BnetGameAccountId, long> subscribedPatron in m_subscribedPatronList)
		{
			entityId.hi = subscribedPatron.Key.GetHi();
			entityId.lo = subscribedPatron.Key.GetLo();
			BattleNet.PresenceUnsubscribe(entityId);
			BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(BnetGameAccountId.CreateFromEntityId(entityId));
		}
		m_subscribedPatronList.Clear();
		m_previouslySubscribedPatrons.Clear();
		if (m_previouslySubscribedPatronBattleTags != null)
		{
			m_previouslySubscribedPatronBattleTags.Clear();
		}
	}
}
