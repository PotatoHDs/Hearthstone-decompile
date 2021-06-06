using System.IO;

namespace bnet.protocol
{
	public class FloatRange : IProtoBuf
	{
		public bool HasMin;

		private float _Min;

		public bool HasMax;

		private float _Max;

		public float Min
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

		public float Max
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

		public void SetMin(float val)
		{
			Min = val;
		}

		public void SetMax(float val)
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
			FloatRange floatRange = obj as FloatRange;
			if (floatRange == null)
			{
				return false;
			}
			if (HasMin != floatRange.HasMin || (HasMin && !Min.Equals(floatRange.Min)))
			{
				return false;
			}
			if (HasMax != floatRange.HasMax || (HasMax && !Max.Equals(floatRange.Max)))
			{
				return false;
			}
			return true;
		}

		public static FloatRange ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FloatRange>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FloatRange Deserialize(Stream stream, FloatRange instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FloatRange DeserializeLengthDelimited(Stream stream)
		{
			FloatRange floatRange = new FloatRange();
			DeserializeLengthDelimited(stream, floatRange);
			return floatRange;
		}

		public static FloatRange DeserializeLengthDelimited(Stream stream, FloatRange instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FloatRange Deserialize(Stream stream, FloatRange instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 13:
					instance.Min = binaryReader.ReadSingle();
					continue;
				case 21:
					instance.Max = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, FloatRange instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMin)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Min);
			}
			if (instance.HasMax)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Max);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMin)
			{
				num++;
				num += 4;
			}
			if (HasMax)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
