using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	public class ConnectResponse : IProtoBuf
	{
		public bool HasClientId;

		private ProcessId _ClientId;

		public bool HasBindResult;

		private uint _BindResult;

		public bool HasBindResponse;

		private BindResponse _BindResponse;

		public bool HasContentHandleArray;

		private ConnectionMeteringContentHandles _ContentHandleArray;

		public bool HasServerTime;

		private ulong _ServerTime;

		public bool HasUseBindlessRpc;

		private bool _UseBindlessRpc;

		public bool HasBinaryContentHandleArray;

		private ConnectionMeteringContentHandles _BinaryContentHandleArray;

		public ProcessId ServerId { get; set; }

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

		public uint BindResult
		{
			get
			{
				return _BindResult;
			}
			set
			{
				_BindResult = value;
				HasBindResult = true;
			}
		}

		public BindResponse BindResponse
		{
			get
			{
				return _BindResponse;
			}
			set
			{
				_BindResponse = value;
				HasBindResponse = value != null;
			}
		}

		public ConnectionMeteringContentHandles ContentHandleArray
		{
			get
			{
				return _ContentHandleArray;
			}
			set
			{
				_ContentHandleArray = value;
				HasContentHandleArray = value != null;
			}
		}

		public ulong ServerTime
		{
			get
			{
				return _ServerTime;
			}
			set
			{
				_ServerTime = value;
				HasServerTime = true;
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

		public ConnectionMeteringContentHandles BinaryContentHandleArray
		{
			get
			{
				return _BinaryContentHandleArray;
			}
			set
			{
				_BinaryContentHandleArray = value;
				HasBinaryContentHandleArray = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetServerId(ProcessId val)
		{
			ServerId = val;
		}

		public void SetClientId(ProcessId val)
		{
			ClientId = val;
		}

		public void SetBindResult(uint val)
		{
			BindResult = val;
		}

		public void SetBindResponse(BindResponse val)
		{
			BindResponse = val;
		}

		public void SetContentHandleArray(ConnectionMeteringContentHandles val)
		{
			ContentHandleArray = val;
		}

		public void SetServerTime(ulong val)
		{
			ServerTime = val;
		}

		public void SetUseBindlessRpc(bool val)
		{
			UseBindlessRpc = val;
		}

		public void SetBinaryContentHandleArray(ConnectionMeteringContentHandles val)
		{
			BinaryContentHandleArray = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ServerId.GetHashCode();
			if (HasClientId)
			{
				hashCode ^= ClientId.GetHashCode();
			}
			if (HasBindResult)
			{
				hashCode ^= BindResult.GetHashCode();
			}
			if (HasBindResponse)
			{
				hashCode ^= BindResponse.GetHashCode();
			}
			if (HasContentHandleArray)
			{
				hashCode ^= ContentHandleArray.GetHashCode();
			}
			if (HasServerTime)
			{
				hashCode ^= ServerTime.GetHashCode();
			}
			if (HasUseBindlessRpc)
			{
				hashCode ^= UseBindlessRpc.GetHashCode();
			}
			if (HasBinaryContentHandleArray)
			{
				hashCode ^= BinaryContentHandleArray.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ConnectResponse connectResponse = obj as ConnectResponse;
			if (connectResponse == null)
			{
				return false;
			}
			if (!ServerId.Equals(connectResponse.ServerId))
			{
				return false;
			}
			if (HasClientId != connectResponse.HasClientId || (HasClientId && !ClientId.Equals(connectResponse.ClientId)))
			{
				return false;
			}
			if (HasBindResult != connectResponse.HasBindResult || (HasBindResult && !BindResult.Equals(connectResponse.BindResult)))
			{
				return false;
			}
			if (HasBindResponse != connectResponse.HasBindResponse || (HasBindResponse && !BindResponse.Equals(connectResponse.BindResponse)))
			{
				return false;
			}
			if (HasContentHandleArray != connectResponse.HasContentHandleArray || (HasContentHandleArray && !ContentHandleArray.Equals(connectResponse.ContentHandleArray)))
			{
				return false;
			}
			if (HasServerTime != connectResponse.HasServerTime || (HasServerTime && !ServerTime.Equals(connectResponse.ServerTime)))
			{
				return false;
			}
			if (HasUseBindlessRpc != connectResponse.HasUseBindlessRpc || (HasUseBindlessRpc && !UseBindlessRpc.Equals(connectResponse.UseBindlessRpc)))
			{
				return false;
			}
			if (HasBinaryContentHandleArray != connectResponse.HasBinaryContentHandleArray || (HasBinaryContentHandleArray && !BinaryContentHandleArray.Equals(connectResponse.BinaryContentHandleArray)))
			{
				return false;
			}
			return true;
		}

		public static ConnectResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ConnectResponse DeserializeLengthDelimited(Stream stream)
		{
			ConnectResponse connectResponse = new ConnectResponse();
			DeserializeLengthDelimited(stream, connectResponse);
			return connectResponse;
		}

		public static ConnectResponse DeserializeLengthDelimited(Stream stream, ConnectResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance, long limit)
		{
			instance.UseBindlessRpc = false;
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
					if (instance.ServerId == null)
					{
						instance.ServerId = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ServerId);
					}
					continue;
				case 18:
					if (instance.ClientId == null)
					{
						instance.ClientId = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ClientId);
					}
					continue;
				case 24:
					instance.BindResult = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					if (instance.BindResponse == null)
					{
						instance.BindResponse = BindResponse.DeserializeLengthDelimited(stream);
					}
					else
					{
						BindResponse.DeserializeLengthDelimited(stream, instance.BindResponse);
					}
					continue;
				case 42:
					if (instance.ContentHandleArray == null)
					{
						instance.ContentHandleArray = ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream);
					}
					else
					{
						ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, instance.ContentHandleArray);
					}
					continue;
				case 48:
					instance.ServerTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.UseBindlessRpc = ProtocolParser.ReadBool(stream);
					continue;
				case 66:
					if (instance.BinaryContentHandleArray == null)
					{
						instance.BinaryContentHandleArray = ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream);
					}
					else
					{
						ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, instance.BinaryContentHandleArray);
					}
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

		public static void Serialize(Stream stream, ConnectResponse instance)
		{
			if (instance.ServerId == null)
			{
				throw new ArgumentNullException("ServerId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ServerId.GetSerializedSize());
			ProcessId.Serialize(stream, instance.ServerId);
			if (instance.HasClientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.BindResult);
			}
			if (instance.HasBindResponse)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.BindResponse.GetSerializedSize());
				BindResponse.Serialize(stream, instance.BindResponse);
			}
			if (instance.HasContentHandleArray)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ContentHandleArray.GetSerializedSize());
				ConnectionMeteringContentHandles.Serialize(stream, instance.ContentHandleArray);
			}
			if (instance.HasServerTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.ServerTime);
			}
			if (instance.HasUseBindlessRpc)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.UseBindlessRpc);
			}
			if (instance.HasBinaryContentHandleArray)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.BinaryContentHandleArray.GetSerializedSize());
				ConnectionMeteringContentHandles.Serialize(stream, instance.BinaryContentHandleArray);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = ServerId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasClientId)
			{
				num++;
				uint serializedSize2 = ClientId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasBindResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(BindResult);
			}
			if (HasBindResponse)
			{
				num++;
				uint serializedSize3 = BindResponse.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasContentHandleArray)
			{
				num++;
				uint serializedSize4 = ContentHandleArray.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasServerTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(ServerTime);
			}
			if (HasUseBindlessRpc)
			{
				num++;
				num++;
			}
			if (HasBinaryContentHandleArray)
			{
				num++;
				uint serializedSize5 = BinaryContentHandleArray.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num + 1;
		}
	}
}
