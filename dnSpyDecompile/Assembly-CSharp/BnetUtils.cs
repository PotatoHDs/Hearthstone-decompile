using System;
using bgs;
using PegasusShared;

// Token: 0x02000773 RID: 1907
public static class BnetUtils
{
	// Token: 0x06006BCF RID: 27599 RVA: 0x0022F330 File Offset: 0x0022D530
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

	// Token: 0x06006BD0 RID: 27600 RVA: 0x0022F364 File Offset: 0x0022D564
	public static BnetPlayer GetPlayer(BnetId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return null;
		}
		return BnetUtils.GetPlayer(BnetGameAccountId.CreateFromNet(gameAccountId));
	}

	// Token: 0x06006BD1 RID: 27601 RVA: 0x0022F378 File Offset: 0x0022D578
	public static string GetPlayerBestName(BnetGameAccountId id)
	{
		BnetPlayer player = BnetUtils.GetPlayer(id);
		string text = (player == null) ? null : player.GetBestName();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
		}
		return text;
	}

	// Token: 0x06006BD2 RID: 27602 RVA: 0x0022F3B0 File Offset: 0x0022D5B0
	public static string GetPlayerBestName(BnetAccountId id, bool requestIfNotFound = false)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
		string text = (player == null) ? null : player.GetBestName();
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

	// Token: 0x06006BD3 RID: 27603 RVA: 0x0022F3F4 File Offset: 0x0022D5F4
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

	// Token: 0x06006BD4 RID: 27604 RVA: 0x0022F448 File Offset: 0x0022D648
	public static bool HasPlayerBestNamePresence(BnetGameAccountId id)
	{
		BnetPlayer player = BnetUtils.GetPlayer(id);
		return !string.IsNullOrEmpty((player == null) ? null : player.GetBestName());
	}

	// Token: 0x06006BD5 RID: 27605 RVA: 0x0022F470 File Offset: 0x0022D670
	public static string GetInviterBestName(PartyInvite invite)
	{
		if (invite != null && !string.IsNullOrEmpty(invite.InviterName))
		{
			return invite.InviterName;
		}
		BnetPlayer bnetPlayer = (invite == null) ? null : BnetUtils.GetPlayer(invite.InviterId);
		string text = (bnetPlayer == null) ? null : bnetPlayer.GetBestName();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLOBAL_PLAYER_PLAYER");
		}
		return text;
	}

	// Token: 0x06006BD6 RID: 27606 RVA: 0x0022F4C7 File Offset: 0x0022D6C7
	public static bool CanReceiveWhisperFrom(BnetGameAccountId id)
	{
		return !BnetPresenceMgr.Get().GetMyPlayer().IsBusy() && BnetFriendMgr.Get().IsFriend(id);
	}

	// Token: 0x06006BD7 RID: 27607 RVA: 0x0022F4EC File Offset: 0x0022D6EC
	public static PartyId CreatePartyId(BnetId protoEntityId)
	{
		return new PartyId(protoEntityId.Hi, protoEntityId.Lo);
	}

	// Token: 0x06006BD8 RID: 27608 RVA: 0x0022F4FF File Offset: 0x0022D6FF
	public static BnetId CreatePegasusBnetId(PartyId partyId)
	{
		return new BnetId
		{
			Hi = partyId.Hi,
			Lo = partyId.Lo
		};
	}

	// Token: 0x06006BD9 RID: 27609 RVA: 0x0022F51E File Offset: 0x0022D71E
	public static BnetId CreatePegasusBnetId(BnetEntityId src)
	{
		return new BnetId
		{
			Hi = src.GetHi(),
			Lo = src.GetLo()
		};
	}

	// Token: 0x06006BDA RID: 27610 RVA: 0x0022F540 File Offset: 0x0022D740
	public static string GetNameForProgramId(BnetProgramId programId)
	{
		string nameTag = BnetProgramId.GetNameTag(programId);
		if (nameTag != null)
		{
			return GameStrings.Get(nameTag);
		}
		return null;
	}

	// Token: 0x06006BDB RID: 27611 RVA: 0x0022F560 File Offset: 0x0022D760
	public static ulong? TryGetGameAccountId()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return new ulong?(BattleNet.GetMyGameAccountId().lo);
	}

	// Token: 0x06006BDC RID: 27612 RVA: 0x0022F590 File Offset: 0x0022D790
	public static ulong? TryGetBnetAccountId()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return new ulong?(BattleNet.GetMyAccoundId().lo);
	}

	// Token: 0x06006BDD RID: 27613 RVA: 0x0022F5C0 File Offset: 0x0022D7C0
	public static constants.BnetRegion? TryGetBnetRegion()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return new constants.BnetRegion?(BattleNet.GetAccountRegion());
	}

	// Token: 0x06006BDE RID: 27614 RVA: 0x0022F5E8 File Offset: 0x0022D7E8
	public static constants.BnetRegion? TryGetGameRegion()
	{
		if (!BattleNet.IsInitialized())
		{
			return null;
		}
		return new constants.BnetRegion?(BattleNet.GetCurrentRegion());
	}
}
