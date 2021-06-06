using System;
using bgs.types;

namespace bgs
{
	// Token: 0x0200024D RID: 589
	public class BnetPresenceField
	{
		// Token: 0x06002486 RID: 9350 RVA: 0x0008106D File Offset: 0x0007F26D
		public static string GetGroupName(uint groupId)
		{
			if (groupId == 1U)
			{
				return "Account";
			}
			if (groupId != 2U)
			{
				return groupId.ToString();
			}
			return "GameAccount";
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0008108C File Offset: 0x0007F28C
		public static string GetFieldName(uint groupId, uint fieldId)
		{
			if (groupId != 1U)
			{
				if (groupId == 2U)
				{
					switch (fieldId)
					{
					case 1U:
						return "Online";
					case 2U:
						return "Busy";
					case 3U:
						return "Program";
					case 4U:
						return "LastOnline";
					case 5U:
						return "BattleTag";
					case 6U:
						return "Name";
					case 7U:
						return "AccountId";
					case 8U:
						return "RichPresence";
					case 9U:
						return "RichPresenceTime";
					case 10U:
						return "AFK";
					case 11U:
						return "AFKTime";
					default:
						if (fieldId == 1000U)
						{
							return "ClientRichPresence";
						}
						break;
					}
				}
			}
			else
			{
				switch (fieldId)
				{
				case 1U:
					return "FullName";
				case 2U:
					return "CustomMsg";
				case 3U:
					return "GameAccounts";
				case 4U:
					return "BattleTag";
				case 5U:
					return "CustomMsgTime";
				case 6U:
					return "LastOnline";
				case 7U:
					return "Away";
				case 8U:
					return "AwayTime";
				case 9U:
					return "Invisible";
				case 10U:
					return "InvisibleTime";
				case 11U:
					return "DND";
				}
			}
			return fieldId.ToString();
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000811B4 File Offset: 0x0007F3B4
		public static string GetFieldValue(PresenceUpdate update)
		{
			if (update.valCleared)
			{
				return "null";
			}
			if (update.programId == BnetProgramId.BNET)
			{
				uint num = update.groupId;
				if (num != 1U)
				{
					if (num == 2U)
					{
						num = update.fieldId;
						switch (num)
						{
						case 1U:
						case 10U:
							return update.boolVal.ToString();
						case 2U:
							return update.intVal.ToString();
						case 3U:
							return update.stringVal ?? "null";
						case 4U:
						case 9U:
						case 11U:
						{
							DateTime d = TimeUtils.ConvertEpochMicrosecToDateTime(update.intVal);
							TimeSpan timeSpan = DateTime.UtcNow - d;
							string arg = d.ToString("R");
							return string.Format("{0} ({1} ago)", arg, timeSpan.ToString());
						}
						case 5U:
						case 6U:
							break;
						case 7U:
						case 8U:
							goto IL_197;
						default:
							if (num != 1000U)
							{
								goto IL_197;
							}
							break;
						}
						return update.stringVal ?? "null";
					}
				}
				else
				{
					switch (update.fieldId)
					{
					case 1U:
					case 2U:
					case 4U:
						return update.stringVal ?? "null";
					case 5U:
					case 6U:
					case 8U:
					case 10U:
					{
						DateTime d2 = TimeUtils.ConvertEpochMicrosecToDateTime(update.intVal);
						TimeSpan timeSpan2 = DateTime.UtcNow - d2;
						string arg2 = d2.ToString("R");
						return string.Format("{0} ({1} ago)", arg2, timeSpan2.ToString());
					}
					case 7U:
					case 9U:
					case 11U:
						return update.boolVal.ToString();
					}
				}
			}
			IL_197:
			return BnetPresenceField.GetUnnamedFieldValue(update);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00081360 File Offset: 0x0007F560
		public static string GetUnnamedFieldValue(PresenceUpdate update)
		{
			if (!string.IsNullOrEmpty(update.stringVal))
			{
				return update.stringVal;
			}
			if (update.entityIdVal.hi != 0UL && update.entityIdVal.lo != 0UL)
			{
				return string.Format("{{hi:{0} lo:{1}}}", update.entityIdVal.hi, update.entityIdVal.lo);
			}
			if (update.blobVal != null && update.blobVal.Length != 0)
			{
				return string.Format("blob[{0}]", update.blobVal.Length);
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

		// Token: 0x04000F14 RID: 3860
		public const uint BNET_ACCOUNT = 1U;

		// Token: 0x04000F15 RID: 3861
		public const uint BNET_ACCOUNT_FULL_NAME = 1U;

		// Token: 0x04000F16 RID: 3862
		public const uint BNET_ACCOUNT_CUSTOM_MESSAGE = 2U;

		// Token: 0x04000F17 RID: 3863
		public const uint BNET_ACCOUNT_GAME_ACCOUNTS = 3U;

		// Token: 0x04000F18 RID: 3864
		public const uint BNET_ACCOUNT_BATTLE_TAG = 4U;

		// Token: 0x04000F19 RID: 3865
		public const uint BNET_ACCOUNT_CUSTOM_MESSAGE_TIME = 5U;

		// Token: 0x04000F1A RID: 3866
		public const uint BNET_ACCOUNT_LAST_ONLINE = 6U;

		// Token: 0x04000F1B RID: 3867
		public const uint BNET_ACCOUNT_AWAY = 7U;

		// Token: 0x04000F1C RID: 3868
		public const uint BNET_ACCOUNT_AWAY_TIME = 8U;

		// Token: 0x04000F1D RID: 3869
		public const uint BNET_ACCOUNT_INVISIBLE = 9U;

		// Token: 0x04000F1E RID: 3870
		public const uint BNET_ACCOUNT_INVISIBLE_TIME = 10U;

		// Token: 0x04000F1F RID: 3871
		public const uint BNET_ACCOUNT_DND = 11U;

		// Token: 0x04000F20 RID: 3872
		public const uint BNET_ACCOUNT_APPEAR_OFFLINE = 12U;

		// Token: 0x04000F21 RID: 3873
		public const uint BNET_GAME_ACCOUNT = 2U;

		// Token: 0x04000F22 RID: 3874
		public const uint BNET_GAME_ACCOUNT_ONLINE = 1U;

		// Token: 0x04000F23 RID: 3875
		public const uint BNET_GAME_ACCOUNT_BUSY = 2U;

		// Token: 0x04000F24 RID: 3876
		public const uint BNET_GAME_ACCOUNT_PROGRAM = 3U;

		// Token: 0x04000F25 RID: 3877
		public const uint BNET_GAME_ACCOUNT_LAST_ONLINE = 4U;

		// Token: 0x04000F26 RID: 3878
		public const uint BNET_GAME_ACCOUNT_BATTLE_TAG = 5U;

		// Token: 0x04000F27 RID: 3879
		public const uint BNET_GAME_ACCOUNT_NAME = 6U;

		// Token: 0x04000F28 RID: 3880
		public const uint BNET_GAME_ACCOUNT_ID = 7U;

		// Token: 0x04000F29 RID: 3881
		public const uint BNET_GAME_ACCOUNT_RICH_PRESENCE = 8U;

		// Token: 0x04000F2A RID: 3882
		public const uint BNET_GAME_ACCOUNT_RICH_PRESENCE_TIME = 9U;

		// Token: 0x04000F2B RID: 3883
		public const uint BNET_GAME_ACCOUNT_AFK = 10U;

		// Token: 0x04000F2C RID: 3884
		public const uint BNET_GAME_ACCOUNT_AFK_TIME = 11U;

		// Token: 0x04000F2D RID: 3885
		public const uint BNET_GAME_ACCOUNT_CLIENT_RICH_PRESENCE = 1000U;
	}
}
