using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200015B RID: 347
	public class GameContentSeasonSpec : IProtoBuf
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00050569 File Offset: 0x0004E769
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x00050571 File Offset: 0x0004E771
		public ulong EndSecondsFromNow
		{
			get
			{
				return this._EndSecondsFromNow;
			}
			set
			{
				this._EndSecondsFromNow = value;
				this.HasEndSecondsFromNow = true;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00050581 File Offset: 0x0004E781
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00050589 File Offset: 0x0004E789
		public int SeasonId { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00050592 File Offset: 0x0004E792
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x0005059A File Offset: 0x0004E79A
		public int SeasonEndSecondSpreadCount
		{
			get
			{
				return this._SeasonEndSecondSpreadCount;
			}
			set
			{
				this._SeasonEndSecondSpreadCount = value;
				this.HasSeasonEndSecondSpreadCount = true;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x000505AA File Offset: 0x0004E7AA
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x000505B2 File Offset: 0x0004E7B2
		public List<GameContentScenario> Scenarios
		{
			get
			{
				return this._Scenarios;
			}
			set
			{
				this._Scenarios = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000505BB File Offset: 0x0004E7BB
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x000505C3 File Offset: 0x0004E7C3
		public int DeprecatedScenarioId
		{
			get
			{
				return this._DeprecatedScenarioId;
			}
			set
			{
				this._DeprecatedScenarioId = value;
				this.HasDeprecatedScenarioId = true;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x000505D3 File Offset: 0x0004E7D3
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x000505DB File Offset: 0x0004E7DB
		public uint DeprecatedScenarioRecordByteSize
		{
			get
			{
				return this._DeprecatedScenarioRecordByteSize;
			}
			set
			{
				this._DeprecatedScenarioRecordByteSize = value;
				this.HasDeprecatedScenarioRecordByteSize = true;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x000505EB File Offset: 0x0004E7EB
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x000505F3 File Offset: 0x0004E7F3
		public byte[] DeprecatedScenarioRecordHash
		{
			get
			{
				return this._DeprecatedScenarioRecordHash;
			}
			set
			{
				this._DeprecatedScenarioRecordHash = value;
				this.HasDeprecatedScenarioRecordHash = (value != null);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00050606 File Offset: 0x0004E806
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0005060E File Offset: 0x0004E80E
		public RewardType DeprecatedRewardType
		{
			get
			{
				return this._DeprecatedRewardType;
			}
			set
			{
				this._DeprecatedRewardType = value;
				this.HasDeprecatedRewardType = true;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005061E File Offset: 0x0004E81E
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x00050626 File Offset: 0x0004E826
		public long DeprecatedRewardData1
		{
			get
			{
				return this._DeprecatedRewardData1;
			}
			set
			{
				this._DeprecatedRewardData1 = value;
				this.HasDeprecatedRewardData1 = true;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00050636 File Offset: 0x0004E836
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x0005063E File Offset: 0x0004E83E
		public long DeprecatedRewardData2
		{
			get
			{
				return this._DeprecatedRewardData2;
			}
			set
			{
				this._DeprecatedRewardData2 = value;
				this.HasDeprecatedRewardData2 = true;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0005064E File Offset: 0x0004E84E
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x00050656 File Offset: 0x0004E856
		public RewardTrigger DeprecatedRewardTrigger
		{
			get
			{
				return this._DeprecatedRewardTrigger;
			}
			set
			{
				this._DeprecatedRewardTrigger = value;
				this.HasDeprecatedRewardTrigger = true;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x00050666 File Offset: 0x0004E866
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0005066E File Offset: 0x0004E86E
		public FormatType DeprecatedFormatType
		{
			get
			{
				return this._DeprecatedFormatType;
			}
			set
			{
				this._DeprecatedFormatType = value;
				this.HasDeprecatedFormatType = true;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0005067E File Offset: 0x0004E87E
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00050686 File Offset: 0x0004E886
		public int DeprecatedTicketType
		{
			get
			{
				return this._DeprecatedTicketType;
			}
			set
			{
				this._DeprecatedTicketType = value;
				this.HasDeprecatedTicketType = true;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00050696 File Offset: 0x0004E896
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0005069E File Offset: 0x0004E89E
		public int DeprecatedMaxWins
		{
			get
			{
				return this._DeprecatedMaxWins;
			}
			set
			{
				this._DeprecatedMaxWins = value;
				this.HasDeprecatedMaxWins = true;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x000506AE File Offset: 0x0004E8AE
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x000506B6 File Offset: 0x0004E8B6
		public int DeprecatedMaxLosses
		{
			get
			{
				return this._DeprecatedMaxLosses;
			}
			set
			{
				this._DeprecatedMaxLosses = value;
				this.HasDeprecatedMaxLosses = true;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x000506C6 File Offset: 0x0004E8C6
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x000506CE File Offset: 0x0004E8CE
		public ulong DeprecatedClosedToNewSessionsSecondsFromNow
		{
			get
			{
				return this._DeprecatedClosedToNewSessionsSecondsFromNow;
			}
			set
			{
				this._DeprecatedClosedToNewSessionsSecondsFromNow = value;
				this.HasDeprecatedClosedToNewSessionsSecondsFromNow = true;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000506DE File Offset: 0x0004E8DE
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x000506E6 File Offset: 0x0004E8E6
		public int DeprecatedMaxSessions
		{
			get
			{
				return this._DeprecatedMaxSessions;
			}
			set
			{
				this._DeprecatedMaxSessions = value;
				this.HasDeprecatedMaxSessions = true;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000506F6 File Offset: 0x0004E8F6
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x000506FE File Offset: 0x0004E8FE
		public bool DeprecatedFriendlyChallengeDisabled
		{
			get
			{
				return this._DeprecatedFriendlyChallengeDisabled;
			}
			set
			{
				this._DeprecatedFriendlyChallengeDisabled = value;
				this.HasDeprecatedFriendlyChallengeDisabled = true;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005070E File Offset: 0x0004E90E
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x00050716 File Offset: 0x0004E916
		public int DeprecatedFirstTimeSeenDialogId
		{
			get
			{
				return this._DeprecatedFirstTimeSeenDialogId;
			}
			set
			{
				this._DeprecatedFirstTimeSeenDialogId = value;
				this.HasDeprecatedFirstTimeSeenDialogId = true;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00050726 File Offset: 0x0004E926
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0005072E File Offset: 0x0004E92E
		public int DeprecatedRewardTriggerQuota
		{
			get
			{
				return this._DeprecatedRewardTriggerQuota;
			}
			set
			{
				this._DeprecatedRewardTriggerQuota = value;
				this.HasDeprecatedRewardTriggerQuota = true;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0005073E File Offset: 0x0004E93E
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x00050746 File Offset: 0x0004E946
		public uint DeprecatedFreeSessions
		{
			get
			{
				return this._DeprecatedFreeSessions;
			}
			set
			{
				this._DeprecatedFreeSessions = value;
				this.HasDeprecatedFreeSessions = true;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00050756 File Offset: 0x0004E956
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x0005075E File Offset: 0x0004E95E
		public bool DeprecatedIsPrerelease
		{
			get
			{
				return this._DeprecatedIsPrerelease;
			}
			set
			{
				this._DeprecatedIsPrerelease = value;
				this.HasDeprecatedIsPrerelease = true;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005076E File Offset: 0x0004E96E
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00050776 File Offset: 0x0004E976
		public List<AssetRecordInfo> DeprecatedAdditionalAssets
		{
			get
			{
				return this._DeprecatedAdditionalAssets;
			}
			set
			{
				this._DeprecatedAdditionalAssets = value;
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00050780 File Offset: 0x0004E980
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEndSecondsFromNow)
			{
				num ^= this.EndSecondsFromNow.GetHashCode();
			}
			num ^= this.SeasonId.GetHashCode();
			if (this.HasSeasonEndSecondSpreadCount)
			{
				num ^= this.SeasonEndSecondSpreadCount.GetHashCode();
			}
			foreach (GameContentScenario gameContentScenario in this.Scenarios)
			{
				num ^= gameContentScenario.GetHashCode();
			}
			if (this.HasDeprecatedScenarioId)
			{
				num ^= this.DeprecatedScenarioId.GetHashCode();
			}
			if (this.HasDeprecatedScenarioRecordByteSize)
			{
				num ^= this.DeprecatedScenarioRecordByteSize.GetHashCode();
			}
			if (this.HasDeprecatedScenarioRecordHash)
			{
				num ^= this.DeprecatedScenarioRecordHash.GetHashCode();
			}
			if (this.HasDeprecatedRewardType)
			{
				num ^= this.DeprecatedRewardType.GetHashCode();
			}
			if (this.HasDeprecatedRewardData1)
			{
				num ^= this.DeprecatedRewardData1.GetHashCode();
			}
			if (this.HasDeprecatedRewardData2)
			{
				num ^= this.DeprecatedRewardData2.GetHashCode();
			}
			if (this.HasDeprecatedRewardTrigger)
			{
				num ^= this.DeprecatedRewardTrigger.GetHashCode();
			}
			if (this.HasDeprecatedFormatType)
			{
				num ^= this.DeprecatedFormatType.GetHashCode();
			}
			if (this.HasDeprecatedTicketType)
			{
				num ^= this.DeprecatedTicketType.GetHashCode();
			}
			if (this.HasDeprecatedMaxWins)
			{
				num ^= this.DeprecatedMaxWins.GetHashCode();
			}
			if (this.HasDeprecatedMaxLosses)
			{
				num ^= this.DeprecatedMaxLosses.GetHashCode();
			}
			if (this.HasDeprecatedClosedToNewSessionsSecondsFromNow)
			{
				num ^= this.DeprecatedClosedToNewSessionsSecondsFromNow.GetHashCode();
			}
			if (this.HasDeprecatedMaxSessions)
			{
				num ^= this.DeprecatedMaxSessions.GetHashCode();
			}
			if (this.HasDeprecatedFriendlyChallengeDisabled)
			{
				num ^= this.DeprecatedFriendlyChallengeDisabled.GetHashCode();
			}
			if (this.HasDeprecatedFirstTimeSeenDialogId)
			{
				num ^= this.DeprecatedFirstTimeSeenDialogId.GetHashCode();
			}
			if (this.HasDeprecatedRewardTriggerQuota)
			{
				num ^= this.DeprecatedRewardTriggerQuota.GetHashCode();
			}
			if (this.HasDeprecatedFreeSessions)
			{
				num ^= this.DeprecatedFreeSessions.GetHashCode();
			}
			if (this.HasDeprecatedIsPrerelease)
			{
				num ^= this.DeprecatedIsPrerelease.GetHashCode();
			}
			foreach (AssetRecordInfo assetRecordInfo in this.DeprecatedAdditionalAssets)
			{
				num ^= assetRecordInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00050A48 File Offset: 0x0004EC48
		public override bool Equals(object obj)
		{
			GameContentSeasonSpec gameContentSeasonSpec = obj as GameContentSeasonSpec;
			if (gameContentSeasonSpec == null)
			{
				return false;
			}
			if (this.HasEndSecondsFromNow != gameContentSeasonSpec.HasEndSecondsFromNow || (this.HasEndSecondsFromNow && !this.EndSecondsFromNow.Equals(gameContentSeasonSpec.EndSecondsFromNow)))
			{
				return false;
			}
			if (!this.SeasonId.Equals(gameContentSeasonSpec.SeasonId))
			{
				return false;
			}
			if (this.HasSeasonEndSecondSpreadCount != gameContentSeasonSpec.HasSeasonEndSecondSpreadCount || (this.HasSeasonEndSecondSpreadCount && !this.SeasonEndSecondSpreadCount.Equals(gameContentSeasonSpec.SeasonEndSecondSpreadCount)))
			{
				return false;
			}
			if (this.Scenarios.Count != gameContentSeasonSpec.Scenarios.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Scenarios.Count; i++)
			{
				if (!this.Scenarios[i].Equals(gameContentSeasonSpec.Scenarios[i]))
				{
					return false;
				}
			}
			if (this.HasDeprecatedScenarioId != gameContentSeasonSpec.HasDeprecatedScenarioId || (this.HasDeprecatedScenarioId && !this.DeprecatedScenarioId.Equals(gameContentSeasonSpec.DeprecatedScenarioId)))
			{
				return false;
			}
			if (this.HasDeprecatedScenarioRecordByteSize != gameContentSeasonSpec.HasDeprecatedScenarioRecordByteSize || (this.HasDeprecatedScenarioRecordByteSize && !this.DeprecatedScenarioRecordByteSize.Equals(gameContentSeasonSpec.DeprecatedScenarioRecordByteSize)))
			{
				return false;
			}
			if (this.HasDeprecatedScenarioRecordHash != gameContentSeasonSpec.HasDeprecatedScenarioRecordHash || (this.HasDeprecatedScenarioRecordHash && !this.DeprecatedScenarioRecordHash.Equals(gameContentSeasonSpec.DeprecatedScenarioRecordHash)))
			{
				return false;
			}
			if (this.HasDeprecatedRewardType != gameContentSeasonSpec.HasDeprecatedRewardType || (this.HasDeprecatedRewardType && !this.DeprecatedRewardType.Equals(gameContentSeasonSpec.DeprecatedRewardType)))
			{
				return false;
			}
			if (this.HasDeprecatedRewardData1 != gameContentSeasonSpec.HasDeprecatedRewardData1 || (this.HasDeprecatedRewardData1 && !this.DeprecatedRewardData1.Equals(gameContentSeasonSpec.DeprecatedRewardData1)))
			{
				return false;
			}
			if (this.HasDeprecatedRewardData2 != gameContentSeasonSpec.HasDeprecatedRewardData2 || (this.HasDeprecatedRewardData2 && !this.DeprecatedRewardData2.Equals(gameContentSeasonSpec.DeprecatedRewardData2)))
			{
				return false;
			}
			if (this.HasDeprecatedRewardTrigger != gameContentSeasonSpec.HasDeprecatedRewardTrigger || (this.HasDeprecatedRewardTrigger && !this.DeprecatedRewardTrigger.Equals(gameContentSeasonSpec.DeprecatedRewardTrigger)))
			{
				return false;
			}
			if (this.HasDeprecatedFormatType != gameContentSeasonSpec.HasDeprecatedFormatType || (this.HasDeprecatedFormatType && !this.DeprecatedFormatType.Equals(gameContentSeasonSpec.DeprecatedFormatType)))
			{
				return false;
			}
			if (this.HasDeprecatedTicketType != gameContentSeasonSpec.HasDeprecatedTicketType || (this.HasDeprecatedTicketType && !this.DeprecatedTicketType.Equals(gameContentSeasonSpec.DeprecatedTicketType)))
			{
				return false;
			}
			if (this.HasDeprecatedMaxWins != gameContentSeasonSpec.HasDeprecatedMaxWins || (this.HasDeprecatedMaxWins && !this.DeprecatedMaxWins.Equals(gameContentSeasonSpec.DeprecatedMaxWins)))
			{
				return false;
			}
			if (this.HasDeprecatedMaxLosses != gameContentSeasonSpec.HasDeprecatedMaxLosses || (this.HasDeprecatedMaxLosses && !this.DeprecatedMaxLosses.Equals(gameContentSeasonSpec.DeprecatedMaxLosses)))
			{
				return false;
			}
			if (this.HasDeprecatedClosedToNewSessionsSecondsFromNow != gameContentSeasonSpec.HasDeprecatedClosedToNewSessionsSecondsFromNow || (this.HasDeprecatedClosedToNewSessionsSecondsFromNow && !this.DeprecatedClosedToNewSessionsSecondsFromNow.Equals(gameContentSeasonSpec.DeprecatedClosedToNewSessionsSecondsFromNow)))
			{
				return false;
			}
			if (this.HasDeprecatedMaxSessions != gameContentSeasonSpec.HasDeprecatedMaxSessions || (this.HasDeprecatedMaxSessions && !this.DeprecatedMaxSessions.Equals(gameContentSeasonSpec.DeprecatedMaxSessions)))
			{
				return false;
			}
			if (this.HasDeprecatedFriendlyChallengeDisabled != gameContentSeasonSpec.HasDeprecatedFriendlyChallengeDisabled || (this.HasDeprecatedFriendlyChallengeDisabled && !this.DeprecatedFriendlyChallengeDisabled.Equals(gameContentSeasonSpec.DeprecatedFriendlyChallengeDisabled)))
			{
				return false;
			}
			if (this.HasDeprecatedFirstTimeSeenDialogId != gameContentSeasonSpec.HasDeprecatedFirstTimeSeenDialogId || (this.HasDeprecatedFirstTimeSeenDialogId && !this.DeprecatedFirstTimeSeenDialogId.Equals(gameContentSeasonSpec.DeprecatedFirstTimeSeenDialogId)))
			{
				return false;
			}
			if (this.HasDeprecatedRewardTriggerQuota != gameContentSeasonSpec.HasDeprecatedRewardTriggerQuota || (this.HasDeprecatedRewardTriggerQuota && !this.DeprecatedRewardTriggerQuota.Equals(gameContentSeasonSpec.DeprecatedRewardTriggerQuota)))
			{
				return false;
			}
			if (this.HasDeprecatedFreeSessions != gameContentSeasonSpec.HasDeprecatedFreeSessions || (this.HasDeprecatedFreeSessions && !this.DeprecatedFreeSessions.Equals(gameContentSeasonSpec.DeprecatedFreeSessions)))
			{
				return false;
			}
			if (this.HasDeprecatedIsPrerelease != gameContentSeasonSpec.HasDeprecatedIsPrerelease || (this.HasDeprecatedIsPrerelease && !this.DeprecatedIsPrerelease.Equals(gameContentSeasonSpec.DeprecatedIsPrerelease)))
			{
				return false;
			}
			if (this.DeprecatedAdditionalAssets.Count != gameContentSeasonSpec.DeprecatedAdditionalAssets.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DeprecatedAdditionalAssets.Count; j++)
			{
				if (!this.DeprecatedAdditionalAssets[j].Equals(gameContentSeasonSpec.DeprecatedAdditionalAssets[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00050EE1 File Offset: 0x0004F0E1
		public void Deserialize(Stream stream)
		{
			GameContentSeasonSpec.Deserialize(stream, this);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00050EEB File Offset: 0x0004F0EB
		public static GameContentSeasonSpec Deserialize(Stream stream, GameContentSeasonSpec instance)
		{
			return GameContentSeasonSpec.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		public static GameContentSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			GameContentSeasonSpec gameContentSeasonSpec = new GameContentSeasonSpec();
			GameContentSeasonSpec.DeserializeLengthDelimited(stream, gameContentSeasonSpec);
			return gameContentSeasonSpec;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00050F14 File Offset: 0x0004F114
		public static GameContentSeasonSpec DeserializeLengthDelimited(Stream stream, GameContentSeasonSpec instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameContentSeasonSpec.Deserialize(stream, instance, num);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00050F3C File Offset: 0x0004F13C
		public static GameContentSeasonSpec Deserialize(Stream stream, GameContentSeasonSpec instance, long limit)
		{
			if (instance.Scenarios == null)
			{
				instance.Scenarios = new List<GameContentScenario>();
			}
			instance.DeprecatedRewardType = RewardType.REWARD_UNKNOWN;
			instance.DeprecatedRewardTrigger = RewardTrigger.REWARD_TRIGGER_UNKNOWN;
			instance.DeprecatedFormatType = FormatType.FT_UNKNOWN;
			if (instance.DeprecatedAdditionalAssets == null)
			{
				instance.DeprecatedAdditionalAssets = new List<AssetRecordInfo>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 56)
					{
						if (num <= 24)
						{
							if (num == 8)
							{
								instance.EndSecondsFromNow = ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.DeprecatedScenarioId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 24)
							{
								instance.DeprecatedScenarioRecordByteSize = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 34)
							{
								instance.DeprecatedScenarioRecordHash = ProtocolParser.ReadBytes(stream);
								continue;
							}
							if (num == 40)
							{
								instance.DeprecatedRewardType = (RewardType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.DeprecatedRewardData1 = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 56)
							{
								instance.DeprecatedRewardData2 = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num == 64)
						{
							instance.DeprecatedRewardTrigger = (RewardTrigger)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.DeprecatedFormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 88)
						{
							instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.DeprecatedTicketType = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.DeprecatedMaxWins = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.DeprecatedMaxLosses = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 120)
						{
							instance.DeprecatedClosedToNewSessionsSecondsFromNow = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					switch (field)
					{
					case 16U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedMaxSessions = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFriendlyChallengeDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonEndSecondSpreadCount = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFirstTimeSeenDialogId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedRewardTriggerQuota = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 21U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFreeSessions = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 22U:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedIsPrerelease = ProtocolParser.ReadBool(stream);
						}
						break;
					case 23U:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Scenarios.Add(GameContentScenario.DeserializeLengthDelimited(stream));
						}
						break;
					default:
						if (field != 100U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeprecatedAdditionalAssets.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
						}
						break;
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000512C8 File Offset: 0x0004F4C8
		public void Serialize(Stream stream)
		{
			GameContentSeasonSpec.Serialize(stream, this);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000512D4 File Offset: 0x0004F4D4
		public static void Serialize(Stream stream, GameContentSeasonSpec instance)
		{
			if (instance.HasEndSecondsFromNow)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.EndSecondsFromNow);
			}
			stream.WriteByte(88);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonId));
			if (instance.HasSeasonEndSecondSpreadCount)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonEndSecondSpreadCount));
			}
			if (instance.Scenarios.Count > 0)
			{
				foreach (GameContentScenario gameContentScenario in instance.Scenarios)
				{
					stream.WriteByte(186);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, gameContentScenario.GetSerializedSize());
					GameContentScenario.Serialize(stream, gameContentScenario);
				}
			}
			if (instance.HasDeprecatedScenarioId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedScenarioId));
			}
			if (instance.HasDeprecatedScenarioRecordByteSize)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.DeprecatedScenarioRecordByteSize);
			}
			if (instance.HasDeprecatedScenarioRecordHash)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.DeprecatedScenarioRecordHash);
			}
			if (instance.HasDeprecatedRewardType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedRewardType));
			}
			if (instance.HasDeprecatedRewardData1)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedRewardData1);
			}
			if (instance.HasDeprecatedRewardData2)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedRewardData2);
			}
			if (instance.HasDeprecatedRewardTrigger)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedRewardTrigger));
			}
			if (instance.HasDeprecatedFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedFormatType));
			}
			if (instance.HasDeprecatedTicketType)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedTicketType));
			}
			if (instance.HasDeprecatedMaxWins)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedMaxWins));
			}
			if (instance.HasDeprecatedMaxLosses)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedMaxLosses));
			}
			if (instance.HasDeprecatedClosedToNewSessionsSecondsFromNow)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, instance.DeprecatedClosedToNewSessionsSecondsFromNow);
			}
			if (instance.HasDeprecatedMaxSessions)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedMaxSessions));
			}
			if (instance.HasDeprecatedFriendlyChallengeDisabled)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.DeprecatedFriendlyChallengeDisabled);
			}
			if (instance.HasDeprecatedFirstTimeSeenDialogId)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedFirstTimeSeenDialogId));
			}
			if (instance.HasDeprecatedRewardTriggerQuota)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeprecatedRewardTriggerQuota));
			}
			if (instance.HasDeprecatedFreeSessions)
			{
				stream.WriteByte(168);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.DeprecatedFreeSessions);
			}
			if (instance.HasDeprecatedIsPrerelease)
			{
				stream.WriteByte(176);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.DeprecatedIsPrerelease);
			}
			if (instance.DeprecatedAdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in instance.DeprecatedAdditionalAssets)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, assetRecordInfo.GetSerializedSize());
					AssetRecordInfo.Serialize(stream, assetRecordInfo);
				}
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00051658 File Offset: 0x0004F858
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEndSecondsFromNow)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.EndSecondsFromNow);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonId));
			if (this.HasSeasonEndSecondSpreadCount)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonEndSecondSpreadCount));
			}
			if (this.Scenarios.Count > 0)
			{
				foreach (GameContentScenario gameContentScenario in this.Scenarios)
				{
					num += 2U;
					uint serializedSize = gameContentScenario.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDeprecatedScenarioId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedScenarioId));
			}
			if (this.HasDeprecatedScenarioRecordByteSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.DeprecatedScenarioRecordByteSize);
			}
			if (this.HasDeprecatedScenarioRecordHash)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.DeprecatedScenarioRecordHash.Length) + (uint)this.DeprecatedScenarioRecordHash.Length;
			}
			if (this.HasDeprecatedRewardType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedRewardType));
			}
			if (this.HasDeprecatedRewardData1)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeprecatedRewardData1);
			}
			if (this.HasDeprecatedRewardData2)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeprecatedRewardData2);
			}
			if (this.HasDeprecatedRewardTrigger)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedRewardTrigger));
			}
			if (this.HasDeprecatedFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedFormatType));
			}
			if (this.HasDeprecatedTicketType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedTicketType));
			}
			if (this.HasDeprecatedMaxWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedMaxWins));
			}
			if (this.HasDeprecatedMaxLosses)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedMaxLosses));
			}
			if (this.HasDeprecatedClosedToNewSessionsSecondsFromNow)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.DeprecatedClosedToNewSessionsSecondsFromNow);
			}
			if (this.HasDeprecatedMaxSessions)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedMaxSessions));
			}
			if (this.HasDeprecatedFriendlyChallengeDisabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasDeprecatedFirstTimeSeenDialogId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedFirstTimeSeenDialogId));
			}
			if (this.HasDeprecatedRewardTriggerQuota)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeprecatedRewardTriggerQuota));
			}
			if (this.HasDeprecatedFreeSessions)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.DeprecatedFreeSessions);
			}
			if (this.HasDeprecatedIsPrerelease)
			{
				num += 2U;
				num += 1U;
			}
			if (this.DeprecatedAdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in this.DeprecatedAdditionalAssets)
				{
					num += 2U;
					uint serializedSize2 = assetRecordInfo.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000734 RID: 1844
		public bool HasEndSecondsFromNow;

		// Token: 0x04000735 RID: 1845
		private ulong _EndSecondsFromNow;

		// Token: 0x04000737 RID: 1847
		public bool HasSeasonEndSecondSpreadCount;

		// Token: 0x04000738 RID: 1848
		private int _SeasonEndSecondSpreadCount;

		// Token: 0x04000739 RID: 1849
		private List<GameContentScenario> _Scenarios = new List<GameContentScenario>();

		// Token: 0x0400073A RID: 1850
		public bool HasDeprecatedScenarioId;

		// Token: 0x0400073B RID: 1851
		private int _DeprecatedScenarioId;

		// Token: 0x0400073C RID: 1852
		public bool HasDeprecatedScenarioRecordByteSize;

		// Token: 0x0400073D RID: 1853
		private uint _DeprecatedScenarioRecordByteSize;

		// Token: 0x0400073E RID: 1854
		public bool HasDeprecatedScenarioRecordHash;

		// Token: 0x0400073F RID: 1855
		private byte[] _DeprecatedScenarioRecordHash;

		// Token: 0x04000740 RID: 1856
		public bool HasDeprecatedRewardType;

		// Token: 0x04000741 RID: 1857
		private RewardType _DeprecatedRewardType;

		// Token: 0x04000742 RID: 1858
		public bool HasDeprecatedRewardData1;

		// Token: 0x04000743 RID: 1859
		private long _DeprecatedRewardData1;

		// Token: 0x04000744 RID: 1860
		public bool HasDeprecatedRewardData2;

		// Token: 0x04000745 RID: 1861
		private long _DeprecatedRewardData2;

		// Token: 0x04000746 RID: 1862
		public bool HasDeprecatedRewardTrigger;

		// Token: 0x04000747 RID: 1863
		private RewardTrigger _DeprecatedRewardTrigger;

		// Token: 0x04000748 RID: 1864
		public bool HasDeprecatedFormatType;

		// Token: 0x04000749 RID: 1865
		private FormatType _DeprecatedFormatType;

		// Token: 0x0400074A RID: 1866
		public bool HasDeprecatedTicketType;

		// Token: 0x0400074B RID: 1867
		private int _DeprecatedTicketType;

		// Token: 0x0400074C RID: 1868
		public bool HasDeprecatedMaxWins;

		// Token: 0x0400074D RID: 1869
		private int _DeprecatedMaxWins;

		// Token: 0x0400074E RID: 1870
		public bool HasDeprecatedMaxLosses;

		// Token: 0x0400074F RID: 1871
		private int _DeprecatedMaxLosses;

		// Token: 0x04000750 RID: 1872
		public bool HasDeprecatedClosedToNewSessionsSecondsFromNow;

		// Token: 0x04000751 RID: 1873
		private ulong _DeprecatedClosedToNewSessionsSecondsFromNow;

		// Token: 0x04000752 RID: 1874
		public bool HasDeprecatedMaxSessions;

		// Token: 0x04000753 RID: 1875
		private int _DeprecatedMaxSessions;

		// Token: 0x04000754 RID: 1876
		public bool HasDeprecatedFriendlyChallengeDisabled;

		// Token: 0x04000755 RID: 1877
		private bool _DeprecatedFriendlyChallengeDisabled;

		// Token: 0x04000756 RID: 1878
		public bool HasDeprecatedFirstTimeSeenDialogId;

		// Token: 0x04000757 RID: 1879
		private int _DeprecatedFirstTimeSeenDialogId;

		// Token: 0x04000758 RID: 1880
		public bool HasDeprecatedRewardTriggerQuota;

		// Token: 0x04000759 RID: 1881
		private int _DeprecatedRewardTriggerQuota;

		// Token: 0x0400075A RID: 1882
		public bool HasDeprecatedFreeSessions;

		// Token: 0x0400075B RID: 1883
		private uint _DeprecatedFreeSessions;

		// Token: 0x0400075C RID: 1884
		public bool HasDeprecatedIsPrerelease;

		// Token: 0x0400075D RID: 1885
		private bool _DeprecatedIsPrerelease;

		// Token: 0x0400075E RID: 1886
		private List<AssetRecordInfo> _DeprecatedAdditionalAssets = new List<AssetRecordInfo>();
	}
}
