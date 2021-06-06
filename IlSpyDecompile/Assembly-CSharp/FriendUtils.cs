using System.Collections.Generic;
using bgs;

public class FriendUtils
{
	public static string GetUniqueName(BnetPlayer friend)
	{
		if (GetUniqueName(friend, out var battleTag, out var name))
		{
			return battleTag.ToString();
		}
		return name;
	}

	public static string GetUniqueNameWithColor(BnetPlayer friend)
	{
		string text = ((friend != null && friend.IsOnline()) ? "5ecaf0ff" : "999999ff");
		if (GetUniqueName(friend, out var battleTag, out var name))
		{
			return GetBattleTagWithColor(battleTag, text);
		}
		return $"<color=#{text}>{name}</color>";
	}

	public static string GetBattleTagWithColor(BnetBattleTag battleTag, string nameColorStr)
	{
		return string.Format("<color=#{0}>{1}</color><color=#{2}>#{3}</color>", nameColorStr, battleTag.GetName(), "a1a1a1ff", battleTag.GetNumber());
	}

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
			foreach (KeyValuePair<BnetGameAccountId, BnetGameAccount> gameAccount in friend.GetGameAccounts())
			{
				if (gameAccount.Value.GetBattleTag() != null)
				{
					text = gameAccount.Value.GetBattleTag().ToString();
					break;
				}
			}
		}
		if (addColorTags)
		{
			string arg = (friend.IsOnline() ? "5ecaf0ff" : "999999ff");
			return $"<color=#{arg}>{text}</color>";
		}
		return text;
	}

	public static string GetRequestElapsedTimeString(long epochMicrosec)
	{
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_FRIENDREQUEST_SECONDS",
			m_minutes = "GLOBAL_DATETIME_FRIENDREQUEST_MINUTES",
			m_hours = "GLOBAL_DATETIME_FRIENDREQUEST_HOURS",
			m_yesterday = "GLOBAL_DATETIME_FRIENDREQUEST_DAY",
			m_days = "GLOBAL_DATETIME_FRIENDREQUEST_DAYS",
			m_weeks = "GLOBAL_DATETIME_FRIENDREQUEST_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_FRIENDREQUEST_MONTH"
		};
		return TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	public static string GetLastOnlineElapsedTimeString(long epochMicrosec)
	{
		if (epochMicrosec == 0L)
		{
			return GameStrings.Get("GLOBAL_OFFLINE");
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_LASTONLINE_SECONDS",
			m_minutes = "GLOBAL_DATETIME_LASTONLINE_MINUTES",
			m_hours = "GLOBAL_DATETIME_LASTONLINE_HOURS",
			m_yesterday = "GLOBAL_DATETIME_LASTONLINE_DAY",
			m_days = "GLOBAL_DATETIME_LASTONLINE_DAYS",
			m_weeks = "GLOBAL_DATETIME_LASTONLINE_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_LASTONLINE_MONTH"
		};
		return TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	public static string GetAwayTimeString(long epochMicrosec)
	{
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_AFK_SECONDS",
			m_minutes = "GLOBAL_DATETIME_AFK_MINUTES",
			m_hours = "GLOBAL_DATETIME_AFK_HOURS",
			m_yesterday = "GLOBAL_DATETIME_AFK_DAY",
			m_days = "GLOBAL_DATETIME_AFK_DAYS",
			m_weeks = "GLOBAL_DATETIME_AFK_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_AFK_MONTH"
		};
		return TimeUtils.GetElapsedTimeStringFromEpochMicrosec(epochMicrosec, stringSet);
	}

	public static int FriendSortCompare(BnetPlayer friend1, BnetPlayer friend2)
	{
		int result = 0;
		if (friend1 == null || friend2 == null)
		{
			if (friend1 != friend2)
			{
				if (friend1 != null)
				{
					return -1;
				}
				return 1;
			}
			return 0;
		}
		if (!friend1.IsOnline() && !friend2.IsOnline())
		{
			return FriendNameSortCompare(friend1, friend2);
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
		if (FriendSortFlagCompare(friend1, friend2, bestProgramId == BnetProgramId.HEARTHSTONE, bestProgramId2 == BnetProgramId.HEARTHSTONE, out result))
		{
			return result;
		}
		bool lhsflag = !(bestProgramId == null) && bestProgramId.IsGame();
		bool rhsflag = !(bestProgramId2 == null) && bestProgramId2.IsGame();
		if (FriendSortFlagCompare(friend1, friend2, lhsflag, rhsflag, out result))
		{
			return result;
		}
		bool lhsflag2 = !(bestProgramId == null) && bestProgramId.IsPhoenix();
		bool rhsflag2 = !(bestProgramId2 == null) && bestProgramId2.IsPhoenix();
		if (FriendSortFlagCompare(friend1, friend2, lhsflag2, rhsflag2, out result))
		{
			return result;
		}
		return FriendNameSortCompare(friend1, friend2);
	}

	public static int FriendNameSort(BnetPlayer friend1, BnetPlayer friend2)
	{
		return FriendNameSortCompare(friend1, friend2);
	}

	public static bool FriendFlagSort(BnetPlayer lhs, BnetPlayer rhs, bool lhsflag, bool rhsflag, out int result)
	{
		return FriendSortFlagCompare(lhs, rhs, lhsflag, rhsflag, out result);
	}

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
		foreach (BnetPlayer friend2 in BnetFriendMgr.Get().GetFriends())
		{
			if (friend2 != friend)
			{
				string bestName = friend2.GetBestName();
				if (string.Compare(name, bestName, ignoreCase: true) == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

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

	private static int FriendNameSortCompare(BnetPlayer friend1, BnetPlayer friend2)
	{
		int num = string.Compare(GetFriendListName(friend1, addColorTags: false), GetFriendListName(friend2, addColorTags: false), ignoreCase: true);
		if (num != 0)
		{
			return num;
		}
		ulong lo = friend1.GetAccountId().GetLo();
		long lo2 = (long)friend2.GetAccountId().GetLo();
		return (int)((long)lo - lo2);
	}
}
