using System.IO;

namespace bnet.protocol.connection.v1
{
	public class EchoResponse : IProtoBuf
	{
		public bool HasTime;

		private ulong _Time;

		public bool HasPayload;

		private byte[] _Payload;

		public ulong Time
		{
			get
			{
				return _Time;
			}
			set
			{
				_Time = value;
				HasTime = true;
			}
		}

		public byte[] Payload
		{
			get
			{
				return _Payload;
			}
			set
			{
				_Payload = value;
				HasPayload = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTime(ulong val)
		{
			Time = val;
		}

		public void SetPayload(byte[] val)
		{
			Payload = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTime)
			{
				num ^= Time.GetHashCode();
			}
			if (HasPayload)
			{
				num ^= Payload.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EchoResponse echoResponse = obj as EchoResponse;
			if (echoResponse == null)
			{
				return false;
			}
			if (HasTime != echoResponse.HasTime || (HasTime && !Time.Equals(echoResponse.Time)))
			{
				return false;
			}
			if (HasPayload != echoResponse.HasPayload || (HasPayload && !Payload.Equals(echoResponse.Payload)))
			{
				return false;
			}
			return true;
		}

		public static EchoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EchoResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EchoResponse Deserialize(Stream stream, EchoResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EchoResponse DeserializeLengthDelimited(Stream stream)
		{
			EchoResponse echoResponse = new EchoResponse();
			DeserializeLengthDelimited(stream, echoResponse);
			return echoResponse;
		}

		public static EchoResponse DeserializeLengthDelimited(Stream stream, EchoResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EchoResponse Deserialize(Stream stream, EchoResponse instance, long limit)
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
				case 9:
					instance.Time = binaryReader.ReadUInt64();
					continue;
				case 18:
					instance.Payload = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, EchoResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTime)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Time);
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTime)
			{
				num++;
				num += 8;
			}
			if (HasPayload)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Payload.Length) + Payload.Length);
			}
			return num;
		}
	}
}
