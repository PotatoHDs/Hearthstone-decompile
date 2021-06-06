using System;
using System.Collections.Generic;
using bgs;

// Token: 0x02000097 RID: 151
public class FriendUtils
{
	// Token: 0x06000982 RID: 2434 RVA: 0x000374A0 File Offset: 0x000356A0
	public static string GetUniqueName(BnetPlayer friend)
	{
		BnetBattleTag bnetBattleTag;
		string result;
		if (FriendUtils.GetUniqueName(friend, out bnetBattleTag, out result))
		{
			return bnetBattleTag.ToString();
		}
		return result;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x000374C4 File Offset: 0x000356C4
	public static string GetUniqueNameWithColor(BnetPlayer friend)
	{
		string text = (friend != null && friend.IsOnline()) ? "5ecaf0ff" : "999999ff";
		BnetBattleTag battleTag;
		string arg;
		if (FriendUtils.GetUniqueName(friend, out battleTag, out arg))
		{
			return FriendUtils.GetBattleTagWithColor(battleTag, text);
		}
		return string.Format("<color=#{0}>{1}</color>", text, arg);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00037509 File Offset: 0x00035709
	public static string GetBattleTagWithColor(BnetBattleTag battleTag, string nameColorStr)
	{
		return string.Format("<color=#{0}>{1}</color><color=#{2}>#{3}</color>", new object[]
		{
			nameColorStr,
			battleTag.GetName(),
			"a1a1a1ff",
			battleTag.GetNumber()
		});
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00037540 File Offset: 0x00035740
	public static string GetFriendListName(BnetPlayer friend, bool addColorTags)
	{
		string text = null;
		BnetAccount account = friend.GetAccount();
		if (account != null)
		{
			text = account.GetFullName();
			if (text == null && account.GetBattleTag() != null)
			{
				text = account.GetBattleTag().ToString();
			}
		}
		if (text == null)
		{
			foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> keyValuePair in friend.GetGameAccounts())
			{
				if (keyValuePair.Value.GetBattleTag() != null)
				{
					text = keyValuePair.Value.GetBattleTag().ToString();
					break;
				}
			}
		}
		if (addColorTags)
		{
			string arg = friend.IsOnline() ? "5ecaf0ff" : "999999ff";
			return string.Format("<color=#{0}>{1}</color>", arg, text);
		}
		return text;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00037618 File Offset: 0x00035818
	public static string GetRequestElapsedTimeString(long epochMicrosec)
	{
		global::TimeUtils.ElapsedStringSet stringSet = new global::TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_FRIENDREQUEST_SECONDS",
			m_minutes = "GLOBAL_DATETIME_FRIENDREQUEST_MINUTES",
			m_hours = "GLOBAL_DATETIME_FRIENDREQUEST_HOURS",
			m_yesterday = "GLOBAL_DATETIME_FRIENDREQUEST_DAY",
			m_days = "GLOBAL_DATETIME_FRIENDREQUEST_DAYS",
			m_weeks = "GLOBAL_DATETIME_FRIENDREQUEST_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_FRIENDREQUEST_MONTH"
		};
		return global::TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00037680 File Offset: 0x00035880
	public static string GetLastOnlineElapsedTimeString(long epochMicrosec)
	{
		if (epochMicrosec == 0L)
		{
			return GameStrings.Get("GLOBAL_OFFLINE");
		}
		global::TimeUtils.ElapsedStringSet stringSet = new global::TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_LASTONLINE_SECONDS",
			m_minutes = "GLOBAL_DATETIME_LASTONLINE_MINUTES",
			m_hours = "GLOBAL_DATETIME_LASTONLINE_HOURS",
			m_yesterday = "GLOBAL_DATETIME_LASTONLINE_DAY",
			m_days = "GLOBAL_DATETIME_LASTONLINE_DAYS",
			m_weeks = "GLOBAL_DATETIME_LASTONLINE_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_LASTONLINE_MONTH"
		};
		return global::TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x000376F8 File Offset: 0x000358F8
	public static string GetAwayTimeString(long epochMicrosec)
	{
		global::TimeUtils.ElapsedStringSet stringSet = new global::TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_AFK_SECONDS",
			m_minutes = "GLOBAL_DATETIME_AFK_MINUTES",
			m_hours = "GLOBAL_DATETIME_AFK_HOURS",
			m_yesterday = "GLOBAL_DATETIME_AFK_DAY",
			m_days = "GLOBAL_DATETIME_AFK_DAYS",
			m_weeks = "GLOBAL_DATETIME_AFK_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_AFK_MONTH"
		};
		return global::TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00037760 File Offset: 0x00035960
	public static int FriendSortCompare(BnetPlayer friend1, BnetPlayer friend2)
	{
		int result = 0;
		if (friend1 == null || friend2 == null)
		{
			if (friend1 == friend2)
			{
				return 0;
			}
			if (friend1 != null)
			{
				return -1;
			}
			return 1;
		}
		else
		{
			if (!friend1.IsOnline() && !friend2.IsOnline())
			{
				return FriendUtils.FriendNameSortCompare(friend1, friend2);
			}
			if (friend1.IsOnline() && !friend2.IsOnline())
			{
				return -1;
			}
			if (!friend1.IsOnline() && friend2.IsOnline())
			{
				return 1;
			}
			BnetProgramId bestProgramId = friend1.GetBestProgramId();
			BnetProgramId bestProgramId2 = friend2.GetBestProgramId();
			if (FriendUtils.FriendSortFlagCompare(friend1, friend2, bestProgramId == BnetProgramId.HEARTHSTONE, bestProgramId2 == BnetProgramId.HEARTHSTONE, out result))
			{
				return result;
			}
			bool lhsflag = !(bestProgramId == null) && bestProgramId.IsGame();
			bool rhsflag = !(bestProgramId2 == null) && bestProgramId2.IsGame();
			if (FriendUtils.FriendSortFlagCompare(friend1, friend2, lhsflag, rhsflag, out result))
			{
				return result;
			}
			bool lhsflag2 = !(bestProgramId == null) && bestProgramId.IsPhoenix();
			bool rhsflag2 = !(bestProgramId2 == null) && bestProgramId2.IsPhoenix();
			if (FriendUtils.FriendSortFlagCompare(friend1, friend2, lhsflag2, rhsflag2, out result))
			{
				return result;
			}
			return FriendUtils.FriendNameSortCompare(friend1, friend2);
		}
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00037866 File Offset: 0x00035A66
	public static int FriendNameSort(BnetPlayer friend1, BnetPlayer friend2)
	{
		return FriendUtils.FriendNameSortCompare(friend1, friend2);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003786F File Offset: 0x00035A6F
	public static bool FriendFlagSort(BnetPlayer lhs, BnetPlayer rhs, bool lhsflag, bool rhsflag, out int result)
	{
		return FriendUtils.FriendSortFlagCompare(lhs, rhs, lhsflag, rhsflag, out result);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0003787C File Offset: 0x00035A7C
	public static bool IsValidEmail(string emailString)
	{
		if (emailString == null)
		{
			return false;
		}
		int num = emailString.IndexOf('@');
		if (num >= 1 && num < emailString.Length - 1)
		{
			int num2 = emailString.LastIndexOf('.');
			if (num2 > num + 1 && num2 < emailString.Length - 1)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x000378C4 File Offset: 0x00035AC4
	private static bool GetUniqueName(BnetPlayer friend, out BnetBattleTag battleTag, out string name)
	{
		if (friend != null)
		{
			battleTag = friend.GetBattleTag();
			name = friend.GetBestName();
		}
		else
		{
			battleTag = null;
			name = string.Empty;
		}
		if (battleTag == null)
		{
			return false;
		}
		if (BnetNearbyPlayerMgr.Get().IsNearbyStranger(friend))
		{
			return true;
		}
		foreach (BnetPlayer bnetPlayer in BnetFriendMgr.Get().GetFriends())
		{
			if (bnetPlayer != friend)
			{
				string bestName = bnetPlayer.GetBestName();
				if (string.Compare(name, bestName, true) == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0003796C File Offset: 0x00035B6C
	private static bool FriendSortFlagCompare(BnetPlayer lhs, BnetPlayer rhs, bool lhsflag, bool rhsflag, out int result)
	{
		if (lhsflag && !rhsflag)
		{
			result = -1;
			return true;
		}
		if (!lhsflag && rhsflag)
		{
			result = 1;
			return true;
		}
		result = 0;
		return false;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00037990 File Offset: 0x00035B90
	private static int FriendNameSortCompare(BnetPlayer friend1, BnetPlayer friend2)
	{
		int num = string.Compare(FriendUtils.GetFriendListName(friend1, false), FriendUtils.GetFriendListName(friend2, false), true);
		if (num != 0)
		{
			return num;
		}
		long lo = (long)friend1.GetAccountId().GetLo();
		long lo2 = (long)friend2.GetAccountId().GetLo();
		return (int)(lo - lo2);
	}
}
