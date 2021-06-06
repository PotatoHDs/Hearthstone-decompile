using System.IO;

namespace PegasusUtil
{
	public class ClaimRewardTrackReward : IProtoBuf
	{
		public enum PacketID
		{
			ID = 615,
			System = 0
		}

		public bool HasLevel;

		private int _Level;

		public bool HasForPaidTrack;

		private bool _ForPaidTrack;

		public bool HasChooseOneRewardItemId;

		private int _ChooseOneRewardItemId;

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

		public int ChooseOneRewardItemId
		{
			get
			{
				return _ChooseOneRewardItemId;
			}
			set
			{
				_ChooseOneRewardItemId = value;
				HasChooseOneRewardItemId = true;
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
			if (HasChooseOneRewardItemId)
			{
				num ^= ChooseOneRewardItemId.GetHashCode();
			}
			if (HasRewardTrackId)
			{
				num ^= RewardTrackId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClaimRewardTrackReward claimRewardTrackReward = obj as ClaimRewardTrackReward;
			if (claimRewardTrackReward == null)
			{
				return false;
			}
			if (HasLevel != claimRewardTrackReward.HasLevel || (HasLevel && !Level.Equals(claimRewardTrackReward.Level)))
			{
				return false;
			}
			if (HasForPaidTrack != claimRewardTrackReward.HasForPaidTrack || (HasForPaidTrack && !ForPaidTrack.Equals(claimRewardTrackReward.ForPaidTrack)))
			{
				return false;
			}
			if (HasChooseOneRewardItemId != claimRewardTrackReward.HasChooseOneRewardItemId || (HasChooseOneRewardItemId && !ChooseOneRewardItemId.Equals(claimRewardTrackReward.ChooseOneRewardItemId)))
			{
				return false;
			}
			if (HasRewardTrackId != claimRewardTrackReward.HasRewardTrackId || (HasRewardTrackId && !RewardTrackId.Equals(claimRewardTrackReward.RewardTrackId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ClaimRewardTrackReward Deserialize(Stream stream, ClaimRewardTrackReward instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ClaimRewardTrackReward DeserializeLengthDelimited(Stream stream)
		{
			ClaimRewardTrackReward claimRewardTrackReward = new ClaimRewardTrackReward();
			DeserializeLengthDelimited(stream, claimRewardTrackReward);
			return claimRewardTrackReward;
		}

		public static ClaimRewardTrackReward DeserializeLengthDelimited(Stream stream, ClaimRewardTrackReward instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ClaimRewardTrackReward Deserialize(Stream stream, ClaimRewardTrackReward instance, long limit)
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
					instance.ChooseOneRewardItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
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

		public static void Serialize(Stream stream, ClaimRewardTrackReward instance)
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
			if (instance.HasChooseOneRewardItemId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ChooseOneRewardItemId);
			}
			if (instance.HasRewardTrackId)
			{
				stream.WriteByte(32);
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
			if (HasChooseOneRewardItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ChooseOneRewardItemId);
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
