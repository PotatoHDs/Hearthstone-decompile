using bgs;
using bgs.types;
using PegasusClient;
using PegasusFSG;
using SpectatorProto;

public class GamePresenceField
{
	public const uint GAME_ACCOUNT = 2u;

	public const uint CAN_BE_INVITED_TO_GAME = 1u;

	public const uint DEBUG_STRING = 2u;

	public const uint DEPRECATED_ARENA_RECORD = 3u;

	public const uint CARDS_OPENED = 4u;

	public const uint DRUID_LEVEL = 5u;

	public const uint HUNTER_LEVEL = 6u;

	public const uint MAGE_LEVEL = 7u;

	public const uint PALADIN_LEVEL = 8u;

	public const uint PRIEST_LEVEL = 9u;

	public const uint ROGUE_LEVEL = 10u;

	public const uint SHAMAN_LEVEL = 11u;

	public const uint WARLOCK_LEVEL = 12u;

	public const uint WARRIOR_LEVEL = 13u;

	public const uint GAIN_MEDAL = 14u;

	public const uint TUTORIAL_BEATEN = 15u;

	public const uint COLLECTION_EVENT = 16u;

	public const uint STATUS = 17u;

	public const uint RANK = 18u;

	public const uint CLIENT_VERSION = 19u;

	public const uint CLIENT_ENV = 20u;

	public const uint SPECTATOR_INFO = 21u;

	public const uint SESSION_RECORD = 22u;

	public const uint SECRET_SPECTATOR_INFO = 23u;

	public const uint DECK_VALIDITY = 24u;

	public const uint FIRESIDE_GATHERING_INFO = 25u;

	public const uint PARTY_ID = 26u;

	public static uint[] TransientStatusFields => new uint[8] { 17u, 19u, 20u, 21u, 23u, 24u, 25u, 26u };

	public static string GetFieldName(uint fieldId)
	{
		return fieldId switch
		{
			1u => "CanBeInvitedToGame", 
			2u => "DebugString", 
			3u => "ArenaRecord", 
			4u => "CardsOpened", 
			5u => "DruidLevel", 
			6u => "HunterLevel", 
			7u => "MageLevel", 
			8u => "PaladinLevel", 
			9u => "PriestLevel", 
			10u => "RogueLevel", 
			11u => "ShamanLevel", 
			12u => "WarlockLevel", 
			13u => "WarriorLevel", 
			14u => "GainMedal", 
			15u => "TutorialBeaten", 
			16u => "CollectionEvent", 
			17u => "Status", 
			18u => "Rank", 
			19u => "ClientVersion", 
			20u => "ClientEnv", 
			21u => "SpectatorInfo", 
			22u => "SessionRecord", 
			23u => "SecretJoinInfo", 
			24u => "DeckValidity", 
			25u => "FSGInfo", 
			26u => "PartyId", 
			_ => fieldId.ToString(), 
		};
	}

	public static string GetFieldValue(PresenceUpdate update)
	{
		if (update.valCleared)
		{
			return "null";
		}
		if (update.programId == BnetProgramId.HEARTHSTONE)
		{
			uint groupId = update.groupId;
			if (groupId == 2)
			{
				switch (update.fieldId)
				{
				case 2u:
				case 3u:
				case 4u:
				case 19u:
				case 20u:
					return update.stringVal ?? "null";
				case 1u:
					return update.boolVal.ToString();
				case 5u:
				case 6u:
				case 7u:
				case 8u:
				case 9u:
				case 10u:
				case 11u:
				case 12u:
				case 13u:
				case 14u:
				case 15u:
				case 16u:
					return update.intVal.ToString();
				case 17u:
					if (update.blobVal != null)
					{
						return PresenceMgr.Get().GetStatusText(update.blobVal);
					}
					return "null";
				case 18u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<GamePresenceRank>(update.blobVal).ToHumanReadableString();
				case 21u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<JoinInfo>(update.blobVal).ToHumanReadableString();
				case 22u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<SessionRecord>(update.blobVal).ToHumanReadableString();
				case 23u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<SecretJoinInfo>(update.blobVal).ToHumanReadableString();
				case 24u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<DeckValidity>(update.blobVal).ToHumanReadableString();
				case 25u:
					if (update.blobVal == null)
					{
						return "null";
					}
					return ProtobufUtil.ParseFrom<FiresideGatheringInfo>(update.blobVal).ToHumanReadableString();
				case 26u:
					return update.entityIdVal.ToString();
				}
			}
		}
		return BnetPresenceField.GetUnnamedFieldValue(update);
	}
}
