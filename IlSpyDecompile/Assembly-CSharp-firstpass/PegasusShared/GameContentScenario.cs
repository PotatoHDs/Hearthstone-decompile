using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class GameContentScenario : IProtoBuf
	{
		public bool HasRewardType;

		private RewardType _RewardType;

		public bool HasRewardData1;

		private long _RewardData1;

		public bool HasRewardData2;

		private long _RewardData2;

		public bool HasRewardTrigger;

		private RewardTrigger _RewardTrigger;

		public bool HasRewardTriggerQuota;

		private int _RewardTriggerQuota;

		public bool HasBrawlMode;

		private TavernBrawlMode _BrawlMode;

		public bool HasTicketType;

		private int _TicketType;

		public bool HasMaxWins;

		private int _MaxWins;

		public bool HasMaxLosses;

		private int _MaxLosses;

		public bool HasMaxSessions;

		private int _MaxSessions;

		public bool HasFreeSessions;

		private uint _FreeSessions;

		public bool HasClosedToNewSessionsSecondsFromNow;

		private ulong _ClosedToNewSessionsSecondsFromNow;

		public bool HasFriendlyChallengeDisabled;

		private bool _FriendlyChallengeDisabled;

		public bool HasFirstTimeSeenDialogId;

		private int _FirstTimeSeenDialogId;

		public bool HasIsPrerelease;

		private bool _IsPrerelease;

		private List<AssetRecordInfo> _AdditionalAssets = new List<AssetRecordInfo>();

		public int LibraryItemId { get; set; }

		public bool IsRequired { get; set; }

		public bool IsFallback { get; set; }

		public int ScenarioId { get; set; }

		public uint ScenarioRecordByteSize { get; set; }

		public byte[] ScenarioRecordHash { get; set; }

		public FormatType FormatType { get; set; }

		public RewardType RewardType
		{
			get
			{
				return _RewardType;
			}
			set
			{
				_RewardType = value;
				HasRewardType = true;
			}
		}

		public long RewardData1
		{
			get
			{
				return _RewardData1;
			}
			set
			{
				_RewardData1 = value;
				HasRewardData1 = true;
			}
		}

		public long RewardData2
		{
			get
			{
				return _RewardData2;
			}
			set
			{
				_RewardData2 = value;
				HasRewardData2 = true;
			}
		}

		public RewardTrigger RewardTrigger
		{
			get
			{
				return _RewardTrigger;
			}
			set
			{
				_RewardTrigger = value;
				HasRewardTrigger = true;
			}
		}

		public int RewardTriggerQuota
		{
			get
			{
				return _RewardTriggerQuota;
			}
			set
			{
				_RewardTriggerQuota = value;
				HasRewardTriggerQuota = true;
			}
		}

		public TavernBrawlMode BrawlMode
		{
			get
			{
				return _BrawlMode;
			}
			set
			{
				_BrawlMode = value;
				HasBrawlMode = true;
			}
		}

		public int TicketType
		{
			get
			{
				return _TicketType;
			}
			set
			{
				_TicketType = value;
				HasTicketType = true;
			}
		}

		public int MaxWins
		{
			get
			{
				return _MaxWins;
			}
			set
			{
				_MaxWins = value;
				HasMaxWins = true;
			}
		}

		public int MaxLosses
		{
			get
			{
				return _MaxLosses;
			}
			set
			{
				_MaxLosses = value;
				HasMaxLosses = true;
			}
		}

		public int MaxSessions
		{
			get
			{
				return _MaxSessions;
			}
			set
			{
				_MaxSessions = value;
				HasMaxSessions = true;
			}
		}

		public uint FreeSessions
		{
			get
			{
				return _FreeSessions;
			}
			set
			{
				_FreeSessions = value;
				HasFreeSessions = true;
			}
		}

		public ulong ClosedToNewSessionsSecondsFromNow
		{
			get
			{
				return _ClosedToNewSessionsSecondsFromNow;
			}
			set
			{
				_ClosedToNewSessionsSecondsFromNow = value;
				HasClosedToNewSessionsSecondsFromNow = true;
			}
		}

		public bool FriendlyChallengeDisabled
		{
			get
			{
				return _FriendlyChallengeDisabled;
			}
			set
			{
				_FriendlyChallengeDisabled = value;
				HasFriendlyChallengeDisabled = true;
			}
		}

		public int FirstTimeSeenDialogId
		{
			get
			{
				return _FirstTimeSeenDialogId;
			}
			set
			{
				_FirstTimeSeenDialogId = value;
				HasFirstTimeSeenDialogId = true;
			}
		}

		public bool IsPrerelease
		{
			get
			{
				return _IsPrerelease;
			}
			set
			{
				_IsPrerelease = value;
				HasIsPrerelease = true;
			}
		}

		public List<AssetRecordInfo> AdditionalAssets
		{
			get
			{
				return _AdditionalAssets;
			}
			set
			{
				_AdditionalAssets = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= LibraryItemId.GetHashCode();
			hashCode ^= IsRequired.GetHashCode();
			hashCode ^= IsFallback.GetHashCode();
			hashCode ^= ScenarioId.GetHashCode();
			hashCode ^= ScenarioRecordByteSize.GetHashCode();
			hashCode ^= ScenarioRecordHash.GetHashCode();
			hashCode ^= FormatType.GetHashCode();
			if (HasRewardType)
			{
				hashCode ^= RewardType.GetHashCode();
			}
			if (HasRewardData1)
			{
				hashCode ^= RewardData1.GetHashCode();
			}
			if (HasRewardData2)
			{
				hashCode ^= RewardData2.GetHashCode();
			}
			if (HasRewardTrigger)
			{
				hashCode ^= RewardTrigger.GetHashCode();
			}
			if (HasRewardTriggerQuota)
			{
				hashCode ^= RewardTriggerQuota.GetHashCode();
			}
			if (HasBrawlMode)
			{
				hashCode ^= BrawlMode.GetHashCode();
			}
			if (HasTicketType)
			{
				hashCode ^= TicketType.GetHashCode();
			}
			if (HasMaxWins)
			{
				hashCode ^= MaxWins.GetHashCode();
			}
			if (HasMaxLosses)
			{
				hashCode ^= MaxLosses.GetHashCode();
			}
			if (HasMaxSessions)
			{
				hashCode ^= MaxSessions.GetHashCode();
			}
			if (HasFreeSessions)
			{
				hashCode ^= FreeSessions.GetHashCode();
			}
			if (HasClosedToNewSessionsSecondsFromNow)
			{
				hashCode ^= ClosedToNewSessionsSecondsFromNow.GetHashCode();
			}
			if (HasFriendlyChallengeDisabled)
			{
				hashCode ^= FriendlyChallengeDisabled.GetHashCode();
			}
			if (HasFirstTimeSeenDialogId)
			{
				hashCode ^= FirstTimeSeenDialogId.GetHashCode();
			}
			if (HasIsPrerelease)
			{
				hashCode ^= IsPrerelease.GetHashCode();
			}
			foreach (AssetRecordInfo additionalAsset in AdditionalAssets)
			{
				hashCode ^= additionalAsset.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GameContentScenario gameContentScenario = obj as GameContentScenario;
			if (gameContentScenario == null)
			{
				return false;
			}
			if (!LibraryItemId.Equals(gameContentScenario.LibraryItemId))
			{
				return false;
			}
			if (!IsRequired.Equals(gameContentScenario.IsRequired))
			{
				return false;
			}
			if (!IsFallback.Equals(gameContentScenario.IsFallback))
			{
				return false;
			}
			if (!ScenarioId.Equals(gameContentScenario.ScenarioId))
			{
				return false;
			}
			if (!ScenarioRecordByteSize.Equals(gameContentScenario.ScenarioRecordByteSize))
			{
				return false;
			}
			if (!ScenarioRecordHash.Equals(gameContentScenario.ScenarioRecordHash))
			{
				return false;
			}
			if (!FormatType.Equals(gameContentScenario.FormatType))
			{
				return false;
			}
			if (HasRewardType != gameContentScenario.HasRewardType || (HasRewardType && !RewardType.Equals(gameContentScenario.RewardType)))
			{
				return false;
			}
			if (HasRewardData1 != gameContentScenario.HasRewardData1 || (HasRewardData1 && !RewardData1.Equals(gameContentScenario.RewardData1)))
			{
				return false;
			}
			if (HasRewardData2 != gameContentScenario.HasRewardData2 || (HasRewardData2 && !RewardData2.Equals(gameContentScenario.RewardData2)))
			{
				return false;
			}
			if (HasRewardTrigger != gameContentScenario.HasRewardTrigger || (HasRewardTrigger && !RewardTrigger.Equals(gameContentScenario.RewardTrigger)))
			{
				return false;
			}
			if (HasRewardTriggerQuota != gameContentScenario.HasRewardTriggerQuota || (HasRewardTriggerQuota && !RewardTriggerQuota.Equals(gameContentScenario.RewardTriggerQuota)))
			{
				return false;
			}
			if (HasBrawlMode != gameContentScenario.HasBrawlMode || (HasBrawlMode && !BrawlMode.Equals(gameContentScenario.BrawlMode)))
			{
				return false;
			}
			if (HasTicketType != gameContentScenario.HasTicketType || (HasTicketType && !TicketType.Equals(gameContentScenario.TicketType)))
			{
				return false;
			}
			if (HasMaxWins != gameContentScenario.HasMaxWins || (HasMaxWins && !MaxWins.Equals(gameContentScenario.MaxWins)))
			{
				return false;
			}
			if (HasMaxLosses != gameContentScenario.HasMaxLosses || (HasMaxLosses && !MaxLosses.Equals(gameContentScenario.MaxLosses)))
			{
				return false;
			}
			if (HasMaxSessions != gameContentScenario.HasMaxSessions || (HasMaxSessions && !MaxSessions.Equals(gameContentScenario.MaxSessions)))
			{
				return false;
			}
			if (HasFreeSessions != gameContentScenario.HasFreeSessions || (HasFreeSessions && !FreeSessions.Equals(gameContentScenario.FreeSessions)))
			{
				return false;
			}
			if (HasClosedToNewSessionsSecondsFromNow != gameContentScenario.HasClosedToNewSessionsSecondsFromNow || (HasClosedToNewSessionsSecondsFromNow && !ClosedToNewSessionsSecondsFromNow.Equals(gameContentScenario.ClosedToNewSessionsSecondsFromNow)))
			{
				return false;
			}
			if (HasFriendlyChallengeDisabled != gameContentScenario.HasFriendlyChallengeDisabled || (HasFriendlyChallengeDisabled && !FriendlyChallengeDisabled.Equals(gameContentScenario.FriendlyChallengeDisabled)))
			{
				return false;
			}
			if (HasFirstTimeSeenDialogId != gameContentScenario.HasFirstTimeSeenDialogId || (HasFirstTimeSeenDialogId && !FirstTimeSeenDialogId.Equals(gameContentScenario.FirstTimeSeenDialogId)))
			{
				return false;
			}
			if (HasIsPrerelease != gameContentScenario.HasIsPrerelease || (HasIsPrerelease && !IsPrerelease.Equals(gameContentScenario.IsPrerelease)))
			{
				return false;
			}
			if (AdditionalAssets.Count != gameContentScenario.AdditionalAssets.Count)
			{
				return false;
			}
			for (int i = 0; i < AdditionalAssets.Count; i++)
			{
				if (!AdditionalAssets[i].Equals(gameContentScenario.AdditionalAssets[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameContentScenario Deserialize(Stream stream, GameContentScenario instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameContentScenario DeserializeLengthDelimited(Stream stream)
		{
			GameContentScenario gameContentScenario = new GameContentScenario();
			DeserializeLengthDelimited(stream, gameContentScenario);
			return gameContentScenario;
		}

		public static GameContentScenario DeserializeLengthDelimited(Stream stream, GameContentScenario instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameContentScenario Deserialize(Stream stream, GameContentScenario instance, long limit)
		{
			instance.RewardType = RewardType.REWARD_UNKNOWN;
			instance.RewardTrigger = RewardTrigger.REWARD_TRIGGER_UNKNOWN;
			instance.BrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
			if (instance.AdditionalAssets == null)
			{
				instance.AdditionalAssets = new List<AssetRecordInfo>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.LibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.IsRequired = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.IsFallback = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.ScenarioRecordByteSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 50:
					instance.ScenarioRecordHash = ProtocolParser.ReadBytes(stream);
					continue;
				case 56:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.RewardType = (RewardType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.RewardData1 = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.RewardData2 = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.RewardTrigger = (RewardTrigger)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.RewardTriggerQuota = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.BrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
					continue;
				case 112:
					instance.TicketType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 120:
					instance.MaxWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxLosses = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.MaxSessions = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.FreeSessions = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.ClosedToNewSessionsSecondsFromNow = ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.FriendlyChallengeDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 21u:
						if (key.WireType == Wire.Varint)
						{
							instance.FirstTimeSeenDialogId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 22u:
						if (key.WireType == Wire.Varint)
						{
							instance.IsPrerelease = ProtocolParser.ReadBool(stream);
						}
						break;
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.AdditionalAssets.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameContentScenario instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.LibraryItemId);
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.IsRequired);
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.IsFallback);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt32(stream, instance.ScenarioRecordByteSize);
			if (instance.ScenarioRecordHash == null)
			{
				throw new ArgumentNullException("ScenarioRecordHash", "Required by proto specification.");
			}
			stream.WriteByte(50);
			ProtocolParser.WriteBytes(stream, instance.ScenarioRecordHash);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			if (instance.HasRewardType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardType);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardTrigger);
			}
			if (instance.HasRewardTriggerQuota)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardTriggerQuota);
			}
			if (instance.HasBrawlMode)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlMode);
			}
			if (instance.HasTicketType)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TicketType);
			}
			if (instance.HasMaxWins)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxWins);
			}
			if (instance.HasMaxLosses)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxLosses);
			}
			if (instance.HasMaxSessions)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxSessions);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FirstTimeSeenDialogId);
			}
			if (instance.HasIsPrerelease)
			{
				stream.WriteByte(176);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.IsPrerelease);
			}
			if (instance.AdditionalAssets.Count <= 0)
			{
				return;
			}
			foreach (AssetRecordInfo additionalAsset in instance.AdditionalAssets)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, additionalAsset.GetSerializedSize());
				AssetRecordInfo.Serialize(stream, additionalAsset);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)LibraryItemId);
			num++;
			num++;
			num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			num += ProtocolParser.SizeOfUInt32(ScenarioRecordByteSize);
			num += (uint)((int)ProtocolParser.SizeOfUInt32(ScenarioRecordHash.Length) + ScenarioRecordHash.Length);
			num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			if (HasRewardType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardType);
			}
			if (HasRewardData1)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardData1);
			}
			if (HasRewardData2)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardData2);
			}
			if (HasRewardTrigger)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardTrigger);
			}
			if (HasRewardTriggerQuota)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardTriggerQuota);
			}
			if (HasBrawlMode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlMode);
			}
			if (HasTicketType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TicketType);
			}
			if (HasMaxWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxWins);
			}
			if (HasMaxLosses)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxLosses);
			}
			if (HasMaxSessions)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxSessions);
			}
			if (HasFreeSessions)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(FreeSessions);
			}
			if (HasClosedToNewSessionsSecondsFromNow)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64(ClosedToNewSessionsSecondsFromNow);
			}
			if (HasFriendlyChallengeDisabled)
			{
				num += 2;
				num++;
			}
			if (HasFirstTimeSeenDialogId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FirstTimeSeenDialogId);
			}
			if (HasIsPrerelease)
			{
				num += 2;
				num++;
			}
			if (AdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo additionalAsset in AdditionalAssets)
				{
					num += 2;
					uint serializedSize = additionalAsset.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 7;
		}
	}
}
