using System.IO;

namespace PegasusShared
{
	public class ProfileNoticeMedal : IProtoBuf
	{
		public enum NoticeID
		{
			ID = 1
		}

		public enum MedalType
		{
			UNKNOWN_MEDAL,
			STANDARD_MEDAL,
			WILD_MEDAL,
			CLASSIC_MEDAL
		}

		public bool HasLegendRank;

		private int _LegendRank;

		public bool HasBestStarLevel;

		private int _BestStarLevel;

		public bool HasChest;

		private RewardChest _Chest;

		public bool HasMedalType_;

		private MedalType _MedalType_;

		public bool HasLeagueId;

		private int _LeagueId;

		public bool HasWasLimitedByBestEverStarLevel;

		private bool _WasLimitedByBestEverStarLevel;

		public int StarLevel { get; set; }

		public int LegendRank
		{
			get
			{
				return _LegendRank;
			}
			set
			{
				_LegendRank = value;
				HasLegendRank = true;
			}
		}

		public int BestStarLevel
		{
			get
			{
				return _BestStarLevel;
			}
			set
			{
				_BestStarLevel = value;
				HasBestStarLevel = true;
			}
		}

		public RewardChest Chest
		{
			get
			{
				return _Chest;
			}
			set
			{
				_Chest = value;
				HasChest = value != null;
			}
		}

		public MedalType MedalType_
		{
			get
			{
				return _MedalType_;
			}
			set
			{
				_MedalType_ = value;
				HasMedalType_ = true;
			}
		}

		public int LeagueId
		{
			get
			{
				return _LeagueId;
			}
			set
			{
				_LeagueId = value;
				HasLeagueId = true;
			}
		}

		public bool WasLimitedByBestEverStarLevel
		{
			get
			{
				return _WasLimitedByBestEverStarLevel;
			}
			set
			{
				_WasLimitedByBestEverStarLevel = value;
				HasWasLimitedByBestEverStarLevel = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= StarLevel.GetHashCode();
			if (HasLegendRank)
			{
				hashCode ^= LegendRank.GetHashCode();
			}
			if (HasBestStarLevel)
			{
				hashCode ^= BestStarLevel.GetHashCode();
			}
			if (HasChest)
			{
				hashCode ^= Chest.GetHashCode();
			}
			if (HasMedalType_)
			{
				hashCode ^= MedalType_.GetHashCode();
			}
			if (HasLeagueId)
			{
				hashCode ^= LeagueId.GetHashCode();
			}
			if (HasWasLimitedByBestEverStarLevel)
			{
				hashCode ^= WasLimitedByBestEverStarLevel.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ProfileNoticeMedal profileNoticeMedal = obj as ProfileNoticeMedal;
			if (profileNoticeMedal == null)
			{
				return false;
			}
			if (!StarLevel.Equals(profileNoticeMedal.StarLevel))
			{
				return false;
			}
			if (HasLegendRank != profileNoticeMedal.HasLegendRank || (HasLegendRank && !LegendRank.Equals(profileNoticeMedal.LegendRank)))
			{
				return false;
			}
			if (HasBestStarLevel != profileNoticeMedal.HasBestStarLevel || (HasBestStarLevel && !BestStarLevel.Equals(profileNoticeMedal.BestStarLevel)))
			{
				return false;
			}
			if (HasChest != profileNoticeMedal.HasChest || (HasChest && !Chest.Equals(profileNoticeMedal.Chest)))
			{
				return false;
			}
			if (HasMedalType_ != profileNoticeMedal.HasMedalType_ || (HasMedalType_ && !MedalType_.Equals(profileNoticeMedal.MedalType_)))
			{
				return false;
			}
			if (HasLeagueId != profileNoticeMedal.HasLeagueId || (HasLeagueId && !LeagueId.Equals(profileNoticeMedal.LeagueId)))
			{
				return false;
			}
			if (HasWasLimitedByBestEverStarLevel != profileNoticeMedal.HasWasLimitedByBestEverStarLevel || (HasWasLimitedByBestEverStarLevel && !WasLimitedByBestEverStarLevel.Equals(profileNoticeMedal.WasLimitedByBestEverStarLevel)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProfileNoticeMedal Deserialize(Stream stream, ProfileNoticeMedal instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProfileNoticeMedal DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeMedal profileNoticeMedal = new ProfileNoticeMedal();
			DeserializeLengthDelimited(stream, profileNoticeMedal);
			return profileNoticeMedal;
		}

		public static ProfileNoticeMedal DeserializeLengthDelimited(Stream stream, ProfileNoticeMedal instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProfileNoticeMedal Deserialize(Stream stream, ProfileNoticeMedal instance, long limit)
		{
			instance.MedalType_ = MedalType.UNKNOWN_MEDAL;
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
					instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.LegendRank = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.BestStarLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					if (instance.Chest == null)
					{
						instance.Chest = RewardChest.DeserializeLengthDelimited(stream);
					}
					else
					{
						RewardChest.DeserializeLengthDelimited(stream, instance.Chest);
					}
					continue;
				case 40:
					instance.MedalType_ = (MedalType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.WasLimitedByBestEverStarLevel = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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

		public static void Serialize(Stream stream, ProfileNoticeMedal instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.StarLevel);
			if (instance.HasLegendRank)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LegendRank);
			}
			if (instance.HasBestStarLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BestStarLevel);
			}
			if (instance.HasChest)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Chest.GetSerializedSize());
				RewardChest.Serialize(stream, instance.Chest);
			}
			if (instance.HasMedalType_)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MedalType_);
			}
			if (instance.HasLeagueId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LeagueId);
			}
			if (instance.HasWasLimitedByBestEverStarLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.WasLimitedByBestEverStarLevel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)StarLevel);
			if (HasLegendRank)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LegendRank);
			}
			if (HasBestStarLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BestStarLevel);
			}
			if (HasChest)
			{
				num++;
				uint serializedSize = Chest.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasMedalType_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MedalType_);
			}
			if (HasLeagueId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LeagueId);
			}
			if (HasWasLimitedByBestEverStarLevel)
			{
				num++;
				num++;
			}
			return num + 1;
		}
	}
}
