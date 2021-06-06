using System;
using bgs.types;

namespace bgs
{
	public class BnetPresenceField
	{
		public const uint BNET_ACCOUNT = 1u;

		public const uint BNET_ACCOUNT_FULL_NAME = 1u;

		public const uint BNET_ACCOUNT_CUSTOM_MESSAGE = 2u;

		public const uint BNET_ACCOUNT_GAME_ACCOUNTS = 3u;

		public const uint BNET_ACCOUNT_BATTLE_TAG = 4u;

		public const uint BNET_ACCOUNT_CUSTOM_MESSAGE_TIME = 5u;

		public const uint BNET_ACCOUNT_LAST_ONLINE = 6u;

		public const uint BNET_ACCOUNT_AWAY = 7u;

		public const uint BNET_ACCOUNT_AWAY_TIME = 8u;

		public const uint BNET_ACCOUNT_INVISIBLE = 9u;

		public const uint BNET_ACCOUNT_INVISIBLE_TIME = 10u;

		public const uint BNET_ACCOUNT_DND = 11u;

		public const uint BNET_ACCOUNT_APPEAR_OFFLINE = 12u;

		public const uint BNET_GAME_ACCOUNT = 2u;

		public const uint BNET_GAME_ACCOUNT_ONLINE = 1u;

		public const uint BNET_GAME_ACCOUNT_BUSY = 2u;

		public const uint BNET_GAME_ACCOUNT_PROGRAM = 3u;

		public const uint BNET_GAME_ACCOUNT_LAST_ONLINE = 4u;

		public const uint BNET_GAME_ACCOUNT_BATTLE_TAG = 5u;

		public const uint BNET_GAME_ACCOUNT_NAME = 6u;

		public const uint BNET_GAME_ACCOUNT_ID = 7u;

		public const uint BNET_GAME_ACCOUNT_RICH_PRESENCE = 8u;

		public const uint BNET_GAME_ACCOUNT_RICH_PRESENCE_TIME = 9u;

		public const uint BNET_GAME_ACCOUNT_AFK = 10u;

		public const uint BNET_GAME_ACCOUNT_AFK_TIME = 11u;

		public const uint BNET_GAME_ACCOUNT_CLIENT_RICH_PRESENCE = 1000u;

		public static string GetGroupName(uint groupId)
		{
			return groupId switch
			{
				1u => "Account", 
				2u => "GameAccount", 
				_ => groupId.ToString(), 
			};
		}

		public static string GetFieldName(uint groupId, uint fieldId)
		{
			switch (groupId)
			{
			case 1u:
				switch (fieldId)
				{
				case 1u:
					return "FullName";
				case 2u:
					return "CustomMsg";
				case 3u:
					return "GameAccounts";
				case 4u:
					return "BattleTag";
				case 5u:
					return "CustomMsgTime";
				case 6u:
					return "LastOnline";
				case 7u:
					return "Away";
				case 8u:
					return "AwayTime";
				case 9u:
					return "Invisible";
				case 10u:
					return "InvisibleTime";
				case 11u:
					return "DND";
				}
				break;
			case 2u:
				switch (fieldId)
				{
				case 1u:
					return "Online";
				case 2u:
					return "Busy";
				case 3u:
					return "Program";
				case 4u:
					return "LastOnline";
				case 5u:
					return "BattleTag";
				case 6u:
					return "Name";
				case 7u:
					return "AccountId";
				case 8u:
					return "RichPresence";
				case 9u:
					return "RichPresenceTime";
				case 10u:
					return "AFK";
				case 11u:
					return "AFKTime";
				case 1000u:
					return "ClientRichPresence";
				}
				break;
			}
			return fieldId.ToString();
		}

		public static string GetFieldValue(PresenceUpdate update)
		{
			if (update.valCleared)
			{
				return "null";
			}
			if (update.programId == BnetProgramId.BNET)
			{
				switch (update.groupId)
				{
				case 1u:
					switch (update.fieldId)
					{
					case 1u:
					case 2u:
					case 4u:
						return update.stringVal ?? "null";
					case 7u:
					case 9u:
					case 11u:
						return update.boolVal.ToString();
					case 5u:
					case 6u:
					case 8u:
					case 10u:
					{
						DateTime dateTime2 = TimeUtils.ConvertEpochMicrosecToDateTime(update.intVal);
						TimeSpan timeSpan2 = DateTime.UtcNow - dateTime2;
						string arg2 = dateTime2.ToString("R");
						return $"{arg2} ({timeSpan2.ToString()} ago)";
					}
					}
					break;
				case 2u:
					switch (update.fieldId)
					{
					case 1u:
					case 10u:
						return update.boolVal.ToString();
					case 2u:
						return update.intVal.ToString();
					case 4u:
					case 9u:
					case 11u:
					{
						DateTime dateTime = TimeUtils.ConvertEpochMicrosecToDateTime(update.intVal);
						TimeSpan timeSpan = DateTime.UtcNow - dateTime;
						string arg = dateTime.ToString("R");
						return $"{arg} ({timeSpan.ToString()} ago)";
					}
					case 3u:
						return update.stringVal ?? "null";
					case 5u:
					case 6u:
					case 1000u:
						return update.stringVal ?? "null";
					}
					break;
				}
			}
			return GetUnnamedFieldValue(update);
		}

		public static string GetUnnamedFieldValue(PresenceUpdate update)
		{
			if (!string.IsNullOrEmpty(update.stringVal))
			{
				return update.stringVal;
			}
			if (update.entityIdVal.hi != 0L && update.entityIdVal.lo != 0L)
			{
				return $"{{hi:{update.entityIdVal.hi} lo:{update.entityIdVal.lo}}}";
			}
			if (update.blobVal != null && update.blobVal.Length != 0)
			{
				return $"blob[{update.blobVal.Length}]";
			}
			if (update.boolVal)
			{
				return update.boolVal.ToString();
			}
			if (update.intVal != 0L)
			{
				return update.intVal.ToString();
			}
			return "null_or_0_or_blank";
		}
	}
}
