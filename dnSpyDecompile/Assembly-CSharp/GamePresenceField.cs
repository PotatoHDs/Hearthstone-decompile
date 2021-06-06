using System;
using bgs;
using bgs.types;
using PegasusClient;
using PegasusFSG;
using SpectatorProto;

// Token: 0x02000777 RID: 1911
public class GamePresenceField
{
	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x06006C0E RID: 27662 RVA: 0x0022FD24 File Offset: 0x0022DF24
	public static uint[] TransientStatusFields
	{
		get
		{
			return new uint[]
			{
				17U,
				19U,
				20U,
				21U,
				23U,
				24U,
				25U,
				26U
			};
		}
	}

	// Token: 0x06006C0F RID: 27663 RVA: 0x0022FD38 File Offset: 0x0022DF38
	public static string GetFieldName(uint fieldId)
	{
		switch (fieldId)
		{
		case 1U:
			return "CanBeInvitedToGame";
		case 2U:
			return "DebugString";
		case 3U:
			return "ArenaRecord";
		case 4U:
			return "CardsOpened";
		case 5U:
			return "DruidLevel";
		case 6U:
			return "HunterLevel";
		case 7U:
			return "MageLevel";
		case 8U:
			return "PaladinLevel";
		case 9U:
			return "PriestLevel";
		case 10U:
			return "RogueLevel";
		case 11U:
			return "ShamanLevel";
		case 12U:
			return "WarlockLevel";
		case 13U:
			return "WarriorLevel";
		case 14U:
			return "GainMedal";
		case 15U:
			return "TutorialBeaten";
		case 16U:
			return "CollectionEvent";
		case 17U:
			return "Status";
		case 18U:
			return "Rank";
		case 19U:
			return "ClientVersion";
		case 20U:
			return "ClientEnv";
		case 21U:
			return "SpectatorInfo";
		case 22U:
			return "SessionRecord";
		case 23U:
			return "SecretJoinInfo";
		case 24U:
			return "DeckValidity";
		case 25U:
			return "FSGInfo";
		case 26U:
			return "PartyId";
		default:
			return fieldId.ToString();
		}
	}

	// Token: 0x06006C10 RID: 27664 RVA: 0x0022FE60 File Offset: 0x0022E060
	public static string GetFieldValue(PresenceUpdate update)
	{
		if (update.valCleared)
		{
			return "null";
		}
		if (update.programId == BnetProgramId.HEARTHSTONE)
		{
			uint groupId = update.groupId;
			if (groupId == 2U)
			{
				switch (update.fieldId)
				{
				case 1U:
					return update.boolVal.ToString();
				case 2U:
				case 3U:
				case 4U:
				case 19U:
				case 20U:
					return update.stringVal ?? "null";
				case 5U:
				case 6U:
				case 7U:
				case 8U:
				case 9U:
				case 10U:
				case 11U:
				case 12U:
				case 13U:
				case 14U:
				case 15U:
				case 16U:
					return update.intVal.ToString();
				case 17U:
					if (update.blobVal != null)
					{
						return PresenceMgr.Get().GetStatusText(update.blobVal);
					}
					return "null";
				case 18U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<GamePresenceRank>(update.blobVal, 0, -1).ToHumanReadableString();
				case 21U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<JoinInfo>(update.blobVal, 0, -1).ToHumanReadableString();
				case 22U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<SessionRecord>(update.blobVal, 0, -1).ToHumanReadableString();
				case 23U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<SecretJoinInfo>(update.blobVal, 0, -1).ToHumanReadableString();
				case 24U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<DeckValidity>(update.blobVal, 0, -1).ToHumanReadableString();
				case 25U:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<FiresideGatheringInfo>(update.blobVal, 0, -1).ToHumanReadableString();
				case 26U:
					return update.entityIdVal.ToString();
				}
			}
		}
		return BnetPresenceField.GetUnnamedFieldValue(update);
	}

	// Token: 0x04005742 RID: 22338
	public const uint GAME_ACCOUNT = 2U;

	// Token: 0x04005743 RID: 22339
	public const uint CAN_BE_INVITED_TO_GAME = 1U;

	// Token: 0x04005744 RID: 22340
	public const uint DEBUG_STRING = 2U;

	// Token: 0x04005745 RID: 22341
	public const uint DEPRECATED_ARENA_RECORD = 3U;

	// Token: 0x04005746 RID: 22342
	public const uint CARDS_OPENED = 4U;

	// Token: 0x04005747 RID: 22343
	public const uint DRUID_LEVEL = 5U;

	// Token: 0x04005748 RID: 22344
	public const uint HUNTER_LEVEL = 6U;

	// Token: 0x04005749 RID: 22345
	public const uint MAGE_LEVEL = 7U;

	// Token: 0x0400574A RID: 22346
	public const uint PALADIN_LEVEL = 8U;

	// Token: 0x0400574B RID: 22347
	public const uint PRIEST_LEVEL = 9U;

	// Token: 0x0400574C RID: 22348
	public const uint ROGUE_LEVEL = 10U;

	// Token: 0x0400574D RID: 22349
	public const uint SHAMAN_LEVEL = 11U;

	// Token: 0x0400574E RID: 22350
	public const uint WARLOCK_LEVEL = 12U;

	// Token: 0x0400574F RID: 22351
	public const uint WARRIOR_LEVEL = 13U;

	// Token: 0x04005750 RID: 22352
	public const uint GAIN_MEDAL = 14U;

	// Token: 0x04005751 RID: 22353
	public const uint TUTORIAL_BEATEN = 15U;

	// Token: 0x04005752 RID: 22354
	public const uint COLLECTION_EVENT = 16U;

	// Token: 0x04005753 RID: 22355
	public const uint STATUS = 17U;

	// Token: 0x04005754 RID: 22356
	public const uint RANK = 18U;

	// Token: 0x04005755 RID: 22357
	public const uint CLIENT_VERSION = 19U;

	// Token: 0x04005756 RID: 22358
	public const uint CLIENT_ENV = 20U;

	// Token: 0x04005757 RID: 22359
	public const uint SPECTATOR_INFO = 21U;

	// Token: 0x04005758 RID: 22360
	public const uint SESSION_RECORD = 22U;

	// Token: 0x04005759 RID: 22361
	public const uint SECRET_SPECTATOR_INFO = 23U;

	// Token: 0x0400575A RID: 22362
	public const uint DECK_VALIDITY = 24U;

	// Token: 0x0400575B RID: 22363
	public const uint FIRESIDE_GATHERING_INFO = 25U;

	// Token: 0x0400575C RID: 22364
	public const uint PARTY_ID = 26U;
}
