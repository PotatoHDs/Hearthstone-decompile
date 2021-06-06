using System.IO;

namespace PegasusUtil
{
	public class AckRewardTrackReward : IProtoBuf
	{
		public enum PacketID
		{
			ID = 616,
			System = 0
		}

		public bool HasLevel;

		private int _Level;

		public bool HasForPaidTrack;

		private bool _ForPaidTrack;

		public bool HasRewardTrackId;

		private int _RewardTrackId;

		public int Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
			}
		}

		public bool ForPaidTrack
		{
			get
			{
				return _ForPaidTrack;
			}
			set
			{
				_ForPaidTrack = value;
				HasForPaidTrack = true;
			}
		}

		public int RewardTrackId
		{
			get
			{
				return _RewardTrackId;
			}
			set
			{
				_RewardTrackId = value;
				HasRewardTrackId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			if (HasForPaidTrack)
			{
				num ^= ForPaidTrack.GetHashCode();
			}
			if (HasRewardTrackId)
			{
				num ^= RewardTrackId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AckRewardTrackReward ackRewardTrackReward = obj as AckRewardTrackReward;
			if (ackRewardTrackReward == null)
			{
				return false;
			}
			if (HasLevel != ackRewardTrackReward.HasLevel || (HasLevel && !Level.Equals(ackRewardTrackReward.Level)))
			{
				return false;
			}
			if (HasForPaidTrack != ackRewardTrackReward.HasForPaidTrack || (HasForPaidTrack && !ForPaidTrack.Equals(ackRewardTrackReward.ForPaidTrack)))
			{
				return false;
			}
			if (HasRewardTrackId != ackRewardTrackReward.HasRewardTrackId || (HasRewardTrackId && !RewardTrackId.Equals(ackRewardTrackReward.RewardTrackId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AckRewardTrackReward Deserialize(Stream stream, AckRewardTrackReward instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AckRewardTrackReward DeserializeLengthDelimited(Stream stream)
		{
			AckRewardTrackReward ackRewardTrackReward = new AckRewardTrackReward();
			DeserializeLengthDelimited(stream, ackRewardTrackReward);
			return ackRewardTrackReward;
		}

		public static AckRewardTrackReward DeserializeLengthDelimited(Stream stream, AckRewardTrackReward instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AckRewardTrackReward Deserialize(Stream stream, AckRewardTrackReward instance, long limit)
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
					instance.Level = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.ForPaidTrack = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.RewardTrackId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AckRewardTrackReward instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasForPaidTrack)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ForPaidTrack);
			}
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardTrackId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
			}
			if (HasForPaidTrack)
			{
				num++;
				num++;
			}
			if (HasRewardTrackId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardTrackId);
			}
			return num;
		}
	}
}
