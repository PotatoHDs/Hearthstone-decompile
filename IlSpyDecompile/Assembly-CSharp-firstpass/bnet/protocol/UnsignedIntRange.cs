using System.IO;

namespace bnet.protocol
{
	public class UnsignedIntRange : IProtoBuf
	{
		public bool HasMin;

		private ulong _Min;

		public bool HasMax;

		private ulong _Max;

		public ulong Min
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

		public ulong Max
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

		public void SetMin(ulong val)
		{
			Min = val;
		}

		public void SetMax(ulong val)
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
			UnsignedIntRange unsignedIntRange = obj as UnsignedIntRange;
			if (unsignedIntRange == null)
			{
				return false;
			}
			if (HasMin != unsignedIntRange.HasMin || (HasMin && !Min.Equals(unsignedIntRange.Min)))
			{
				return false;
			}
			if (HasMax != unsignedIntRange.HasMax || (HasMax && !Max.Equals(unsignedIntRange.Max)))
			{
				return false;
			}
			return true;
		}

		public static UnsignedIntRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsignedIntRange>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UnsignedIntRange Deserialize(Stream stream, UnsignedIntRange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UnsignedIntRange DeserializeLengthDelimited(Stream stream)
		{
			UnsignedIntRange unsignedIntRange = new UnsignedIntRange();
			DeserializeLengthDelimited(stream, unsignedIntRange);
			return unsignedIntRange;
		}

		public static UnsignedIntRange DeserializeLengthDelimited(Stream stream, UnsignedIntRange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UnsignedIntRange Deserialize(Stream stream, UnsignedIntRange instance, long limit)
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
					instance.Min = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Max = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, UnsignedIntRange instance)
		{
			if (instance.HasMin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Max);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMin)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Min);
			}
			if (HasMax)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Max);
			}
			return num;
		}
	}
}
