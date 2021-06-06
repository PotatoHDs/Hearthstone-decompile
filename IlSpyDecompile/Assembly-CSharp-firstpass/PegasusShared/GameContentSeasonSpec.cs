using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class GameContentSeasonSpec : IProtoBuf
	{
		public bool HasEndSecondsFromNow;

		private ulong _EndSecondsFromNow;

		public bool HasSeasonEndSecondSpreadCount;

		private int _SeasonEndSecondSpreadCount;

		private List<GameContentScenario> _Scenarios = new List<GameContentScenario>();

		public bool HasDeprecatedScenarioId;

		private int _DeprecatedScenarioId;

		public bool HasDeprecatedScenarioRecordByteSize;

		private uint _DeprecatedScenarioRecordByteSize;

		public bool HasDeprecatedScenarioRecordHash;

		private byte[] _DeprecatedScenarioRecordHash;

		public bool HasDeprecatedRewardType;

		private RewardType _DeprecatedRewardType;

		public bool HasDeprecatedRewardData1;

		private long _DeprecatedRewardData1;

		public bool HasDeprecatedRewardData2;

		private long _DeprecatedRewardData2;

		public bool HasDeprecatedRewardTrigger;

		private RewardTrigger _DeprecatedRewardTrigger;

		public bool HasDeprecatedFormatType;

		private FormatType _DeprecatedFormatType;

		public bool HasDeprecatedTicketType;

		private int _DeprecatedTicketType;

		public bool HasDeprecatedMaxWins;

		private int _DeprecatedMaxWins;

		public bool HasDeprecatedMaxLosses;

		private int _DeprecatedMaxLosses;

		public bool HasDeprecatedClosedToNewSessionsSecondsFromNow;

		private ulong _DeprecatedClosedToNewSessionsSecondsFromNow;

		public bool HasDeprecatedMaxSessions;

		private int _DeprecatedMaxSessions;

		public bool HasDeprecatedFriendlyChallengeDisabled;

		private bool _DeprecatedFriendlyChallengeDisabled;

		public bool HasDeprecatedFirstTimeSeenDialogId;

		private int _DeprecatedFirstTimeSeenDialogId;

		public bool HasDeprecatedRewardTriggerQuota;

		private int _DeprecatedRewardTriggerQuota;

		public bool HasDeprecatedFreeSessions;

		private uint _DeprecatedFreeSessions;

		public bool HasDeprecatedIsPrerelease;

		private bool _DeprecatedIsPrerelease;

		private List<AssetRecordInfo> _DeprecatedAdditionalAssets = new List<AssetRecordInfo>();

		public ulong EndSecondsFromNow
		{
			get
			{
				return _EndSecondsFromNow;
			}
			set
			{
				_EndSecondsFromNow = value;
				HasEndSecondsFromNow = true;
			}
		}

		public int SeasonId { get; set; }

		public int SeasonEndSecondSpreadCount
		{
			get
			{
				return _SeasonEndSecondSpreadCount;
			}
			set
			{
				_SeasonEndSecondSpreadCount = value;
				HasSeasonEndSecondSpreadCount = true;
			}
		}

		public List<GameContentScenario> Scenarios
		{
			get
			{
				return _Scenarios;
			}
			set
			{
				_Scenarios = value;
			}
		}

		public int DeprecatedScenarioId
		{
			get
			{
				return _DeprecatedScenarioId;
			}
			set
			{
				_DeprecatedScenarioId = value;
				HasDeprecatedScenarioId = true;
			}
		}

		public uint DeprecatedScenarioRecordByteSize
		{
			get
			{
				return _DeprecatedScenarioRecordByteSize;
			}
			set
			{
				_DeprecatedScenarioRecordByteSize = value;
				HasDeprecatedScenarioRecordByteSize = true;
			}
		}

		public byte[] DeprecatedScenarioRecordHash
		{
			get
			{
				return _DeprecatedScenarioRecordHash;
			}
			set
			{
				_DeprecatedScenarioRecordHash = value;
				HasDeprecatedScenarioRecordHash = value != null;
			}
		}

		public RewardType DeprecatedRewardType
		{
			get
			{
				return _DeprecatedRewardType;
			}
			set
			{
				_DeprecatedRewardType = value;
				HasDeprecatedRewardType = true;
			}
		}

		public long DeprecatedRewardData1
		{
			get
			{
				return _DeprecatedRewardData1;
			}
			set
			{
				_DeprecatedRewardData1 = value;
				HasDeprecatedRewardData1 = true;
			}
		}

		public long DeprecatedRewardData2
		{
			get
			{
				return _DeprecatedRewardData2;
			}
			set
			{
				_DeprecatedRewardData2 = value;
				HasDeprecatedRewardData2 = true;
			}
		}

		public RewardTrigger DeprecatedRewardTrigger
		{
			get
			{
				return _DeprecatedRewardTrigger;
			}
			set
			{
				_DeprecatedRewardTrigger = value;
				HasDeprecatedRewardTrigger = true;
			}
		}

		public FormatType DeprecatedFormatType
		{
			get
			{
				return _DeprecatedFormatType;
			}
			set
			{
				_DeprecatedFormatType = value;
				HasDeprecatedFormatType = true;
			}
		}

		public int DeprecatedTicketType
		{
			get
			{
				return _DeprecatedTicketType;
			}
			set
			{
				_DeprecatedTicketType = value;
				HasDeprecatedTicketType = true;
			}
		}

		public int DeprecatedMaxWins
		{
			get
			{
				return _DeprecatedMaxWins;
			}
			set
			{
				_DeprecatedMaxWins = value;
				HasDeprecatedMaxWins = true;
			}
		}

		public int DeprecatedMaxLosses
		{
			get
			{
				return _DeprecatedMaxLosses;
			}
			set
			{
				_DeprecatedMaxLosses = value;
				HasDeprecatedMaxLosses = true;
			}
		}

		public ulong DeprecatedClosedToNewSessionsSecondsFromNow
		{
			get
			{
				return _DeprecatedClosedToNewSessionsSecondsFromNow;
			}
			set
			{
				_DeprecatedClosedToNewSessionsSecondsFromNow = value;
				HasDeprecatedClosedToNewSessionsSecondsFromNow = true;
			}
		}

		public int DeprecatedMaxSessions
		{
			get
			{
				return _DeprecatedMaxSessions;
			}
			set
			{
				_DeprecatedMaxSessions = value;
				HasDeprecatedMaxSessions = true;
			}
		}

		public bool DeprecatedFriendlyChallengeDisabled
		{
			get
			{
				return _DeprecatedFriendlyChallengeDisabled;
			}
			set
			{
				_DeprecatedFriendlyChallengeDisabled = value;
				HasDeprecatedFriendlyChallengeDisabled = true;
			}
		}

		public int DeprecatedFirstTimeSeenDialogId
		{
			get
			{
				return _DeprecatedFirstTimeSeenDialogId;
			}
			set
			{
				_DeprecatedFirstTimeSeenDialogId = value;
				HasDeprecatedFirstTimeSeenDialogId = true;
			}
		}

		public int DeprecatedRewardTriggerQuota
		{
			get
			{
				return _DeprecatedRewardTriggerQuota;
			}
			set
			{
				_DeprecatedRewardTriggerQuota = value;
				HasDeprecatedRewardTriggerQuota = true;
			}
		}

		public uint DeprecatedFreeSessions
		{
			get
			{
				return _DeprecatedFreeSessions;
			}
			set
			{
				_DeprecatedFreeSessions = value;
				HasDeprecatedFreeSessions = true;
			}
		}

		public bool DeprecatedIsPrerelease
		{
			get
			{
				return _DeprecatedIsPrerelease;
			}
			set
			{
				_DeprecatedIsPrerelease = value;
				HasDeprecatedIsPrerelease = true;
			}
		}

		public List<AssetRecordInfo> DeprecatedAdditionalAssets
		{
			get
			{
				return _DeprecatedAdditionalAssets;
			}
			set
			{
				_DeprecatedAdditionalAssets = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasEndSecondsFromNow)
			{
				num ^= EndSecondsFromNow.GetHashCode();
			}
			num ^= SeasonId.GetHashCode();
			if (HasSeasonEndSecondSpreadCount)
			{
				num ^= SeasonEndSecondSpreadCount.GetHashCode();
			}
			foreach (GameContentScenario scenario in Scenarios)
			{
				num ^= scenario.GetHashCode();
			}
			if (HasDeprecatedScenarioId)
			{
				num ^= DeprecatedScenarioId.GetHashCode();
			}
			if (HasDeprecatedScenarioRecordByteSize)
			{
				num ^= DeprecatedScenarioRecordByteSize.GetHashCode();
			}
			if (HasDeprecatedScenarioRecordHash)
			{
				num ^= DeprecatedScenarioRecordHash.GetHashCode();
			}
			if (HasDeprecatedRewardType)
			{
				num ^= DeprecatedRewardType.GetHashCode();
			}
			if (HasDeprecatedRewardData1)
			{
				num ^= DeprecatedRewardData1.GetHashCode();
			}
			if (HasDeprecatedRewardData2)
			{
				num ^= DeprecatedRewardData2.GetHashCode();
			}
			if (HasDeprecatedRewardTrigger)
			{
				num ^= DeprecatedRewardTrigger.GetHashCode();
			}
			if (HasDeprecatedFormatType)
			{
				num ^= DeprecatedFormatType.GetHashCode();
			}
			if (HasDeprecatedTicketType)
			{
				num ^= DeprecatedTicketType.GetHashCode();
			}
			if (HasDeprecatedMaxWins)
			{
				num ^= DeprecatedMaxWins.GetHashCode();
			}
			if (HasDeprecatedMaxLosses)
			{
				num ^= DeprecatedMaxLosses.GetHashCode();
			}
			if (HasDeprecatedClosedToNewSessionsSecondsFromNow)
			{
				num ^= DeprecatedClosedToNewSessionsSecondsFromNow.GetHashCode();
			}
			if (HasDeprecatedMaxSessions)
			{
				num ^= DeprecatedMaxSessions.GetHashCode();
			}
			if (HasDeprecatedFriendlyChallengeDisabled)
			{
				num ^= DeprecatedFriendlyChallengeDisabled.GetHashCode();
			}
			if (HasDeprecatedFirstTimeSeenDialogId)
			{
				num ^= DeprecatedFirstTimeSeenDialogId.GetHashCode();
			}
			if (HasDeprecatedRewardTriggerQuota)
			{
				num ^= DeprecatedRewardTriggerQuota.GetHashCode();
			}
			if (HasDeprecatedFreeSessions)
			{
				num ^= DeprecatedFreeSessions.GetHashCode();
			}
			if (HasDeprecatedIsPrerelease)
			{
				num ^= DeprecatedIsPrerelease.GetHashCode();
			}
			foreach (AssetRecordInfo deprecatedAdditionalAsset in DeprecatedAdditionalAssets)
			{
				num ^= deprecatedAdditionalAsset.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameContentSeasonSpec gameContentSeasonSpec = obj as GameContentSeasonSpec;
			if (gameContentSeasonSpec == null)
			{
				return false;
			}
			if (HasEndSecondsFromNow != gameContentSeasonSpec.HasEndSecondsFromNow || (HasEndSecondsFromNow && !EndSecondsFromNow.Equals(gameContentSeasonSpec.EndSecondsFromNow)))
			{
				return false;
			}
			if (!SeasonId.Equals(gameContentSeasonSpec.SeasonId))
			{
				return false;
			}
			if (HasSeasonEndSecondSpreadCount != gameContentSeasonSpec.HasSeasonEndSecondSpreadCount || (HasSeasonEndSecondSpreadCount && !SeasonEndSecondSpreadCount.Equals(gameContentSeasonSpec.SeasonEndSecondSpreadCount)))
			{
				return false;
			}
			if (Scenarios.Count != gameContentSeasonSpec.Scenarios.Count)
			{
				return false;
			}
			for (int i = 0; i < Scenarios.Count; i++)
			{
				if (!Scenarios[i].Equals(gameContentSeasonSpec.Scenarios[i]))
				{
					return false;
				}
			}
			if (HasDeprecatedScenarioId != gameContentSeasonSpec.HasDeprecatedScenarioId || (HasDeprecatedScenarioId && !DeprecatedScenarioId.Equals(gameContentSeasonSpec.DeprecatedScenarioId)))
			{
				return false;
			}
			if (HasDeprecatedScenarioRecordByteSize != gameContentSeasonSpec.HasDeprecatedScenarioRecordByteSize || (HasDeprecatedScenarioRecordByteSize && !DeprecatedScenarioRecordByteSize.Equals(gameContentSeasonSpec.DeprecatedScenarioRecordByteSize)))
			{
				return false;
			}
			if (HasDeprecatedScenarioRecordHash != gameContentSeasonSpec.HasDeprecatedScenarioRecordHash || (HasDeprecatedScenarioRecordHash && !DeprecatedScenarioRecordHash.Equals(gameContentSeasonSpec.DeprecatedScenarioRecordHash)))
			{
				return false;
			}
			if (HasDeprecatedRewardType != gameContentSeasonSpec.HasDeprecatedRewardType || (HasDeprecatedRewardType && !DeprecatedRewardType.Equals(gameContentSeasonSpec.DeprecatedRewardType)))
			{
				return false;
			}
			if (HasDeprecatedRewardData1 != gameContentSeasonSpec.HasDeprecatedRewardData1 || (HasDeprecatedRewardData1 && !DeprecatedRewardData1.Equals(gameContentSeasonSpec.DeprecatedRewardData1)))
			{
				return false;
			}
			if (HasDeprecatedRewardData2 != gameContentSeasonSpec.HasDeprecatedRewardData2 || (HasDeprecatedRewardData2 && !DeprecatedRewardData2.Equals(gameContentSeasonSpec.DeprecatedRewardData2)))
			{
				return false;
			}
			if (HasDeprecatedRewardTrigger != gameContentSeasonSpec.HasDeprecatedRewardTrigger || (HasDeprecatedRewardTrigger && !DeprecatedRewardTrigger.Equals(gameContentSeasonSpec.DeprecatedRewardTrigger)))
			{
				return false;
			}
			if (HasDeprecatedFormatType != gameContentSeasonSpec.HasDeprecatedFormatType || (HasDeprecatedFormatType && !DeprecatedFormatType.Equals(gameContentSeasonSpec.DeprecatedFormatType)))
			{
				return false;
			}
			if (HasDeprecatedTicketType != gameContentSeasonSpec.HasDeprecatedTicketType || (HasDeprecatedTicketType && !DeprecatedTicketType.Equals(gameContentSeasonSpec.DeprecatedTicketType)))
			{
				return false;
			}
			if (HasDeprecatedMaxWins != gameContentSeasonSpec.HasDeprecatedMaxWins || (HasDeprecatedMaxWins && !DeprecatedMaxWins.Equals(gameContentSeasonSpec.DeprecatedMaxWins)))
			{
				return false;
			}
			if (HasDeprecatedMaxLosses != gameContentSeasonSpec.HasDeprecatedMaxLosses || (HasDeprecatedMaxLosses && !DeprecatedMaxLosses.Equals(gameContentSeasonSpec.DeprecatedMaxLosses)))
			{
				return false;
			}
			if (HasDeprecatedClosedToNewSessionsSecondsFromNow != gameContentSeasonSpec.HasDeprecatedClosedToNewSessionsSecondsFromNow || (HasDeprecatedClosedToNewSessionsSecondsFromNow && !DeprecatedClosedToNewSessionsSecondsFromNow.Equals(gameContentSeasonSpec.DeprecatedClosedToNewSessionsSecondsFromNow)))
			{
				return false;
			}
			if (HasDeprecatedMaxSessions != gameContentSeasonSpec.HasDeprecatedMaxSessions || (HasDeprecatedMaxSessions && !DeprecatedMaxSessions.Equals(gameContentSeasonSpec.DeprecatedMaxSessions)))
			{
				return false;
			}
			if (HasDeprecatedFriendlyChallengeDisabled != gameContentSeasonSpec.HasDeprecatedFriendlyChallengeDisabled || (HasDeprecatedFriendlyChallengeDisabled && !DeprecatedFriendlyChallengeDisabled.Equals(gameContentSeasonSpec.DeprecatedFriendlyChallengeDisabled)))
			{
				return false;
			}
			if (HasDeprecatedFirstTimeSeenDialogId != gameContentSeasonSpec.HasDeprecatedFirstTimeSeenDialogId || (HasDeprecatedFirstTimeSeenDialogId && !DeprecatedFirstTimeSeenDialogId.Equals(gameContentSeasonSpec.DeprecatedFirstTimeSeenDialogId)))
			{
				return false;
			}
			if (HasDeprecatedRewardTriggerQuota != gameContentSeasonSpec.HasDeprecatedRewardTriggerQuota || (HasDeprecatedRewardTriggerQuota && !DeprecatedRewardTriggerQuota.Equals(gameContentSeasonSpec.DeprecatedRewardTriggerQuota)))
			{
				return false;
			}
			if (HasDeprecatedFreeSessions != gameContentSeasonSpec.HasDeprecatedFreeSessions || (HasDeprecatedFreeSessions && !DeprecatedFreeSessions.Equals(gameContentSeasonSpec.DeprecatedFreeSessions)))
			{
				return false;
			}
			if (HasDeprecatedIsPrerelease != gameContentSeasonSpec.HasDeprecatedIsPrerelease || (HasDeprecatedIsPrerelease && !DeprecatedIsPrerelease.Equals(gameContentSeasonSpec.DeprecatedIsPrerelease)))
			{
				return false;
			}
			if (DeprecatedAdditionalAssets.Count != gameContentSeasonSpec.DeprecatedAdditionalAssets.Count)
			{
				return false;
			}
			for (int j = 0; j < DeprecatedAdditionalAssets.Count; j++)
			{
				if (!DeprecatedAdditionalAssets[j].Equals(gameContentSeasonSpec.DeprecatedAdditionalAssets[j]))
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

		public static GameContentSeasonSpec Deserialize(Stream stream, GameContentSeasonSpec instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameContentSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			GameContentSeasonSpec gameContentSeasonSpec = new GameContentSeasonSpec();
			DeserializeLengthDelimited(stream, gameContentSeasonSpec);
			return gameContentSeasonSpec;
		}

		public static GameContentSeasonSpec DeserializeLengthDelimited(Stream stream, GameContentSeasonSpec instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

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
					instance.EndSecondsFromNow = ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.DeprecatedScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.DeprecatedScenarioRecordByteSize = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.DeprecatedScenarioRecordHash = ProtocolParser.ReadBytes(stream);
					continue;
				case 40:
					instance.DeprecatedRewardType = (RewardType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.DeprecatedRewardData1 = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.DeprecatedRewardData2 = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.DeprecatedRewardTrigger = (RewardTrigger)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.DeprecatedFormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.DeprecatedTicketType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.DeprecatedMaxWins = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 112:
					instance.DeprecatedMaxLosses = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 120:
					instance.DeprecatedClosedToNewSessionsSecondsFromNow = ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.SeasonEndSecondSpreadCount = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 23u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Scenarios.Add(GameContentScenario.DeserializeLengthDelimited(stream));
						}
						break;
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedMaxSessions = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFriendlyChallengeDisabled = ProtocolParser.ReadBool(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFirstTimeSeenDialogId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedRewardTriggerQuota = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 21u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedFreeSessions = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 22u:
						if (key.WireType == Wire.Varint)
						{
							instance.DeprecatedIsPrerelease = ProtocolParser.ReadBool(stream);
						}
						break;
					case 100u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeprecatedAdditionalAssets.Add(AssetRecordInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GameContentSeasonSpec instance)
		{
			if (instance.HasEndSecondsFromNow)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.EndSecondsFromNow);
			}
			stream.WriteByte(88);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			if (instance.HasSeasonEndSecondSpreadCount)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonEndSecondSpreadCount);
			}
			if (instance.Scenarios.Count > 0)
			{
				foreach (GameContentScenario scenario in instance.Scenarios)
				{
					stream.WriteByte(186);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, scenario.GetSerializedSize());
					GameContentScenario.Serialize(stream, scenario);
				}
			}
			if (instance.HasDeprecatedScenarioId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedScenarioId);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedRewardType);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedRewardTrigger);
			}
			if (instance.HasDeprecatedFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedFormatType);
			}
			if (instance.HasDeprecatedTicketType)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedTicketType);
			}
			if (instance.HasDeprecatedMaxWins)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedMaxWins);
			}
			if (instance.HasDeprecatedMaxLosses)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedMaxLosses);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedMaxSessions);
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
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedFirstTimeSeenDialogId);
			}
			if (instance.HasDeprecatedRewardTriggerQuota)
			{
				stream.WriteByte(160);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedRewardTriggerQuota);
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
			if (instance.DeprecatedAdditionalAssets.Count <= 0)
			{
				return;
			}
			foreach (AssetRecordInfo deprecatedAdditionalAsset in instance.DeprecatedAdditionalAssets)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, deprecatedAdditionalAsset.GetSerializedSize());
				AssetRecordInfo.Serialize(stream, deprecatedAdditionalAsset);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasEndSecondsFromNow)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(EndSecondsFromNow);
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			if (HasSeasonEndSecondSpreadCount)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonEndSecondSpreadCount);
			}
			if (Scenarios.Count > 0)
			{
				foreach (GameContentScenario scenario in Scenarios)
				{
					num += 2;
					uint serializedSize = scenario.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasDeprecatedScenarioId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedScenarioId);
			}
			if (HasDeprecatedScenarioRecordByteSize)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(DeprecatedScenarioRecordByteSize);
			}
			if (HasDeprecatedScenarioRecordHash)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(DeprecatedScenarioRecordHash.Length) + DeprecatedScenarioRecordHash.Length);
			}
			if (HasDeprecatedRewardType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedRewardType);
			}
			if (HasDeprecatedRewardData1)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedRewardData1);
			}
			if (HasDeprecatedRewardData2)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedRewardData2);
			}
			if (HasDeprecatedRewardTrigger)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedRewardTrigger);
			}
			if (HasDeprecatedFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedFormatType);
			}
			if (HasDeprecatedTicketType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedTicketType);
			}
			if (HasDeprecatedMaxWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedMaxWins);
			}
			if (HasDeprecatedMaxLosses)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedMaxLosses);
			}
			if (HasDeprecatedClosedToNewSessionsSecondsFromNow)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(DeprecatedClosedToNewSessionsSecondsFromNow);
			}
			if (HasDeprecatedMaxSessions)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedMaxSessions);
			}
			if (HasDeprecatedFriendlyChallengeDisabled)
			{
				num += 2;
				num++;
			}
			if (HasDeprecatedFirstTimeSeenDialogId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedFirstTimeSeenDialogId);
			}
			if (HasDeprecatedRewardTriggerQuota)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedRewardTriggerQuota);
			}
			if (HasDeprecatedFreeSessions)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(DeprecatedFreeSessions);
			}
			if (HasDeprecatedIsPrerelease)
			{
				num += 2;
				num++;
			}
			if (DeprecatedAdditionalAssets.Count > 0)
			{
				foreach (AssetRecordInfo deprecatedAdditionalAsset in DeprecatedAdditionalAssets)
				{
					num += 2;
					uint serializedSize2 = deprecatedAdditionalAsset.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 1;
		}
	}
}
