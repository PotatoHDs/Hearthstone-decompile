using System.IO;

namespace PegasusUtil
{
	public class RewardItemOutput : IProtoBuf
	{
		public bool HasRewardItemId;

		private int _RewardItemId;

		public bool HasOutputData;

		private int _OutputData;

		public int RewardItemId
		{
			get
			{
				return _RewardItemId;
			}
			set
			{
				_RewardItemId = value;
				HasRewardItemId = true;
			}
		}

		public int OutputData
		{
			get
			{
				return _OutputData;
			}
			set
			{
				_OutputData = value;
				HasOutputData = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRewardItemId)
			{
				num ^= RewardItemId.GetHashCode();
			}
			if (HasOutputData)
			{
				num ^= OutputData.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RewardItemOutput rewardItemOutput = obj as RewardItemOutput;
			if (rewardItemOutput == null)
			{
				return false;
			}
			if (HasRewardItemId != rewardItemOutput.HasRewardItemId || (HasRewardItemId && !RewardItemId.Equals(rewardItemOutput.RewardItemId)))
			{
				return false;
			}
			if (HasOutputData != rewardItemOutput.HasOutputData || (HasOutputData && !OutputData.Equals(rewardItemOutput.OutputData)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RewardItemOutput Deserialize(Stream stream, RewardItemOutput instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RewardItemOutput DeserializeLengthDelimited(Stream stream)
		{
			RewardItemOutput rewardItemOutput = new RewardItemOutput();
			DeserializeLengthDelimited(stream, rewardItemOutput);
			return rewardItemOutput;
		}

		public static RewardItemOutput DeserializeLengthDelimited(Stream stream, RewardItemOutput instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RewardItemOutput Deserialize(Stream stream, RewardItemOutput instance, long limit)
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
					instance.RewardItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.OutputData = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RewardItemOutput instance)
		{
			if (instance.HasRewardItemId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RewardItemId);
			}
			if (instance.HasOutputData)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OutputData);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRewardItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RewardItemId);
			}
			if (HasOutputData)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OutputData);
			}
			return num;
		}
	}
}
