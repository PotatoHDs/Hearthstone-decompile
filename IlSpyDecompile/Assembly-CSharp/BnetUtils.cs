using bgs;
using PegasusShared;

public static class BnetUtils
{
	public static BnetPlayer GetPlayer(BnetGameAccountId id)
	{
		if (id == null)
		{
			return null;
		}
		BnetPlayer bnetPlayer = BnetNearbyPlayerMgr.Get().FindNearbyStranger(id);
		if (bnetPlayer == null)
		{
			bnetPlayer = BnetPresenceMgr.Get().GetPlayer(id);
		}
		return bnetPlayer;
	}

	public static BnetPlayer GetPlayer(BnetId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return null;
		}
		return GetPlayer(BnetGameAccountId.CreateFromNet(gameAccountId));
	}

	public static string GetPlayerBestName(BnetGameAccountId id)
	{
		string text = GetPlayer(id)?.GetBestName();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
		}
		return text;
	}

	public static string GetPlayerBestName(BnetAccountId id, bool requestIfNotFound = false)
	{
		string text = BnetPresenceMgr.Get().GetPlayer(id)?.GetBestName();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
			if (requestIfNotFound)
			{
				BnetPresenceMgr.RequestPlayerBattleTag(id);
			}
		}
		return text;
	}

	public static string GetPlayerBattleTagName(BnetAccountId id, bool requestIfNotFound = false)
	{
		string text = null;
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		if (player != null)
		{
			BnetBattleTag battleTag = player.GetBattleTag();
			if (battleTag != null)
			{
				text = battleTag.GetName();
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
			if (requestIfNotFound)
			{
				BnetPresenceMgr.RequestPlayerBattleTag(id);
			}
		}
		return text;
	}

	public static bool HasPlayerBestNamePresence(BnetGameAccountId id)
	{
		return !string.IsNullOrEmpty(GetPlayer(id)?.GetBestName());
	}

	public static string GetInviterBestName(PartyInvite invite)
	{
		if (invite != null && !string.IsNullOrEmpty(invite.InviterName))
		{
			return invite.InviterName;
		}
		string text = ((invite == null) ? null : GetPlayer(invite.InviterId))?.GetBestName();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
		}
		return text;
	}

	public static bool CanReceiveWhisperFrom(BnetGameAccountId id)
	{
		if (BnetPresenceMgr.Get().GetMyPlayer().IsBusy())
		{
			return false;
		}
		if (BnetFriendMgr.Get().IsFriend(id))
		{
			return true;
		}
		return false;
	}

	public static PartyId CreatePartyId(BnetId protoEntityId)
	{
		return new PartyId(protoEntityId.Hi, protoEntityId.Lo);
	}

	public static BnetId CreatePegasusBnetId(PartyId partyId)
	{
		return new BnetId
		{
			Hi = partyId.Hi,
			Lo = partyId.Lo
		};
	}

	public static BnetId CreatePegasusBnetId(BnetEntityId src)
	{
		return new BnetId
		{
			Hi = src.GetHi(),
			Lo = src.GetLo()
		};
	}

	public static string GetNameForProgramId(BnetProgramId programId)
	{
		string nameTag = BnetProgramId.GetNameTag(programId);
		if (nameTag != null)
		{
			return GameStrings.Get(nameTag);
		}
		return null;
	}

	public static ulong? TryGetGameAccountId()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return BattleNet.GetMyGameAccountId().lo;
	}

	public static ulong? TryGetBnetAccountId()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return BattleNet.GetMyAccoundId().lo;
	}

	public static constants.BnetRegion? TryGetBnetRegion()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return BattleNet.GetAccountRegion();
	}

	public static constants.BnetRegion? TryGetGameRegion()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return BattleNet.GetCurrentRegion();
	}
}
