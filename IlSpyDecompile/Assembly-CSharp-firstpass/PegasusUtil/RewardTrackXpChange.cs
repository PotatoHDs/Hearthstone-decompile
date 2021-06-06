using System.IO;

namespace PegasusUtil
{
	public class RewardTrackXpChange : IProtoBuf
	{
		public bool HasRewardSourceType;

		private int _RewardSourceType;

		public bool HasRewardSourceId;

		private int _RewardSourceId;

		public bool HasPrevXp;

		private int _PrevXp;

		public bool HasCurrXp;

		private int _CurrXp;

		public bool HasPrevLevel;

		private int _PrevLevel;

		public bool HasCurrLevel;

		private int _CurrLevel;

		public int RewardSourceType
		{
			get
			{
				return _RewardSourceType;
			}
			set
			{
				_RewardSourceType = value;
				HasRewardSourceType = true;
			}
		}

		public int RewardSourceId
		{
			get
			{
				return _RewardSourceId;
			}
			set
			{
				_RewardSourceId = value;
				HasRewardSourceId = true;
			}
		}

		public int PrevXp
		{
			get
			{
				return _PrevXp;
			}
			set
			{
				_PrevXp = value;
				HasPrevXp = true;
			}
		}

		public int CurrXp
		{
			get
			{
				return _CurrXp;
			}
			set
			{
				_CurrXp = value;
				HasCurrXp = true;
			}
		}

		public int PrevLevel
		{
			get
			{
				return _PrevLevel;
			}
			set
			{
				_PrevLevel = value;
				HasPrevLevel = true;
			}
		}

		public int CurrLevel
		{
			get
			{
				return _CurrLevel;
			}
			set
			{
				_CurrLevel = value;
				HasCurrLevel = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardSourceType)
			{
				num ^= RewardSourceType.GetHashCode();
			}
			if (HasRewardSourceId)
			{
				num ^= RewardSourceId.GetHashCode();
			}
			if (HasPrevXp)
			{
				num ^= PrevXp.GetHashCode();
			}
			if (HasCurrXp)
			{
				num ^= CurrXp.GetHashCode();
			}
			if (HasPrevLevel)
			{
				num ^= PrevLevel.GetHashCode();
			}
			if (HasCurrLevel)
			{
				num ^= CurrLevel.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardTrackXpChange rewardTrackXpChange = obj as RewardTrackXpChange;
			if (rewardTrackXpChange == null)
			{
				return false;
			}
			if (HasRewardSourceType != rewardTrackXpChange.HasRewardSourceType || (HasRewardSourceType && !RewardSourceType.Equals(rewardTrackXpChange.RewardSourceType)))
			{
				return false;
			}
			if (HasRewardSourceId != rewardTrackXpChange.HasRewardSourceId || (HasRewardSourceId && !RewardSourceId.Equals(rewardTrackXpChange.RewardSourceId)))
			{
				return false;
			}
			if (HasPrevXp != rewardTrackXpChange.HasPrevXp || (HasPrevXp && !PrevXp.Equals(rewardTrackXpChange.PrevXp)))
			{
				return false;
			}
			if (HasCurrXp != rewardTrackXpChange.HasCurrXp || (HasCurrXp && !CurrXp.Equals(rewardTrackXpChange.CurrXp)))
			{
				return false;
			}
			if (HasPrevLevel != rewardTrackXpChange.HasPrevLevel || (HasPrevLevel && !PrevLevel.Equals(rewardTrackXpChange.PrevLevel)))
			{
				return false;
			}
			if (HasCurrLevel != rewardTrackXpChange.HasCurrLevel || (HasCurrLevel && !CurrLevel.Equals(rewardTrackXpChange.CurrLevel)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RewardTrackXpChange Deserialize(Stream stream, RewardTrackXpChange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardTrackXpChange DeserializeLengthDelimited(Stream stream)
		{
			RewardTrackXpChange rewardTrackXpChange = new RewardTrackXpChange();
			DeserializeLengthDelimited(stream, rewardTrackXpChange);
			return rewardTrackXpChange;
		}

		public static RewardTrackXpChange DeserializeLengthDelimited(Stream stream, RewardTrackXpChange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardTrackXpChange Deserialize(Stream stream, RewardTrackXpChange instance, long limit)
		{
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
					instance.RewardSourceType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.RewardSourceId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PrevXp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CurrXp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.PrevLevel = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CurrLevel = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RewardTrackXpChange instance)
		{
			if (instance.HasRewardSourceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardSourceType);
			}
			if (instance.HasRewardSourceId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardSourceId);
			}
			if (instance.HasPrevXp)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrevXp);
			}
			if (instance.HasCurrXp)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrXp);
			}
			if (instance.HasPrevLevel)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrevLevel);
			}
			if (instance.HasCurrLevel)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CurrLevel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardSourceType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardSourceType);
			}
			if (HasRewardSourceId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardSourceId);
			}
			if (HasPrevXp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrevXp);
			}
			if (HasCurrXp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrXp);
			}
			if (HasPrevLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrevLevel);
			}
			if (HasCurrLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CurrLevel);
			}
			return num;
		}
	}
}
