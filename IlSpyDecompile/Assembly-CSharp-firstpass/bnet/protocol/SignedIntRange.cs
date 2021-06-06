using System.IO;

namespace bnet.protocol
{
	public class SignedIntRange : IProtoBuf
	{
		public bool HasMin;

		private long _Min;

		public bool HasMax;

		private long _Max;

		public long Min
		{
			get
			{
				return _Min;
			}
			set
			{
				_Min = value;
				HasMin = true;
			}
		}

		public long Max
		{
			get
			{
				return _Max;
			}
			set
			{
				_Max = value;
				HasMax = true;
			}
		}

		public bool IsInitialized => true;

		public void SetMin(long val)
		{
			Min = val;
		}

		public void SetMax(long val)
		{
			Max = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMin)
			{
				num ^= Min.GetHashCode();
			}
			if (HasMax)
			{
				num ^= Max.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SignedIntRange signedIntRange = obj as SignedIntRange;
			if (signedIntRange == null)
			{
				return false;
			}
			if (HasMin != signedIntRange.HasMin || (HasMin && !Min.Equals(signedIntRange.Min)))
			{
				return false;
			}
			if (HasMax != signedIntRange.HasMax || (HasMax && !Max.Equals(signedIntRange.Max)))
			{
				return false;
			}
			return true;
		}

		public static SignedIntRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SignedIntRange>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SignedIntRange Deserialize(Stream stream, SignedIntRange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SignedIntRange DeserializeLengthDelimited(Stream stream)
		{
			SignedIntRange signedIntRange = new SignedIntRange();
			DeserializeLengthDelimited(stream, signedIntRange);
			return signedIntRange;
		}

		public static SignedIntRange DeserializeLengthDelimited(Stream stream, SignedIntRange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SignedIntRange Deserialize(Stream stream, SignedIntRange instance, long limit)
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
					instance.Min = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Max = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SignedIntRange instance)
		{
			if (instance.HasMin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Max);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMin)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Min);
			}
			if (HasMax)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Max);
			}
			return num;
		}
	}
}
