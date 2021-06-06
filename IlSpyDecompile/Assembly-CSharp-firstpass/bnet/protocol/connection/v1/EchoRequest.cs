using System.IO;
using System.Text;

namespace bnet.protocol.connection.v1
{
	public class EchoRequest : IProtoBuf
	{
		public bool HasTime;

		private ulong _Time;

		public bool HasNetworkOnly;

		private bool _NetworkOnly;

		public bool HasPayload;

		private byte[] _Payload;

		public bool HasForward;

		private ProcessId _Forward;

		public bool HasForwardClientId;

		private string _ForwardClientId;

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

		public bool NetworkOnly
		{
			get
			{
				return _NetworkOnly;
			}
			set
			{
				_NetworkOnly = value;
				HasNetworkOnly = true;
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

		public ProcessId Forward
		{
			get
			{
				return _Forward;
			}
			set
			{
				_Forward = value;
				HasForward = value != null;
			}
		}

		public string ForwardClientId
		{
			get
			{
				return _ForwardClientId;
			}
			set
			{
				_ForwardClientId = value;
				HasForwardClientId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTime(ulong val)
		{
			Time = val;
		}

		public void SetNetworkOnly(bool val)
		{
			NetworkOnly = val;
		}

		public void SetPayload(byte[] val)
		{
			Payload = val;
		}

		public void SetForward(ProcessId val)
		{
			Forward = val;
		}

		public void SetForwardClientId(string val)
		{
			ForwardClientId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTime)
			{
				num ^= Time.GetHashCode();
			}
			if (HasNetworkOnly)
			{
				num ^= NetworkOnly.GetHashCode();
			}
			if (HasPayload)
			{
				num ^= Payload.GetHashCode();
			}
			if (HasForward)
			{
				num ^= Forward.GetHashCode();
			}
			if (HasForwardClientId)
			{
				num ^= ForwardClientId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EchoRequest echoRequest = obj as EchoRequest;
			if (echoRequest == null)
			{
				return false;
			}
			if (HasTime != echoRequest.HasTime || (HasTime && !Time.Equals(echoRequest.Time)))
			{
				return false;
			}
			if (HasNetworkOnly != echoRequest.HasNetworkOnly || (HasNetworkOnly && !NetworkOnly.Equals(echoRequest.NetworkOnly)))
			{
				return false;
			}
			if (HasPayload != echoRequest.HasPayload || (HasPayload && !Payload.Equals(echoRequest.Payload)))
			{
				return false;
			}
			if (HasForward != echoRequest.HasForward || (HasForward && !Forward.Equals(echoRequest.Forward)))
			{
				return false;
			}
			if (HasForwardClientId != echoRequest.HasForwardClientId || (HasForwardClientId && !ForwardClientId.Equals(echoRequest.ForwardClientId)))
			{
				return false;
			}
			return true;
		}

		public static EchoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EchoRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EchoRequest Deserialize(Stream stream, EchoRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EchoRequest DeserializeLengthDelimited(Stream stream)
		{
			EchoRequest echoRequest = new EchoRequest();
			DeserializeLengthDelimited(stream, echoRequest);
			return echoRequest;
		}

		public static EchoRequest DeserializeLengthDelimited(Stream stream, EchoRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EchoRequest Deserialize(Stream stream, EchoRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.NetworkOnly = false;
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
				case 16:
					instance.NetworkOnly = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
					instance.Payload = ProtocolParser.ReadBytes(stream);
					continue;
				case 34:
					if (instance.Forward == null)
					{
						instance.Forward = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Forward);
					}
					continue;
				case 42:
					instance.ForwardClientId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, EchoRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTime)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Time);
			}
			if (instance.HasNetworkOnly)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.NetworkOnly);
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Forward);
			}
			if (instance.HasForwardClientId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ForwardClientId));
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
			if (HasNetworkOnly)
			{
				num++;
				num++;
			}
			if (HasPayload)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Payload.Length) + Payload.Length);
			}
			if (HasForward)
			{
				num++;
				uint serializedSize = Forward.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasForwardClientId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ForwardClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
