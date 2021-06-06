using System.IO;

namespace bnet.protocol.connection.v1
{
	public class ConnectRequest : IProtoBuf
	{
		public bool HasClientId;

		private ProcessId _ClientId;

		public bool HasBindRequest;

		private BindRequest _BindRequest;

		public bool HasUseBindlessRpc;

		private bool _UseBindlessRpc;

		public ProcessId ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
			}
		}

		public BindRequest BindRequest
		{
			get
			{
				return _BindRequest;
			}
			set
			{
				_BindRequest = value;
				HasBindRequest = value != null;
			}
		}

		public bool UseBindlessRpc
		{
			get
			{
				return _UseBindlessRpc;
			}
			set
			{
				_UseBindlessRpc = value;
				HasUseBindlessRpc = true;
			}
		}

		public bool IsInitialized => true;

		public void SetClientId(ProcessId val)
		{
			ClientId = val;
		}

		public void SetBindRequest(BindRequest val)
		{
			BindRequest = val;
		}

		public void SetUseBindlessRpc(bool val)
		{
			UseBindlessRpc = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			if (HasBindRequest)
			{
				num ^= BindRequest.GetHashCode();
			}
			if (HasUseBindlessRpc)
			{
				num ^= UseBindlessRpc.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectRequest connectRequest = obj as ConnectRequest;
			if (connectRequest == null)
			{
				return false;
			}
			if (HasClientId != connectRequest.HasClientId || (HasClientId && !ClientId.Equals(connectRequest.ClientId)))
			{
				return false;
			}
			if (HasBindRequest != connectRequest.HasBindRequest || (HasBindRequest && !BindRequest.Equals(connectRequest.BindRequest)))
			{
				return false;
			}
			if (HasUseBindlessRpc != connectRequest.HasUseBindlessRpc || (HasUseBindlessRpc && !UseBindlessRpc.Equals(connectRequest.UseBindlessRpc)))
			{
				return false;
			}
			return true;
		}

		public static ConnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ConnectRequest DeserializeLengthDelimited(Stream stream)
		{
			ConnectRequest connectRequest = new ConnectRequest();
			DeserializeLengthDelimited(stream, connectRequest);
			return connectRequest;
		}

		public static ConnectRequest DeserializeLengthDelimited(Stream stream, ConnectRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance, long limit)
		{
			instance.UseBindlessRpc = true;
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
				case 10:
					if (instance.ClientId == null)
					{
						instance.ClientId = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ClientId);
					}
					continue;
				case 18:
					if (instance.BindRequest == null)
					{
						instance.BindRequest = BindRequest.DeserializeLengthDelimited(stream);
					}
					else
					{
						BindRequest.DeserializeLengthDelimited(stream, instance.BindRequest);
					}
					continue;
				case 24:
					instance.UseBindlessRpc = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, ConnectRequest instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindRequest)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.BindRequest.GetSerializedSize());
				BindRequest.Serialize(stream, instance.BindRequest);
			}
			if (instance.HasUseBindlessRpc)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.UseBindlessRpc);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientId)
			{
				num++;
				uint serializedSize = ClientId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasBindRequest)
			{
				num++;
				uint serializedSize2 = BindRequest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasUseBindlessRpc)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
