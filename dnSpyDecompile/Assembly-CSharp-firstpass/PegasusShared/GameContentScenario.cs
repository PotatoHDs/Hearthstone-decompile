using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200015C RID: 348
	public class GameContentScenario : IProtoBuf
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00051966 File Offset: 0x0004FB66
		// (set) Token: 0x06001781 RID: 6017 RVA: 0x0005196E File Offset: 0x0004FB6E
		public int LibraryItemId { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00051977 File Offset: 0x0004FB77
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x0005197F File Offset: 0x0004FB7F
		public bool IsRequired { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00051988 File Offset: 0x0004FB88
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00051990 File Offset: 0x0004FB90
		public bool IsFallback { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00051999 File Offset: 0x0004FB99
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x000519A1 File Offset: 0x0004FBA1
		public int ScenarioId { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000519AA File Offset: 0x0004FBAA
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x000519B2 File Offset: 0x0004FBB2
		public uint ScenarioRecordByteSize { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000519BB File Offset: 0x0004FBBB
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x000519C3 File Offset: 0x0004FBC3
		public byte[] ScenarioRecordHash { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x000519CC File Offset: 0x0004FBCC
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x000519D4 File Offset: 0x0004FBD4
		public FormatType FormatType { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x000519DD File Offset: 0x0004FBDD
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x000519E5 File Offset: 0x0004FBE5
		public RewardType RewardType
		{
			get
			{
				return this._RewardType;
			}
			set
			{
				this._RewardType = value;
				this.HasRewardType = true;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x000519F5 File Offset: 0x0004FBF5
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x000519FD File Offset: 0x0004FBFD
		public long RewardData1
		{
			get
			{
				return this._RewardData1;
			}
			set
			{
				this._RewardData1 = value;
				this.HasRewardData1 = true;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00051A0D File Offset: 0x0004FC0D
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x00051A15 File Offset: 0x0004FC15
		public long RewardData2
		{
			get
			{
				return this._RewardData2;
			}
			set
			{
				this._RewardData2 = value;
				this.HasRewardData2 = true;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00051A25 File Offset: 0x0004FC25
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x00051A2D File Offset: 0x0004FC2D
		public RewardTrigger RewardTrigger
		{
			get
			{
				return this._RewardTrigger;
			}
			set
			{
				this._RewardTrigger = value;
				this.HasRewardTrigger = true;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00051A3D File Offset: 0x0004FC3D
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x00051A45 File Offset: 0x0004FC45
		public int RewardTriggerQuota
		{
			get
			{
				return this._RewardTriggerQuota;
			}
			set
			{
				this._RewardTriggerQuota = value;
				this.HasRewardTriggerQuota = true;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00051A55 File Offset: 0x0004FC55
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00051A5D File Offset: 0x0004FC5D
		public TavernBrawlMode BrawlMode
		{
			get
			{
				return this._BrawlMode;
			}
			set
			{
				this._BrawlMode = value;
				this.HasBrawlMode = true;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00051A6D File Offset: 0x0004FC6D
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00051A75 File Offset: 0x0004FC75
		public int TicketType
		{
			get
			{
				return this._TicketType;
			}
			set
			{
				this._TicketType = value;
				this.HasTicketType = true;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00051A85 File Offset: 0x0004FC85
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00051A8D File Offset: 0x0004FC8D
		public int MaxWins
		{
			get
			{
				return this._MaxWins;
			}
			set
			{
				this._MaxWins = value;
				this.HasMaxWins = true;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00051A9D File Offset: 0x0004FC9D
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00051AA5 File Offset: 0x0004FCA5
		public int MaxLosses
		{
			get
			{
				return this._MaxLosses;
			}
			set
			{
				this._MaxLosses = value;
				this.HasMaxLosses = true;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00051AB5 File Offset: 0x0004FCB5
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x00051ABD File Offset: 0x0004FCBD
		public int MaxSessions
		{
			get
			{
				return this._MaxSessions;
			}
			set
			{
				this._MaxSessions = value;
				this.HasMaxSessions = true;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00051ACD File Offset: 0x0004FCCD
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00051AD5 File Offset: 0x0004FCD5
		public uint FreeSessions
		{
			get
			{
				return this._FreeSessions;
			}
			set
			{
				this._FreeSessions = value;
				this.HasFreeSessions = true;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00051AE5 File Offset: 0x0004FCE5
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x00051AED File Offset: 0x0004FCED
		public ulong ClosedToNewSessionsSecondsFromNow
		{
			get
			{
				return this._ClosedToNewSessionsSecondsFromNow;
			}
			set
			{
				this._ClosedToNewSessionsSecondsFromNow = value;
				this.HasClosedToNewSessionsSecondsFromNow = true;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00051AFD File Offset: 0x0004FCFD
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x00051B05 File Offset: 0x0004FD05
		public bool FriendlyChallengeDisabled
		{
			get
			{
				return this._FriendlyChallengeDisabled;
			}
			set
			{
				this._FriendlyChallengeDisabled = value;
				this.HasFriendlyChallengeDisabled = true;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00051B15 File Offset: 0x0004FD15
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x00051B1D File Offset: 0x0004FD1D
		public int FirstTimeSeenDialogId
		{
			get
			{
				return this._FirstTimeSeenDialogId;
			}
			set
			{
				this._FirstTimeSeenDialogId = value;
				this.HasFirstTimeSeenDialogId = true;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00051B2D File Offset: 0x0004FD2D
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00051B35 File Offset: 0x0004FD35
		public bool IsPrerelease
		{
			get
			{
				return this._IsPrerelease;
			}
			set
			{
				this._IsPrerelease = value;
				this.HasIsPrerelease = true;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00051B45 File Offset: 0x0004FD45
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x00051B4D File Offset: 0x0004FD4D
		public List<AssetRecordInfo> AdditionalAssets
		{
			get
			{
				return this._AdditionalAssets;
			}
			set
			{
				this._AdditionalAssets = value;
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00051B58 File Offset: 0x0004FD58
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.LibraryItemId.GetHashCode();
			num ^= this.IsRequired.GetHashCode();
			num ^= this.IsFallback.GetHashCode();
			num ^= this.ScenarioId.GetHashCode();
			num ^= this.ScenarioRecordByteSize.GetHashCode();
			num ^= this.ScenarioRecordHash.GetHashCode();
			num ^= this.FormatType.GetHashCode();
			if (this.HasRewardType)
			{
				num ^= this.RewardType.GetHashCode();
			}
			if (this.HasRewardData1)
			{
				num ^= this.RewardData1.GetHashCode();
			}
			if (this.HasRewardData2)
			{
				num ^= this.RewardData2.GetHashCode();
			}
			if (this.HasRewardTrigger)
			{
				num ^= this.RewardTrigger.GetHashCode();
			}
			if (this.HasRewardTriggerQuota)
			{
				num ^= this.RewardTriggerQuota.GetHashCode();
			}
			if (this.HasBrawlMode)
			{
				num ^= this.BrawlMode.GetHashCode();
			}
			if (this.HasTicketType)
			{
				num ^= this.TicketType.GetHashCode();
			}
			if (this.HasMaxWins)
			{
				num ^= this.MaxWins.GetHashCode();
			}
			if (this.HasMaxLosses)
			{
				num ^= this.MaxLosses.GetHashCode();
			}
			if (this.HasMaxSessions)
			{
				num ^= this.MaxSessions.GetHashCode();
			}
			if (this.HasFreeSessions)
			{
				num ^= this.FreeSessions.GetHashCode();
			}
			if (this.HasClosedToNewSessionsSecondsFromNow)
			{
				num ^= this.ClosedToNewSessionsSecondsFromNow.GetHashCode();
			}
			if (this.HasFriendlyChallengeDisabled)
			{
				num ^= this.FriendlyChallengeDisabled.GetHashCode();
			}
			if (this.HasFirstTimeSeenDialogId)
			{
				num ^= this.FirstTimeSeenDialogId.GetHashCode();
			}
			if (this.HasIsPrerelease)
			{
				num ^= this.IsPrerelease.GetHashCode();
			}
			foreach (AssetRecordInfo assetRecordInfo in this.AdditionalAssets)
			{
				num ^= assetRecordInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00051DC8 File Offset: 0x0004FFC8
		public override bool Equals(object obj)
		{
			GameContentScenario gameContentScenario = obj as GameContentScenario;
			if (gameContentScenario == null)
			{
				return false;
			}
			if (!this.LibraryItemId.Equals(gameContentScenario.LibraryItemId))
			{
				return false;
			}
			if (!this.IsRequired.Equals(gameContentScenario.IsRequired))
			{
				return false;
			}
			if (!this.IsFallback.Equals(gameContentScenario.IsFallback))
			{
				return false;
			}
			if (!this.ScenarioId.Equals(gameContentScenario.ScenarioId))
			{
				return false;
			}
			if (!this.ScenarioRecordByteSize.Equals(gameContentScenario.ScenarioRecordByteSize))
			{
				return false;
			}
			if (!this.ScenarioRecordHash.Equals(gameContentScenario.ScenarioRecordHash))
			{
				return false;
			}
			if (!this.FormatType.Equals(gameContentScenario.FormatType))
			{
				return false;
			}
			if (this.HasRewardType != gameContentScenario.HasRewardType || (this.HasRewardType && !this.RewardType.Equals(gameContentScenario.RewardType)))
			{
				return false;
			}
			if (this.HasRewardData1 != gameContentScenario.HasRewardData1 || (this.HasRewardData1 && !this.RewardData1.Equals(gameContentScenario.RewardData1)))
			{
				return false;
			}
			if (this.HasRewardData2 != gameContentScenario.HasRewardData2 || (this.HasRewardData2 && !this.RewardData2.Equals(gameContentScenario.RewardData2)))
			{
				return false;
			}
			if (this.HasRewardTrigger != gameContentScenario.HasRewardTrigger || (this.HasRewardTrigger && !this.RewardTrigger.Equals(gameContentScenario.RewardTrigger)))
			{
				return false;
			}
			if (this.HasRewardTriggerQuota != gameContentScenario.HasRewardTriggerQuota || (this.HasRewardTriggerQuota && !this.RewardTriggerQuota.Equals(gameContentScenario.RewardTriggerQuota)))
			{
				return false;
			}
			if (this.HasBrawlMode != gameContentScenario.HasBrawlMode || (this.HasBrawlMode && !this.BrawlMode.Equals(gameContentScenario.BrawlMode)))
			{
				return false;
			}
			if (this.HasTicketType != gameContentScenario.HasTicketType || (this.HasTicketType && !this.TicketType.Equals(gameContentScenario.TicketType)))
			{
				return false;
			}
			if (this.HasMaxWins != gameContentScenario.HasMaxWins || (this.HasMaxWins && !this.MaxWins.Equals(gameContentScenario.MaxWins)))
			{
				return false;
			}
			if (this.HasMaxLosses != gameContentScenario.HasMaxLosses || (this.HasMaxLosses && !this.MaxLosses.Equals(gameContentScenario.MaxLosses)))
			{
				return false;
			}
			if (this.HasMaxSessions != gameContentScenario.HasMaxSessions || (this.HasMaxSessions && !this.MaxSessions.Equals(gameContentScenario.MaxSessions)))
			{
				return false;
			}
			if (this.HasFreeSessions != gameContentScenario.HasFreeSessions || (this.HasFreeSessions && !this.FreeSessions.Equals(gameContentScenario.FreeSessions)))
			{
				return false;
			}
			if (this.HasClosedToNewSessionsSecondsFromNow != gameContentScenario.HasClosedToNewSessionsSecondsFromNow || (this.HasClosedToNewSessionsSecondsFromNow && !this.ClosedToNewSessionsSecondsFromNow.Equals(gameContentScenario.ClosedToNewSessionsSecondsFromNow)))
			{
				return false;
			}
			if (this.HasFriendlyChallengeDisabled != gameContentScenario.HasFriendlyChallengeDisabled || (this.HasFriendlyChallengeDisabled && !this.FriendlyChallengeDisabled.Equals(gameContentScenario.FriendlyChallengeDisabled)))
			{
				return false;
			}
			if (this.HasFirstTimeSeenDialogId != gameContentScenario.HasFirstTimeSeenDialogId || (this.HasFirstTimeSeenDialogId && !this.FirstTimeSeenDialogId.Equals(gameContentScenario.FirstTimeSeenDialogId)))
			{
				return false;
			}
			if (this.HasIsPrerelease != gameContentScenario.HasIsPrerelease || (this.HasIsPrerelease && !this.IsPrerelease.Equals(gameContentScenario.IsPrerelease)))
			{
				return false;
			}
			if (this.AdditionalAssets.Count != gameContentScenario.AdditionalAssets.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AdditionalAssets.Count; i++)
			{
				if (!this.AdditionalAssets[i].Equals(gameContentScenario.AdditionalAssets[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000521C3 File Offset: 0x000503C3
		public void Deserialize(Stream stream)
		{
			GameContentScenario.Deserialize(stream, this);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000521CD File Offset: 0x000503CD
		public static GameContentScenario Deserialize(Stream stream, GameContentScenario instance)
		{
			return GameContentScenario.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000521D8 File Offset: 0x000503D8
		public static GameContentScenario DeserializeLengthDelimited(Stream stream)
		{
			GameContentScenario gameContentScenario = new GameContentScenario();
			GameContentScenario.DeserializeLengthDelimited(stream, gameContentScenario);
			return gameContentScenario;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000521F4 File Offset: 0x000503F4
		public static GameContentScenario DeserializeLengthDelimited(Stream stream, GameContentScenario instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameContentScenario.Deserialize(stream, instance, num);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005221C File Offset: 0x0005041C
		public static GameContentScenario Deserialize(Stream stream, GameContentScenario instance, long limit)
		{
			instance.RewardType = RewardType.REWARD_UNKNOWN;
			instance.RewardTrigger = RewardTrigger.REWARD_TRIGGER_UNKNOWN;
			instance.BrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
			if (instance.AdditionalAssets == null)
			{
				instance.AdditionalAssets = new List<AssetRecordInfo>();
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
								instance.LibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.IsRequired = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 24)
							{
								instance.IsFallback = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else if (num <= 40)
						{
							if (num == 32)
							{
								instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.ScenarioRecordByteSize = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (num == 50)
							{
								instance.ScenarioRecordHash = ProtocolParser.ReadBytes(stream);
								continue;
							}
							if (num == 56)
							{
								instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num <= 72)
						{
							if (num == 64)
							{
								instance.RewardType = (RewardType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 72)
							{
								instance.RewardData1 = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 80)
							{
								instance.RewardData2 = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 88)
							{
								instance.RewardTrigger = (RewardTrigger)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 104)
					{
						if (num == 96)
						{
							instance.RewardTriggerQuota = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 104)
						{
							instance.BrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.TicketType = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 120)
						{
							instance.MaxWins = (int)ProtocolParser.ReadUInt64(stream);
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
							instance.MaxLosses = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17U:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxSessions = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18U:
						if (key.WireType == Wire.Varint)
						{
							instance.FreeSessions = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 19U:
						if (key.WireType == Wire.Varint)
						{
							instance.ClosedToNewSessionsSecondsFromNow = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20U:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendlyChallengeDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 21U:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstTimeSeenDialogId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 22U:
						if (key.WireType == Wire.Varint)
						{
							instance.IsPrerelease = ProtocolParser.ReadBool(stream);
						}
						break;
					default:
						if (field != 100U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.LengthDelimited)
						{
							instance.AdditionalAssets.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x060017B5 RID: 6069 RVA: 0x00052595 File Offset: 0x00050795
		public void Serialize(Stream stream)
		{
			GameContentScenario.Serialize(stream, this);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000525A0 File Offset: 0x000507A0
		public static void Serialize(Stream stream, GameContentScenario instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LibraryItemId));
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.IsRequired);
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.IsFallback);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt32(stream, instance.ScenarioRecordByteSize);
			if (instance.ScenarioRecordHash == null)
			{
				throw new ArgumentNullException("ScenarioRecordHash", "Required by proto specification.");
			}
			stream.WriteByte(50);
			ProtocolParser.WriteBytes(stream, instance.ScenarioRecordHash);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			if (instance.HasRewardType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardType));
			}
			if (instance.HasRewardData1)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardData1);
			}
			if (instance.HasRewardData2)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardData2);
			}
			if (instance.HasRewardTrigger)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTrigger));
			}
			if (instance.HasRewardTriggerQuota)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardTriggerQuota));
			}
			if (instance.HasBrawlMode)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlMode));
			}
			if (instance.HasTicketType)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TicketType));
			}
			if (instance.HasMaxWins)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxWins));
			}
			if (instance.HasMaxLosses)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxLosses));
			}
			if (instance.HasMaxSessions)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxSessions));
			}
			if (instance.HasFreeSessions)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.FreeSessions);
			}
			if (instance.HasClosedToNewSessionsSecondsFromNow)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, instance.ClosedToNewSessionsSecondsFromNow);
			}
			if (instance.HasFriendlyChallengeDisabled)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.FriendlyChallengeDisabled);
			}
			if (instance.HasFirstTimeSeenDialogId)
			{
				stream.WriteByte(168);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FirstTimeSeenDialogId));
			}
			if (instance.HasIsPrerelease)
			{
				stream.WriteByte(176);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.IsPrerelease);
			}
			if (instance.AdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in instance.AdditionalAssets)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, assetRecordInfo.GetSerializedSize());
					AssetRecordInfo.Serialize(stream, assetRecordInfo);
				}
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000528B8 File Offset: 0x00050AB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.LibraryItemId));
			num += 1U;
			num += 1U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId));
			num += ProtocolParser.SizeOfUInt32(this.ScenarioRecordByteSize);
			num += ProtocolParser.SizeOfUInt32(this.ScenarioRecordHash.Length) + (uint)this.ScenarioRecordHash.Length;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			if (this.HasRewardType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardType));
			}
			if (this.HasRewardData1)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.RewardData1);
			}
			if (this.HasRewardData2)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.RewardData2);
			}
			if (this.HasRewardTrigger)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTrigger));
			}
			if (this.HasRewardTriggerQuota)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardTriggerQuota));
			}
			if (this.HasBrawlMode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlMode));
			}
			if (this.HasTicketType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TicketType));
			}
			if (this.HasMaxWins)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxWins));
			}
			if (this.HasMaxLosses)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxLosses));
			}
			if (this.HasMaxSessions)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxSessions));
			}
			if (this.HasFreeSessions)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt32(this.FreeSessions);
			}
			if (this.HasClosedToNewSessionsSecondsFromNow)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64(this.ClosedToNewSessionsSecondsFromNow);
			}
			if (this.HasFriendlyChallengeDisabled)
			{
				num += 2U;
				num += 1U;
			}
			if (this.HasFirstTimeSeenDialogId)
			{
				num += 2U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FirstTimeSeenDialogId));
			}
			if (this.HasIsPrerelease)
			{
				num += 2U;
				num += 1U;
			}
			if (this.AdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo assetRecordInfo in this.AdditionalAssets)
				{
					num += 2U;
					uint serializedSize = assetRecordInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 7U;
			return num;
		}

		// Token: 0x04000766 RID: 1894
		public bool HasRewardType;

		// Token: 0x04000767 RID: 1895
		private RewardType _RewardType;

		// Token: 0x04000768 RID: 1896
		public bool HasRewardData1;

		// Token: 0x04000769 RID: 1897
		private long _RewardData1;

		// Token: 0x0400076A RID: 1898
		public bool HasRewardData2;

		// Token: 0x0400076B RID: 1899
		private long _RewardData2;

		// Token: 0x0400076C RID: 1900
		public bool HasRewardTrigger;

		// Token: 0x0400076D RID: 1901
		private RewardTrigger _RewardTrigger;

		// Token: 0x0400076E RID: 1902
		public bool HasRewardTriggerQuota;

		// Token: 0x0400076F RID: 1903
		private int _RewardTriggerQuota;

		// Token: 0x04000770 RID: 1904
		public bool HasBrawlMode;

		// Token: 0x04000771 RID: 1905
		private TavernBrawlMode _BrawlMode;

		// Token: 0x04000772 RID: 1906
		public bool HasTicketType;

		// Token: 0x04000773 RID: 1907
		private int _TicketType;

		// Token: 0x04000774 RID: 1908
		public bool HasMaxWins;

		// Token: 0x04000775 RID: 1909
		private int _MaxWins;

		// Token: 0x04000776 RID: 1910
		public bool HasMaxLosses;

		// Token: 0x04000777 RID: 1911
		private int _MaxLosses;

		// Token: 0x04000778 RID: 1912
		public bool HasMaxSessions;

		// Token: 0x04000779 RID: 1913
		private int _MaxSessions;

		// Token: 0x0400077A RID: 1914
		public bool HasFreeSessions;

		// Token: 0x0400077B RID: 1915
		private uint _FreeSessions;

		// Token: 0x0400077C RID: 1916
		public bool HasClosedToNewSessionsSecondsFromNow;

		// Token: 0x0400077D RID: 1917
		private ulong _ClosedToNewSessionsSecondsFromNow;

		// Token: 0x0400077E RID: 1918
		public bool HasFriendlyChallengeDisabled;

		// Token: 0x0400077F RID: 1919
		private bool _FriendlyChallengeDisabled;

		// Token: 0x04000780 RID: 1920
		public bool HasFirstTimeSeenDialogId;

		// Token: 0x04000781 RID: 1921
		private int _FirstTimeSeenDialogId;

		// Token: 0x04000782 RID: 1922
		public bool HasIsPrerelease;

		// Token: 0x04000783 RID: 1923
		private bool _IsPrerelease;

		// Token: 0x04000784 RID: 1924
		private List<AssetRecordInfo> _AdditionalAssets = new List<AssetRecordInfo>();
	}
}
